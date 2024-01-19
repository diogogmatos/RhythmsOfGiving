namespace RhythmsOfGiving.Controller.UI;

public class UIService
{
    // TODO
    public List<LeilaoUI> GetLeiloesDisponiveis()
    {
        List<LeilaoUI> leiloes = new List<LeilaoUI>();
        leiloes.Add(new LeilaoUI(
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

    public LeilaoUI GetLeilaoById(int id)
    {
        return new LeilaoUI(
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

    // TODO
    public List<NotificacaoUI> GetNotificacoesUtilizador(string email)
    {
        List<NotificacaoUI> notificacoes = new List<NotificacaoUI>();
        notificacoes.Add(new NotificacaoUI(
            "A sua licitação foi ultrapassada!",
            "The Eras Tour: Exclusive VIP Seating",
            DateTime.Parse("2023-11-04"),
            0,
            1
        ));
        return notificacoes;
    }
    
    // TODO
    public List<InstituicaoUI> GetInstituicoes()
    {
        List<InstituicaoUI> instituicoes = new List<InstituicaoUI>();
        instituicoes.Add(new InstituicaoUI(
            "The Trevor Project",
            "The Trevor Project é uma organização sem fins lucrativos norte-americana fundada em 1998 em West Hollywood, Califórnia, com o objetivo de informar e prevenir o suicído entre jovens LGBT, incluindo outros queer.",
            "https://mma.prnewswire.com/media/1636688/ttp_logo_primary_tagline_rgb_Logo.jpg?p=twitter",
            "https://www.thetrevorproject.org/"
        ));
        return instituicoes;
    }
}