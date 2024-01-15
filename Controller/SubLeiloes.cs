
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
}
}