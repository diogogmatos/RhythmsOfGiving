
namespace RhythmsOfGiving.Controller{
public class SubUtilizadores: ISubUtilizadores {
    private LicitacaoDAO licitadores;
    private  Dictionary<int, Administrador> administradores;


    //Construtor
    public SubUtilizadores(){
        this.licitadores = LicitacaoDAO.getInstance();
        this.administradores = new Dictionary<int, Administrador>();
        //preencher o map administradores
        //ver classe SubServicos no trabalho DSS para ajudar
    }

    public bool validarAutenticacao(string email, string palavraPasse){
        return false;
    }

}
}