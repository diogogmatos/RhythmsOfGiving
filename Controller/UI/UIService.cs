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
    
    // leilões
    
    private async Task BroadcastAuctionUpdate()
    {
        await context.Clients.All.UpdateAuctionInfo();
    }

    private float valorTeste = 7600f;

    // TODO
    public async void Licitar(int idLeilao, string email, float valor)
    {
        valorTeste = valor;
        // TODO: Método AdicionarLicitacao da LN recebe idLicitador, não email; recebe idLicitacao, não idLeilao
        await BroadcastAuctionUpdate();
    }

    // TODO: Sem método necessário na LN
    public float GetUltimaLicitacaoUtilizador(string email, int idLeilao)
    {
        return valorTeste;
    }

    // TODO: Sem método necessário na LN
    public float GetUltimaLicitacaoLeilao(string email, int idLeilao)
    {
        return 8543.92f;
    }
    
    private int shortDescSize = 100;
    
    // TODO
    public List<LeilaoUi> GetLeiloesDisponiveis()
    {
        List<LeilaoUi> leiloes = new List<LeilaoUi>();
        try
        {
            Dictionary<Leilao, Artista> leiloesLn = rhythmsLn.ConsultarLeiloesAtivos();
            foreach (KeyValuePair<Leilao, Artista> leilao in leiloesLn)
            {
                string tipoLeilao = leilao.Key.GetTipo() == 0 ? "Ás Cegas" : "Inglês";
                leiloes.Add(new LeilaoUi(
                    leilao.Key.IdLeilao,
                    leilao.Value.GetNome(),
                    leilao.Key.Titulo,
                    leilao.Key.Experiencia.GetLocalizacao(),
                    leilao.Key.Experiencia.GetGeneroMusical().GetNome(),
                    tipoLeilao,
                    leilao.Key.DataHoraFinal,
                    leilao.Key.Experiencia.GetDescricao().Substring(0,
                        Math.Min(leilao.Key.Experiencia.GetDescricao().Length, shortDescSize)),
                    leilao.Key.Experiencia.GetDescricao(),
                    leilao.Key.Experiencia.GetImagem(),
                    leilao.Value.GetImagem(),
                    leilao.Key.GetTipo() == 0,
                    leilao.Key.ValorBase
                ));
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

    // TODO: Sem método necessário na LN
    public LeilaoUi GetLeilaoById(int id)
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
    
    // notificações

    // TODO: Sem método necessário na LN
    public List<NotificacaoUi> GetNotificacoesUtilizador(string email)
    {
        List<NotificacaoUi> notificacoes = new List<NotificacaoUi>();
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
            instituicoes.Add(new InstituicaoUi(
                instituicao.GetNome(),
                instituicao.GetDescricao(),
                instituicao.GetLogoPath(),
                instituicao.GetLink()    
            ));
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
    
    // perfil
    
    // TODO: Sem método necessário na LN
    public string GetNomeUtilizador(string email)
    {
        return "Diogo";
    }
}