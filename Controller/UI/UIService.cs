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
    
    // TODO: apresentar texto no elemento quando n tem not
    public List<NotificacaoUi> GetNotificacoesUtilizador(string email)
    {
        int idLicitador = 1;
        List<Notificacao> notificacoesLn = rhythmsLn.GetNotificacoesUtilizador(idLicitador);
        List<NotificacaoUi> notificacoes = new List<NotificacaoUi>();
        foreach (Notificacao not in notificacoesLn)
        {
            notificacoes.Add(new NotificacaoUi(not));
        }
        // notificacoes.Add(new NotificacaoUi(
        //     "A sua licitação foi ultrapassada!",
        //     "The Eras Tour: Exclusive VIP Seating",
        //     DateTime.Parse("2023-11-04"),
        //     0,
        //     1
        // ));
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
        // TODO: Remover
        instituicoes.Add(new InstituicaoUi(
            "The Trevor Project",
            "The Trevor Project é uma organização sem fins lucrativos norte-americana fundada em 1998 em West Hollywood, Califórnia, com o objetivo de informar e prevenir o suicído entre jovens LGBT, incluindo outros queer.",
            "https://mma.prnewswire.com/media/1636688/ttp_logo_primary_tagline_rgb_Logo.jpg?p=twitter",
            "https://www.thetrevorproject.org/"
        ));
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
