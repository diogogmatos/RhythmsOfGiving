using Microsoft.AspNetCore.SignalR;
using RhythmsOfGiving.Controller.Leiloes;
using RhythmsOfGiving.Controller.Utilizadores;

namespace RhythmsOfGiving.Controller.UI;

public class UiService
{
    private readonly RhythmsLn rhythmsLn = new();
    private readonly IHubContext<InfoHub, IInfoHub> context;
    private readonly ReaderWriterLockSlim lockSlim = new();
    
    public UiService(IHubContext<InfoHub, IInfoHub> context)
    {
        this.context = context;
    }
    
    // leilões
    
    private async Task BroadcastAuctionUpdate(int idLeilao)
    {
        await context.Clients.All.UpdateAuctionInfo(idLeilao);
    }

    private async Task BroadcastNotificationUpdate(List<int> idsLicitador)
    {
        await context.Clients.All.UpdateNotificationInfo(idsLicitador);
    }
    
    public async Task Licitar(int idLeilao, int idLicitador, float valor)
    {
        // adicionar licitação
        float valorBase = rhythmsLn.CalcularValorMinimo(idLeilao);
        int idLicitacao = rhythmsLn.CriarLicitacao(idLicitador, idLeilao, valor, valorBase);
        rhythmsLn.AdicionarLicitacao(idLicitacao, idLicitador);
        await BroadcastAuctionUpdate(idLeilao);
        // notificar licitador anterior
        Leilao leilao = rhythmsLn.GetLeilaoById(idLeilao);
        if (leilao.GetTipo() != 0)
        {
            Licitacao licitacao = rhythmsLn.ProcurarLicitacaoAtual(idLeilao);
            rhythmsLn.CriarNotificacaoUltrapassada(licitacao.GetIdLicitador(), "'" + leilao.Titulo + "': " + valor.ToString("0.00€"),
                idLeilao);
            // notificar apenas licitador ultrapassado
            List<int> idsLicitador = new List<int>();
            idsLicitador.Add(licitacao.GetIdLicitador());
            await BroadcastNotificationUpdate(idsLicitador);
        }
    }
    
    public Licitacao GetUltimaLicitacaoUtilizador(int idLicitador, int idLeilao)
    {
        return rhythmsLn.GetUltimaLicitacaoUtilizador(idLicitador, idLeilao);
    }
    
    public Licitacao GetUltimaLicitacaoLeilao(int idLeilao)
    {

        return rhythmsLn.GetUltimaLicitacao(idLeilao);
    }

    private void AtualizarLeiloes()
    {
        // terminar leilões que já acabaram, notificar licitadores
        new Thread(async () =>
        {
            List<int> idsLicitador = rhythmsLn.CriarNotificacoesFimLeilao();
            await BroadcastNotificationUpdate(idsLicitador);
        }).Start();
    }
    
