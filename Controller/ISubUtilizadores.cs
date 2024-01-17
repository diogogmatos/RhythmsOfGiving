
namespace RhythmsOfGiving.Controller
{
    public interface ISubUtilizadores
    {
        public void registarLicitador (string nome, string email, string palavraPasse, int nCC, int nif, DateOnly dataNascimento, int nrCartao);

        public bool validarAutenticacao(string email, string palavraPasse);

        public bool alterarInfosPessoais(string email,string novoNome, DateTime novaDataNascimento, int novoNumeroCC, string novaPalavraPasse);

        public void AdicionarLicitacao(int idLicitacao, int idLicitador);

    }
}