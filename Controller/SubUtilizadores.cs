
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

        public bool validarAutenticacao(string email, string palavraPasse)
        {
            try
            {
                Licitador l = this.licitadores.get(email);

                if (l.getPalavraPasse().Equals(palavraPasse))
                {
                    return true;
                }

                return false;
            }
            catch (LicitadorNaoExisteException ex)
            {
                throw; // Lança a exceção novamente
            }
        }

    }
}