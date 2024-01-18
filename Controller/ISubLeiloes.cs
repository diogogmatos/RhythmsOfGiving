namespace RhythmsOfGiving.Controller
{
    public interface ISubLeiloes
    {
        public bool registarArtista(string nome,String imagem, int idAdmin);
        public bool registarGeneroMusical(string nome,int idAdmin );
        public bool registarInstituicao(string nome, string descricao, string logoPath, string link, string iban, int idAdmin);
        public bool registarLeilao(int artista, string title, string localizacao, string genero, string tipo, DateTime fim, string shortDescricao, string descricao, float valorBase, string imagePath, string authorImagePath, bool asCegas);

        //public Dictionary<Leilao, Artista> filtrarLeiloesPorGenero(List<int> idsGenero);

        public int GetLicitadorGanhador(int idLeilao);
        public Dictionary<Leilao, Artista> consultarLeiloesAtivos();
        public Dictionary<Leilao, Artista> filtrarLeiloesPorArtista(string nome);

        public int criarLicitacao(int idLicitador, int idLeilao, float valorLicitacao, float valorMinimo);

        public List<Instituicao> ApresentarInstituicoes();
        
         public Dictionary<Leilao, Licitacao> infoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações);

        public string getTituloLeilao(int idLeilao);

        public float calcularValorMinimo(int idLeilao);

        public float getValorFimLeilao (int idLeilao);

    }
}