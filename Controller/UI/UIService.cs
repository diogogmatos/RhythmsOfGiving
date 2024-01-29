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
    
    // TODO: método de gerar notificação de ultrapassagem quando é feita licitação
    
    // TODO: página redimir para escolher instituição de leilão terminado
    
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
    
    // TODO: Verificar utilização desta função (passou de float para Licitacao)
    public Licitacao GetUltimaLicitacaoUtilizador(int idLicitador, int idLeilao)
    {
        try
        {
            return rhythmsLn.GetUltimaLicitacaoUtilizador(idLicitador, idLeilao);
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    public float GetUltimaLicitacaoLeilao(int idLeilao)
    {
        return rhythmsLn.GetValorFimLeilao(idLeilao);
    }
    
    public List<LeilaoUi> GetLeiloesDisponiveis()
    {
        // terminar leilões que já acabaram, notificar licitadores
        new Thread(async () =>
        {
            List<int> idsLicitador = rhythmsLn.CriarNotificacoesFimLeilao();
            await BroadcastNotificationUpdate(idsLicitador);
        }).Start();
        
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
        // terminar leilões que já acabaram, notificar licitadores
        new Thread(async () =>
        {
            List<int> idsLicitador = rhythmsLn.CriarNotificacoesFimLeilao();
            await BroadcastNotificationUpdate(idsLicitador);
        }).Start();
        
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
        ; // TODO
    }

    public void RedimirExperiencia(int idLeilao, int idInstituicao)
    {
        rhythmsLn.PreencherInstituicaoLeilao(idLeilao, idInstituicao);
    }
    
    public List<String> GetGenerosMusicais()
    {
        return rhythmsLn.GetNomesGenerosMusicais();
    }

    public List<String> GetArtistas()
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

    // public Dictionary<LeilaoUi, Licitacao> GetLeilaoLicitacoes(int idLeilao)
    // {
    //     rhythmsLn.InfoLeiloesLicitacoes()
    // }
    
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
    
    public int Login(string email, string password)
    {
        return rhythmsLn.ValidarAutenticacao(email, password);
    }
    
    public void Registar(string nome, DateOnly dataNascimento, int nrCc, int nif, int cartaoDebitoCredito,
        string email, string password)
    {
        rhythmsLn.RegistarLicitador(nome, email, password, nrCc, nif, dataNascimento, cartaoDebitoCredito);
    }
    
    public UserUI GetInformacaoUtilizador(int idLicitador, string email)
    {
        string nome = idLicitador == -1 ? "Admin" : rhythmsLn.GetNomeUtilizador(idLicitador);
        return new UserUI(email, nome, idLicitador == -1 ? "Admin" : "User");
    }
}
