using Microsoft.AspNetCore.SignalR;

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
    
    public async void Licitar(int idLeilao, string email, float valor)
    {
        int idLicitador = 1; // TODO: Método que dado o email devolva o idLicitador
        float valorBase = rhythmsLn.CalcularValorMinimo(idLeilao);
        int idLicitacao = rhythmsLn.CriarLicitacao(idLicitador, idLeilao, valor, valorBase);
        rhythmsLn.AdicionarLicitacao(idLicitacao, idLicitador);
        await BroadcastAuctionUpdate(idLeilao);
    }
    
    public float GetUltimaLicitacaoUtilizador(string email, int idLeilao)
    {
        // int idLicitador = 1; // TODO: Método que dado o email devolva o idLicitador
        // return rhythmsLn.GetUltimaLicitacaoUtilizador(idLicitador, idLeilao);
        return 9000f;
    }
    
    public float GetUltimaLicitacaoLeilao(int idLeilao)
    {
        // TODO: ?
        // return rhythmsLn.GetValorFimLeilao(idLeilao);
        return 9000f;
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
        // TODO: Remover
        leiloes.Add(new LeilaoUi(
            1,
            "Taylor Swift",
            "The Eras Tour: Exclusive VIP Seating ",
            "Lisboa",
            "Pop",
            "Inglês",
            DateTime.Parse("2023-12-24"),
            "Viva uma experiência única e exclusiva num espetáculo do maior nome na indústria musical. Tenha acesso a uma visão total e desobstruída para o palco, com acesso incluído a bar e outras vantagens.",
            "Participe neste emocionante leilão e tenha a oportunidade de experienciar o espetáculo mais esperado do ano de uma forma que nunca imaginou possível. Para além de desfrutar de uma vista privilegiada e desobstruída do palco, será tratado como uma verdadeira celebridade. O seu acesso VIP inclui uma entrada exclusiva para o bar, onde poderá degustar os melhores cocktails e petiscos enquanto se prepara para uma noite inesquecível. Sinta-se como parte integrante do espetáculo, enquanto Taylor Swift hipnotiza o público com seus êxitos lendários e faixas do seu último álbum.",
            "https://assets.teenvogue.com/photos/641b2a23912ddccbabf80f80/16:9/w_2560%2Cc_limit/GettyImages-1474459622.jpg",
            "https://i.scdn.co/image/ab67616100005174859e4c14fa59296c8649e0e4",
            false,
            7500f
        ));
        return leiloes;
    }

    // TODO: Sem método necessário na LN
    //Já há método GetLeilaoById mas devolve Leilao e não LeilaoUi
    public LeilaoUi GetLeilaoById(int idLeilao)
    {
        return new LeilaoUi(
            1,
            "Taylor Swift",
            "The Eras Tour: Exclusive VIP Seating ",
            "Lisboa",
            "Pop",
            "Inglês",
            DateTime.Parse("2023-12-24"),
            "Viva uma experiência única e exclusiva num espetáculo do maior nome na indústria musical. Tenha acesso a uma visão total e desobstruída para o palco, com acesso incluído a bar e outras vantagens.",
            "Participe neste emocionante leilão e tenha a oportunidade de experienciar o espetáculo mais esperado do ano de uma forma que nunca imaginou possível. Para além de desfrutar de uma vista privilegiada e desobstruída do palco, será tratado como uma verdadeira celebridade. O seu acesso VIP inclui uma entrada exclusiva para o bar, onde poderá degustar os melhores cocktails e petiscos enquanto se prepara para uma noite inesquecível. Sinta-se como parte integrante do espetáculo, enquanto Taylor Swift hipnotiza o público com seus êxitos lendários e faixas do seu último álbum.",
            "https://assets.teenvogue.com/photos/641b2a23912ddccbabf80f80/16:9/w_2560%2Cc_limit/GettyImages-1474459622.jpg",
            "https://i.scdn.co/image/ab67616100005174859e4c14fa59296c8649e0e4",
            false,
            7500f
        );
    }
    
    // GetNomeGenerosMusicais()
    public List<String> GetGenerosMusicais()
    {
        List<String> generos = new List<String>();
        generos.Add("Pop");
        generos.Add("Rock");
        generos.Add("Hip Hop");
        generos.Add("Rap");
        generos.Add("Funk");
        generos.Add("Fado");
        generos.Add("Jazz");
        generos.Add("Blues");
        generos.Add("Soul");
        generos.Add("Reggae");
        generos.Add("Eletrónica");
        generos.Add("Clássica");
        generos.Add("Alternativa");
        return generos;
    }

    public List<String> GetArtistas()
    {
        // GetNomesArtistasMusicais()
        List<String> artistas = new List<String>();
        artistas.Add("Taylor Swift");
        artistas.Add("Harry Styles");
        artistas.Add("Ariana Grande");
        artistas.Add("Billie Eilish");
        artistas.Add("Drake");
        artistas.Add("Ed Sheeran");
        artistas.Add("Dua Lipa");
        artistas.Add("Justin Bieber");
        artistas.Add("The Weeknd");
        return artistas;
    }
    
    // notificações
    
    public List<NotificacaoUi> GetNotificacoesUtilizador(string email)
    {
        int idLicitador = 1;
        List<Notificacao> notificacoesLn = rhythmsLn.GetNotificacoesUtilizador(idLicitador);
        List<NotificacaoUi> notificacoes = new List<NotificacaoUi>();
        foreach (Notificacao not in notificacoesLn)
        {
            notificacoes.Add(new NotificacaoUi(not));
        }
        notificacoes.Add(new NotificacaoUi(
            "A sua licitação foi ultrapassada!",
            "The Eras Tour: Exclusive VIP Seating",
            DateTime.Parse("2023-11-04"),
            0,
            1
        ));
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
    
    public bool Login(string email, string password)
    {
        // ValidarAutenticacao(email, password);
        return true;
    }
    
    public void Registar(string nome, DateOnly dataNascimento, int nrCc, int nif, int cartaoDebitoCredito,
        string email, string password)
    {
        rhythmsLn.RegistarLicitador(nome, email, password, nrCc, nif, dataNascimento, cartaoDebitoCredito);
    }
    
    // TODO: Sem método necessário na LN - preciso do nome do utilizador e de se ele é admin ou não
    public UserUI GetInformacaoUtilizador(string email)
    {
        return new UserUI(email, "Admin", "User");
    }
}
