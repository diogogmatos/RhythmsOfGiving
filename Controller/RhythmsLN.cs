namespace RhythmsOfGiving.Controller;

public class RhythmsLN : IRhythmsLN
{
    private ISubUtilizadores subUtilizadores;
    private ISubLeiloes subLeiloes;
    
    //Construtor
    public RhythmsLN()
    {
        this.subUtilizadores = new SubUtilizadores();
        this.subLeiloes = new SubLeiloes();
    }

    public void RegistarLicitador(string nome, string email, string palavraPasse, int nCC, int nif, DateOnly dataNascimento, int nrCartao)
    {
        subUtilizadores.registarLicitador(nome, email, palavraPasse, nCC, nif, dataNascimento, nrCartao);
    }

    public bool ValidarAutenticacao(string email, string palavraPasse)
    {
        return subUtilizadores.validarAutenticacao(email, palavraPasse);
    }

    public void AlterarInfosPessoais(string email, string novoNome, DateTime novaDataNascimento, int novoNumeroCC, string novaPalavraPasse)
    {
        subUtilizadores.AlterarInfosPessoais(email, novoNome, novaDataNascimento, novoNumeroCC, novaPalavraPasse);
    }

    public void AdicionarLicitacao(int idLicitacao, int idLicitador)
    {
        subUtilizadores.AdicionarLicitacao(idLicitacao, idLicitador);
    }

    public Notificacao CriarNotificacaoUltrapassada(int idLicitador, string titulo)
    {
        return subUtilizadores.criarNotificacaoUltrapassada(idLicitador, titulo);
    }

    public Dictionary<int, Notificacao> CriarNotificacaoPerdedora(HashSet<int> idLicitadores, int idLeilao, string titulo, float valor)
    {
        return subUtilizadores.criarNotificacaoPerdedora(idLicitadores, idLeilao, titulo, valor);
    }

    public Notificacao CriarNotificacaoVencedora(int idLicitador, int idLeilao, string titulo, float valor)
    {
        return subUtilizadores.criarNotificacaoVencedora(idLicitador, idLeilao, titulo, valor);
    }

    public Dictionary<int, Licitacao> SaberLeiloesParticipa_Licitacao(int idLicitador)
    {
        return subUtilizadores.saberLeiloesParticipa_Licitacao(idLicitador);
    }

    public SortedSet<Fatura> MinhasFaturas(int idLicitador)
    {
        return subUtilizadores.minhasFaturas(idLicitador);
    }

    public Dictionary<Licitador, float> LicitadoresTop10()
    {
        return subUtilizadores.licitadoresTop10();
    }

    public void CriarFatura(int idInstituicao, int idLicitacao, int idLicitador)
    {
        subUtilizadores.criarFatura(idInstituicao, idLicitacao, idLicitador);
    }
    
    public SortedSet<Licitacao> PesquisarLicitacoes (int idLicitador, int idLeilao)
    {
        return subUtilizadores.pesquisarLicitacoes(idLicitador, idLeilao);
    }

    public bool RegistarArtista(string nome, string imagem, int idAdmin)
    {
        return subLeiloes.registarArtista(nome, imagem, idAdmin);
    }

    public bool RegistarGeneroMusical(string nome, int idAdmin)
    {
        return subLeiloes.registarGeneroMusical(nome, idAdmin);
    }

    public bool RegistarInstituicao(string nome, string descricao, string logoPath, string link, string iban, int idAdmin)
    {
        return subLeiloes.registarInstituicao(nome, descricao, logoPath, link, iban, idAdmin);
    }

    public void RegistarLeilao(float valorBase, DateTime dataHoraFinal, string titulo, string descricao, string imagem, string localizacao, int idArtista, int idGenero, int idAdmin, int tipoLeilao)
    {
        subLeiloes.registarLeilao(valorBase, dataHoraFinal, titulo, descricao, imagem, localizacao, idArtista, idGenero, idAdmin, tipoLeilao);
    }

    public Dictionary<Leilao, Artista> FiltrarLeiloesPorGenero(List<int> idsGenero)
    {
        return subLeiloes.filtrarLeiloesPorGenero(idsGenero);
    }

    public int GetLicitadorGanhador(int idLeilao)
    {
        return subLeiloes.GetLicitadorGanhador(idLeilao);
    }

    public Dictionary<Leilao, Artista> ConsultarLeiloesAtivos()
    {
        return subLeiloes.consultarLeiloesAtivos();
    }

    public Dictionary<Leilao, Artista> FiltrarLeiloesPorArtista(string nome)
    {
        return subLeiloes.filtrarLeiloesPorArtista(nome);
    }

    public int CriarLicitacao(int idLicitador, int idLeilao, float valorLicitacao, float valorMinimo)
    {
        return subLeiloes.criarLicitacao(idLicitador, idLeilao, valorLicitacao, valorMinimo);
    }

    public List<Instituicao> ApresentarInstituicoes()
    {
        return subLeiloes.ApresentarInstituicoes();
    }

    public Dictionary<Leilao, Licitacao> InfoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações)
    {
        return subLeiloes.infoLeiloesLicitacoes(ultimasLicitações);
    }

    public string GetTituloLeilao(int idLeilao)
    {
        return subLeiloes.getTituloLeilao(idLeilao);
    }

    public float CalcularValorMinimo(int idLeilao)
    {
        return subLeiloes.calcularValorMinimo(idLeilao);
    }

    public float GetValorFimLeilao(int idLeilao)
    {
        return subLeiloes.getValorFimLeilao(idLeilao);
    }

    public HashSet<int> GetLicitadoresPerdedores(int idLeilao, int idLicitadorGanhou)
    {
        return subLeiloes.getLicitadoresPerdedores(idLeilao, idLicitadorGanhou);
    }

    public Dictionary<Leilao, Artista> FiltrarLeiloesPorTipo(List<int> tipos)
    {
        return subLeiloes.filtrarLeiloesPorTipo(tipos);
    }

    public Dictionary<Leilao, Licitacao> GetLeiloesAtivosInfos(Dictionary<int, Licitacao> leiloesLicitacoes)
    {
        return subLeiloes.getLeiloesAtivosInfos(leiloesLicitacoes);
    }

    public Dictionary<Instituicao, float> GetValoresInstituicoes()
    {
        return subLeiloes.getValoresInstituicoes();
    }

    public void PreencherInstituicaoLeilao(int idLeilao, int idInstituicao)
    {
        subLeiloes.preencherInstituicaoLeilao(idLeilao, idInstituicao);
    }

    public TimeSpan CalcularTempoRestante(int idLeilao)
    {
        return subLeiloes.calcularTempoRestante(idLeilao);
    }

    public Licitacao ProcurarLicitacaoAtual(int idLeilao)
    {
        return subLeiloes.procurarLicitacaoAtual(idLeilao);
    }

    public float GetTotalValorDoado()
    {
        return subLeiloes.getTotalValorDoado();
    }
}