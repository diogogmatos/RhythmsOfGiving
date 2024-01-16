

namespace RhythmsOfGiving.Controller
{
    public interface ISubLeiloes
    {
       public bool registarArtista(string nome,String imagem, int idAdmin);
        public bool registarGeneroMusical(String nome,int idAdmin );

        //public Dictionary<Leilao, Artista> filtrarLeiloesPorGenero(List<int> idsGenero);

    }
}