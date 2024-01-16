
using System.Runtime.CompilerServices;

namespace RhythmsOfGiving.Controller{
public class SubLeiloes: ISubLeiloes {

    private LeilaoDAO leilaoDAO;
    private ArtistaDAO artistaDAO;
    private  Dictionary<int, GeneroMusical> generos;
    private InstituicaoDAO instituicaoDAO;

    //Construtor
    public SubLeiloes(){
        this.leilaoDAO = LeilaoDAO.getInstance();
        this.artistaDAO = ArtistaDAO.getInstance();
        this.generos = new Dictionary<int, GeneroMusical>();
        this.instituicaoDAO = InstituicaoDAO.getInstance();
        //preencher o map generos
        //ver classe SubServicos no trabalho DSS para ajudar
    }

    public bool registarArtista(string nome,String imagem, int idAdmin)
    {
        bool existe = artistaDAO.existeArtista(nome);
        if (existe)
        {
            Artista artista = new Artista(nome, imagem, idAdmin);
            artistaDAO.put(artista.getIdArtista(), artista);
        }

        return existe;
        
    }
    public bool registarGeneroMusical(string nome, int idAdmin)
    {
        GeneroMusical novoGenero = new GeneroMusical(nome, idAdmin);

        if (!generos.ContainsKey(novoGenero.getIdGenero()))
        {
            generos.Add(novoGenero.getIdGenero(), novoGenero);

            return true;
        }

        return false;
    }

}
}