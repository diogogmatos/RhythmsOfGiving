

namespace RhythmsOfGiving.Controller
{
    public interface ISubLeiloes
    {
        public bool registarArtista(string nome, byte[] imagem, int idAdmin);

        public bool registarGeneroMusical(String nome,int idAdmin );
    }
}