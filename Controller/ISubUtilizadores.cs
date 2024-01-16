
namespace RhythmsOfGiving.Controller
{
    public interface ISubUtilizadores
    {

        public bool validarAutenticacao(string email, string palavraPasse);

         public bool AlterarInfosPessoais(string email,string novoNome, DateTime novaDataNascimento, int novoNumeroCC, string novaPalavraPasse);
    }
}