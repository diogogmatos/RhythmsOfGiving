
namespace RhythmsOfGiving.Controller
{
    public interface ISubUtilizadores
    {

        public bool validarAutenticacao(string email, string palavraPasse);

         public bool alterarInfosPessoais(string email,string novoNome, DateTime novaDataNascimento, int novoNumeroCC, string novaPalavraPasse);

          public void AdicionarLicitacao(int idLicitacao, int idLicitador);
    }
}