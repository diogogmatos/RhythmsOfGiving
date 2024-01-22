using Microsoft.AspNetCore.SignalR;
using RhythmsOfGiving.Controller.Leiloes;
using RhythmsOfGiving.Controller.Utilizadores;

namespace RhythmsOfGiving.Controller.UI;

public class UiService
{
    private readonly RhythmsLn rhythmsLn = new();
    private readonly IHubContext<InfoHub, IInfoHub> context;
    
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
    
    public async Task Licitar(int idLeilao, int idLicitador, float valor)
    {
        float valorBase = rhythmsLn.CalcularValorMinimo(idLeilao);
        int idLicitacao = rhythmsLn.CriarLicitacao(idLicitador, idLeilao, valor, valorBase);
        rhythmsLn.AdicionarLicitacao(idLicitacao, idLicitador);
        await BroadcastAuctionUpdate(idLeilao);
    }
    
    public float GetUltimaLicitacaoUtilizador(int idLicitador, int idLeilao)
    {
        try
        {
            return rhythmsLn.GetUltimaLicitacaoUtilizador(idLicitador, idLeilao);
        }
        catch (Exception e)
        {
            return 0;
        }
    }
    
    public float GetUltimaLicitacaoLeilao(int idLeilao)
    {
        return rhythmsLn.GetValorFimLeilao(idLeilao);
    }
    
    public List<LeilaoUi> GetLeiloesDisponiveis()
    {
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

    public int GetLicitadorGanhador(int idLeilao)
    {
        return rhythmsLn.GetLicitadorGanhador(idLeilao);
    }
    
    // GetNomeGenerosMusicais()
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
        string nome = rhythmsLn.GetNomeUtilizador(idLicitador);
        return new UserUI(email, nome, idLicitador == -1 ? "Admin" : "User");
    }
}
