/*namespace RhythmsOfGiving.Controller
{
    public interface ISubLeiloes
    {
        public bool registarArtista(string nome,String imagem, int idAdmin);
        public bool registarGeneroMusical(string nome,int idAdmin );
        public bool registarInstituicao(string nome, string descricao, string logoPath, string link, string iban, int idAdmin);

        public void registarLeilao(float valorBase, DateTime dataHoraFinal, string titulo, string descricao,
            string imagem, string localizacao, int idArtista, int idGenero, int idAdmin, int tipoLeilao);

        public Dictionary<Leilao, Artista> filtrarLeiloesPorGenero(List<int> idsGenero);

        public int GetLicitadorGanhador(int idLeilao);
        public Dictionary<Leilao, Artista> consultarLeiloesAtivos();
        public Dictionary<Leilao, Artista> filtrarLeiloesPorArtista(string nome);

        public int criarLicitacao(int idLicitador, int idLeilao, float valorLicitacao, float valorMinimo);

        public List<Instituicao> ApresentarInstituicoes();
        
         public Dictionary<Leilao, Licitacao> infoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações);

        public string getTituloLeilao(int idLeilao);

        public float calcularValorMinimo(int idLeilao);

        public float getValorFimLeilao (int idLeilao);

        public HashSet<int> getLicitadoresPerdedores(int idLeilao, int idLicitadorGanhou);

        public Dictionary<Leilao, Artista> filtrarLeiloesPorTipo(List<int> tipos);

        public Dictionary<Leilao, Licitacao> getLeiloesAtivosInfos(Dictionary<int, Licitacao> leiloesLicitacoes);

        public Dictionary<Instituicao, float> getValoresInstituicoes();

        public void preencherInstituicaoLeilao(int idLeilao, int idInstituicao);

        public TimeSpan calcularTempoRestante (int idLeilao);

        public Licitacao procurarLicitacaoAtual(int idLeilao);

        public float getTotalValorDoado();

    }
}*/