    public List<LeilaoUi> GetLeiloesDisponiveis()
    {
        AtualizarLeiloes();
        
        List<LeilaoUi> leiloes = new List<LeilaoUi>();
        try
        {
            Dictionary<Leilao, Artista> leiloesLn = rhythmsLn.ConsultarLeiloesAtivos();
            foreach (KeyValuePair<Leilao, Artista> leilao in leiloesLn)
            {
                leiloes.Add(new LeilaoUi(leilao.Key, leilao.Value));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return leiloes;
    }
    
    public LeilaoUi GetLeilaoById(int idLeilao)
    {
        AtualizarLeiloes();
        
        Dictionary<Leilao, Artista> leilaoInfo = rhythmsLn.GetLeilaoArtistaById(idLeilao);
        foreach (var leilao in leilaoInfo)
        {
            Leilao l = leilao.Key;
            Artista a = leilao.Value;
            return new LeilaoUi(l, a);
        }
        throw new Exception("Leilão não encontrado.");
    }

    public bool IsLeilaoAtivo(int idLeilao)
    {
        Leilao leilaoInfo = rhythmsLn.GetLeilaoById(idLeilao);
        return leilaoInfo.Ativo;
    }

    public bool IsLeilaoFinalizado(int idLeilao)
    {
        Leilao leilaoInfo = rhythmsLn.GetLeilaoById(idLeilao);
        return leilaoInfo.IdInstituicao != -1;
    }

    public int GetLicitadorGanhador(int idLeilao)
    {
        return rhythmsLn.GetLicitadorGanhador(idLeilao);
    }

    public void TerminarLeilao(int idLeilao)
    {
        rhythmsLn.DesativarLeilao(idLeilao);
        AtualizarLeiloes();
    }

    public void RedimirExperiencia(int idLeilao, int idInstituicao, int idLicitacao, int idLicitador)
    {
        rhythmsLn.PreencherInstituicaoLeilao(idLeilao, idInstituicao);
        rhythmsLn.CriarFatura(idInstituicao, idLicitacao, idLicitador);
    }
    
    public Dictionary<int, String> GetGenerosMusicais()
    {
        return rhythmsLn.GetNomesGenerosMusicais();
    }

    public Dictionary<int, String> GetArtistas()
    {
        return rhythmsLn.GetNomesArtistasMusicais();
    }
    
    // notificações
    
    public List<NotificacaoUi> GetNotificacoesUtilizador(int idLicitador)
    {
        List<Notificacao> notificacoesLn = rhythmsLn.GetNotificacoesUtilizador(idLicitador);
        List<NotificacaoUi> notificacoes = new List<NotificacaoUi>();
        foreach (Notificacao not in notificacoesLn)
        {
            notificacoes.Add(new NotificacaoUi(not));
        }
        return notificacoes;
    }

    public Dictionary<LeilaoUi, Licitacao> GetLeiloesAtivos(int idLicitador)
    {
        Dictionary<LeilaoUi, Licitacao> leiloes = new Dictionary<LeilaoUi, Licitacao>();
        Dictionary<Leilao, Licitacao> leiloesLn = rhythmsLn.GetLeiloesAtivosLicitador(idLicitador);
        foreach (KeyValuePair<Leilao, Licitacao> leilao in leiloesLn)
        {
            Dictionary<Leilao, Artista> la = rhythmsLn.GetLeilaoArtistaById(leilao.Key.IdLeilao);
            LeilaoUi lui = new LeilaoUi(la.Keys.First(), la.Values.First());
            leiloes.Add(lui, leilao.Value);
        }
        return leiloes;
    }
    
    // instituições
    
    public List<InstituicaoUi> GetInstituicoes()
    {
        List<InstituicaoUi> instituicoes = new List<InstituicaoUi>();
        List<Instituicao> instituicoesLn = rhythmsLn.ApresentarInstituicoes();
        foreach (Instituicao instituicao in instituicoesLn)
        {
            instituicoes.Add(new InstituicaoUi(instituicao));
        }
        return instituicoes;
    }
    
    // autenticação
    
    public Dictionary<int, bool> Login(string email, string password)
    {
        return rhythmsLn.ValidarAutenticacao(email, password);
    }
    
    public void Registar(string nome, DateOnly dataNascimento, int nrCc, int nif, int cartaoDebitoCredito,
        string email, string password)
    {
        rhythmsLn.RegistarLicitador(nome, email, password, nrCc, nif, dataNascimento, cartaoDebitoCredito);
    }
    
    public UserUI GetInformacaoUtilizador(int idLicitador, string email, bool isAdmin)
    {
        string nome = isAdmin ? "Admin" : rhythmsLn.GetNomeUtilizador(idLicitador);
        return new UserUI(email, nome, isAdmin ? "Admin" : "User");
    }
    
    // licitador

    public Licitador GetLicitadorById(int idLicitador)
    {
        return rhythmsLn.GetLicitadorById(idLicitador);
    }

    public void EditarInformacoes(string nome, DateOnly dataNascimento, int nrCartao, string email,
        string password)
    {
        rhythmsLn.AlterarInfosPessoais(email, nome, dataNascimento, nrCartao, password);
    }
    
    public SortedDictionary<LeilaoUi, Licitacao> GetHistorial(int idLicitador)
    {
        SortedDictionary<LeilaoUi, Licitacao> historial = new SortedDictionary<LeilaoUi, Licitacao>();
        foreach (var leilao in rhythmsLn.HistorialLeiloes(idLicitador))
        {
            Dictionary<Leilao, Artista> la = rhythmsLn.GetLeilaoArtistaById(leilao.Key.IdLeilao);
            LeilaoUi lui = new LeilaoUi(la.Keys.First(), la.Values.First());
            historial.Add(lui, leilao.Value);
        }
        return historial;
    }

    public SortedSet<Licitacao> GetLicitacoesLicitadorLeilao(int idLicitador, int idLeilao)
    {
        return rhythmsLn.PesquisarLicitacoes(idLicitador, idLeilao);
    }

    public SortedSet<Fatura> GetFaturasLicitador(int idLicitador)
    {
        return rhythmsLn.MinhasFaturas(idLicitador);
    }

    public string GetNomeInstituicao(int idInstituicao)
    {
        return rhythmsLn.GetInstituicaoById(idInstituicao);
    }
    
    // estatísticas

    public float GetValorTotalDoado()
    {
        return rhythmsLn.GetTotalValorDoado();
    }

    public Dictionary<Instituicao, float> GetValorTotalDoadoInstituicoes()
    {
        return rhythmsLn.GetValoresInstituicoes();
    }

    public Dictionary<Licitador, float> GetTop10Licitadores()
    {
        return rhythmsLn.LicitadoresTop10();
    }
    
    // admin

    public void AdicionarLeilao(int idAdmin, string titulo, string descricao, int idArtista, int idTipo, int idGeneroMusical, string localizacao, DateTime fim, string imageUrl, float valorBase)
    {
        rhythmsLn.RegistarLeilao(valorBase, fim, titulo, descricao, imageUrl, localizacao, idArtista, idGeneroMusical, idAdmin, idTipo);
    }

    public void AdicionarInstituicao(string nome, string descricao, string imageurl, string url, string iban, int idAdmin)
    {
        rhythmsLn.RegistarInstituicao(nome, descricao, imageurl, url, iban, idAdmin);
    }

    public void AdicionarGeneroMusical(string nome, int idAdmin)
    {
        rhythmsLn.RegistarGeneroMusical(nome, idAdmin);
    }

    public void AdicionarArtista(string nome, string imageurl, int idAdmin)
    {
        rhythmsLn.RegistarArtista(nome, imageurl, idAdmin);
    }
}
