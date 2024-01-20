namespace RhythmsOfGiving.Controller;

public interface IRhythmsLn
{

    //Registar elementos novos
    public void RegistarLicitador (string nome, string email, string palavraPasse, int nCc, int nif, DateOnly dataNascimento, int nrCartao);

    public bool RegistarArtista(string nome,String imagem, int idAdmin);
        
    public bool RegistarGeneroMusical(string nome,int idAdmin );

    public bool RegistarInstituicao(string nome, string descricao, string logoPath, string link, string iban, int idAdmin);

    public void RegistarLeilao(float valorBase, DateTime dataHoraFinal, string titulo, string descricao, string imagem, string localizacao, int idArtista, int idGenero, int idAdmin, int tipoLeilao);

    
    //Autenticação
    public int ValidarAutenticacao(string email, string palavraPasse);

    
    //Alterar  informações pessoais
    public void AlterarInfosPessoais(string email, string novoNome, DateOnly novaDataNascimento, int novoNumeroCc, string novaPalavraPasse);

    
    //Ver leilões ativos e filtrá-los
    public Dictionary<Leilao, Artista> ConsultarLeiloesAtivos();

    public Dictionary<Leilao, Artista> FiltrarLeiloesPorArtista(string nome);

   // public Dictionary<Leilao, Artista> FiltrarLeiloesPorGenero(List<int> idsGenero);

    //public Dictionary<Leilao, Artista> FiltrarLeiloesPorTipo(List<int> tipos);

    
    //Realizar licitação
    public float CalcularValorMinimo(int idLeilao);

    public int CriarLicitacao(int idLicitador, int idLeilao, float valorLicitacao, float valorMinimo);

    public void AdicionarLicitacao(int idLicitacao, int idLicitador);


    //Notificação Nova Licitação
    public Licitacao ProcurarLicitacaoAtual(int idLeilao);

    public Notificacao CriarNotificacaoUltrapassada(int idLicitador, string titulo, int idLeilao);


    //Notificações Fim Leilão
    public string GetTituloLeilao(int idLeilao);

    public float GetValorFimLeilao (int idLeilao);

    public HashSet<int> GetLicitadoresPerdedores(int idLeilao, int idLicitadorGanhou);
    
    public int GetLicitadorGanhador(int idLeilao);

    public Dictionary<int, Notificacao> CriarNotificacaoPerdedora(HashSet<int> idLicitadores, int idLeilao, string titulo, float valor);

    public Notificacao CriarNotificacaoVencedora(int idLicitador, int idLeilao, string titulo, float valor);


    //Fim de Leilão: Fatura e escolha de instituição
    public List<Instituicao> ApresentarInstituicoes();

    public void PreencherInstituicaoLeilao(int idLeilao, int idInstituicao);

    public void CriarFatura (int idInstituicao, int idLicitacao, int idLicitador);


    //Historiais
    public Dictionary<int, Licitacao> SaberLeiloesParticipa_Licitacao(int idLicitador);

    public Dictionary<Leilao, Licitacao> InfoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações);

    public SortedSet<Licitacao> PesquisarLicitacoes (int idLicitador, int idLeilao);


    //Consultar faturas
    public SortedSet<Fatura> MinhasFaturas(int idLicitador);


    //Consultar leilões ativos
    public Dictionary<Leilao, Licitacao> GetLeiloesAtivosInfos(Dictionary<int, Licitacao> leiloesLicitacoes);

    public TimeSpan CalcularTempoRestante (int idLeilao);    
    
    
    //Estatísticas
    public Dictionary<Instituicao, float> GetValoresInstituicoes();

    public float GetTotalValorDoado();

    public Dictionary<Licitador, float> LicitadoresTop10();



    //Novas funções

    //SubUtilizadores
    public float GetUltimaLicitacaoUtilizador(int idLicitador, int idLeilao);

    public string GetNomeUtilizador(int idLicitador);

    public List<Notificacao> GetNotificacoesUtilizador(int idLicitador);


    //SubLeilões
    public Leilao GetLeilaoById(int id);

    public List<string> GetNomesGenerosMusicais();

    public List<string> GetNomesArtistasMusicais();

}