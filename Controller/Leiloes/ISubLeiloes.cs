namespace RhythmsOfGiving.Controller.Leiloes
{
    public interface ISubLeiloes
    {
        public bool RegistarArtista(string nome,String imagem, int idAdmin);
        public bool RegistarGeneroMusical(string nome,int idAdmin );
        public bool RegistarInstituicao(string nome, string descricao, string logoPath, string link, string iban, int idAdmin);

        public void RegistarLeilao(float valorBase, DateTime dataHoraFinal, string titulo, string descricao,
            string imagem, string localizacao, int idArtista, int idGenero, int idAdmin, int tipoLeilao);

        //public Dictionary<Leilao, Artista> FiltrarLeiloesPorGenero(List<int> idsGenero);

        public int GetLicitadorGanhador(int idLeilao);
        public Dictionary<Leilao, Artista> ConsultarLeiloesAtivos();
        public Dictionary<Leilao, Artista> FiltrarLeiloesPorArtista(string nome);

        public int CriarLicitacao(int idLicitador, int idLeilao, float valorLicitacao, float valorMinimo);

        public List<Instituicao> ApresentarInstituicoes();
        
         public SortedDictionary<Leilao, Licitacao> InfoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações);

        public string GetTituloLeilao(int idLeilao);

        public float CalcularValorMinimo(int idLeilao);

        public float GetValorFimLeilao (int idLeilao);

        public HashSet<int> GetLicitadoresPerdedores(int idLeilao, int idLicitadorGanhou);

        //public Dictionary<Leilao, Artista> FiltrarLeiloesPorTipo(List<int> tipos);

        public Dictionary<Leilao, Licitacao> GetLeiloesAtivosInfos(Dictionary<int, Licitacao> leiloesLicitacoes);

        public Dictionary<Instituicao, float> GetValoresInstituicoes();

        public void PreencherInstituicaoLeilao(int idLeilao, int idInstituicao);

        public TimeSpan CalcularTempoRestante (int idLeilao);

        public Licitacao ProcurarLicitacaoAtual(int idLeilao);

        public float GetTotalValorDoado();

        public Leilao GetLeilaoById(int id);

        public Dictionary<int, string> GetNomesGenerosMusicais();
        public Dictionary<int, string> GetNomesArtistasMusicais();

        public List<Leilao> LeiloesAtivosParaTerminados();

        public Dictionary<Leilao, Artista> GetLeilaoArtistaById(int idLeilao);


        public Leilao DesativarLeilao(int idLeilao);

        public Licitacao GetUltimaLicitacao(int idLeilao);

        public string GetInstituicaoById(int idInstituicao);
    }
}