

namespace RhythmsOfGiving.Controller
{
    public interface ISubLeiloes
    {
        public bool registarArtista(string nome,String imagem, int idAdmin);
        public bool registarGeneroMusical(String nome,int idAdmin );
        public bool registarInstituicao(string nome, string descricao, string logoPath, string link, string iban, int idAdmin)

        //public Dictionary<Leilao, Artista> filtrarLeiloesPorGenero(List<int> idsGenero);

        public int GetLicitadorGanhador(int idLeilao);
        public Dictionary<Leilao, Artista> consultarLeiloesAtivos();
        public Dictionary<Leilao, Artista> filtrarLeiloesPorArtista(string nome);

    }
}