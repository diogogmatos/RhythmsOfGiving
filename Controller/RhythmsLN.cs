namespace RhythmsOfGiving.Controller;

public class RhythmsLn : IRhythmsLn
{
    private ISubUtilizadores subUtilizadores;
    private ISubLeiloes subLeiloes;
    
    //Construtor
    public RhythmsLn()
    {
        this.subUtilizadores = new SubUtilizadores();
        this.subLeiloes = new SubLeiloes();
    }

    public void RegistarLicitador(string nome, string email, string palavraPasse, int nCc, int nif, DateOnly dataNascimento, int nrCartao)
    {
        subUtilizadores.RegistarLicitador(nome, email, palavraPasse, nCc, nif, dataNascimento, nrCartao);
    }

    public int ValidarAutenticacao(string email, string palavraPasse)
    {
        return subUtilizadores.ValidarAutenticacao(email, palavraPasse);
    }

    public void AlterarInfosPessoais(string email, string novoNome, DateTime novaDataNascimento, int novoNumeroCc, string novaPalavraPasse)
    {
        subUtilizadores.AlterarInfosPessoais(email, novoNome, novaDataNascimento, novoNumeroCc, novaPalavraPasse);
    }

    public void AdicionarLicitacao(int idLicitacao, int idLicitador)
    {
        subUtilizadores.AdicionarLicitacao(idLicitacao, idLicitador);
    }

    public Notificacao CriarNotificacaoUltrapassada(int idLicitador, string titulo)
    {
        return subUtilizadores.CriarNotificacaoUltrapassada(idLicitador, titulo);
    }

    public Dictionary<int, Notificacao> CriarNotificacaoPerdedora(HashSet<int> idLicitadores, int idLeilao, string titulo, float valor)
    {
        return subUtilizadores.CriarNotificacaoPerdedora(idLicitadores, idLeilao, titulo, valor);
    }

    public Notificacao CriarNotificacaoVencedora(int idLicitador, int idLeilao, string titulo, float valor)
    {
        return subUtilizadores.CriarNotificacaoVencedora(idLicitador, idLeilao, titulo, valor);
    }

    public Dictionary<int, Licitacao> SaberLeiloesParticipa_Licitacao(int idLicitador)
    {
        return subUtilizadores.saberLeiloesParticipa_Licitacao(idLicitador);
    }

    public SortedSet<Fatura> MinhasFaturas(int idLicitador)
    {
        return subUtilizadores.MinhasFaturas(idLicitador);
    }

    public Dictionary<Licitador, float> LicitadoresTop10()
    {
        return subUtilizadores.LicitadoresTop10();
    }

    public void CriarFatura(int idInstituicao, int idLicitacao, int idLicitador)
    {
        subUtilizadores.CriarFatura(idInstituicao, idLicitacao, idLicitador);
    }
    
    public SortedSet<Licitacao> PesquisarLicitacoes (int idLicitador, int idLeilao)
    {
        return subUtilizadores.PesquisarLicitacoes(idLicitador, idLeilao);
    }

    public bool RegistarArtista(string nome, string imagem, int idAdmin)
    {
        return subLeiloes.RegistarArtista(nome, imagem, idAdmin);
    }

    public bool RegistarGeneroMusical(string nome, int idAdmin)
    {
        return subLeiloes.RegistarGeneroMusical(nome, idAdmin);
    }

    public bool RegistarInstituicao(string nome, string descricao, string logoPath, string link, string iban, int idAdmin)
    {
        return subLeiloes.RegistarInstituicao(nome, descricao, logoPath, link, iban, idAdmin);
    }

    public void RegistarLeilao(float valorBase, DateTime dataHoraFinal, string titulo, string descricao, string imagem, string localizacao, int idArtista, int idGenero, int idAdmin, int tipoLeilao)
    {
        subLeiloes.RegistarLeilao(valorBase, dataHoraFinal, titulo, descricao, imagem, localizacao, idArtista, idGenero, idAdmin, tipoLeilao);
    }

    public Dictionary<Leilao, Artista> FiltrarLeiloesPorGenero(List<int> idsGenero)
    {
        return subLeiloes.FiltrarLeiloesPorGenero(idsGenero);
    }

    public int GetLicitadorGanhador(int idLeilao)
    {
        return subLeiloes.GetLicitadorGanhador(idLeilao);
    }

    public Dictionary<Leilao, Artista> ConsultarLeiloesAtivos()
    {
        return subLeiloes.ConsultarLeiloesAtivos();
    }

    public Dictionary<Leilao, Artista> FiltrarLeiloesPorArtista(string nome)
    {
        return subLeiloes.FiltrarLeiloesPorArtista(nome);
    }

    public int CriarLicitacao(int idLicitador, int idLeilao, float valorLicitacao, float valorMinimo)
    {
        return subLeiloes.CriarLicitacao(idLicitador, idLeilao, valorLicitacao, valorMinimo);
    }

    public List<Instituicao> ApresentarInstituicoes()
    {
        return subLeiloes.ApresentarInstituicoes();
    }

    public Dictionary<Leilao, Licitacao> InfoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações)
    {
        return subLeiloes.InfoLeiloesLicitacoes(ultimasLicitações);
    }

    public string GetTituloLeilao(int idLeilao)
    {
        return subLeiloes.GetTituloLeilao(idLeilao);
    }

    public float CalcularValorMinimo(int idLeilao)
    {
        return subLeiloes.CalcularValorMinimo(idLeilao);
    }

    public float GetValorFimLeilao(int idLeilao)
    {
        return subLeiloes.GetValorFimLeilao(idLeilao);
    }

    public HashSet<int> GetLicitadoresPerdedores(int idLeilao, int idLicitadorGanhou)
    {
        return subLeiloes.GetLicitadoresPerdedores(idLeilao, idLicitadorGanhou);
    }

    public Dictionary<Leilao, Artista> FiltrarLeiloesPorTipo(List<int> tipos)
    {
        return subLeiloes.FiltrarLeiloesPorTipo(tipos);
    }

    public Dictionary<Leilao, Licitacao> GetLeiloesAtivosInfos(Dictionary<int, Licitacao> leiloesLicitacoes)
    {
        return subLeiloes.GetLeiloesAtivosInfos(leiloesLicitacoes);
    }

    public Dictionary<Instituicao, float> GetValoresInstituicoes()
    {
        return subLeiloes.GetValoresInstituicoes();
    }

    public void PreencherInstituicaoLeilao(int idLeilao, int idInstituicao)
    {
        subLeiloes.PreencherInstituicaoLeilao(idLeilao, idInstituicao);
    }

    public TimeSpan CalcularTempoRestante(int idLeilao)
    {
        return subLeiloes.CalcularTempoRestante(idLeilao);
    }

    public Licitacao ProcurarLicitacaoAtual(int idLeilao)
    {
        return subLeiloes.ProcurarLicitacaoAtual(idLeilao);
    }

    public float GetTotalValorDoado()
    {
        return subLeiloes.GetTotalValorDoado();
    }

    public float GetUltimaLicitacaoUtilizador(int idLicitador, int idLeilao)
    {
        return subUtilizadores.GetUltimaLicitacaoUtilizador(idLicitador, idLeilao);
    }

    public Leilao GetLeilaoById(int id)
    {
        return subLeiloes.GetLeilaoById(int id);
    }
}