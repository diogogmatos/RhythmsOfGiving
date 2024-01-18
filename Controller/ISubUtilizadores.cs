
namespace RhythmsOfGiving.Controller
{
    public interface ISubUtilizadores
    {
        public void registarLicitador (string nome, string email, string palavraPasse, int nCC, int nif, DateOnly dataNascimento, int nrCartao);

        public bool validarAutenticacao(string email, string palavraPasse);

        public void AlterarInfosPessoais(string email, string novoNome, DateTime novaDataNascimento, int novoNumeroCC, string novaPalavraPasse);

        public void AdicionarLicitacao(int idLicitacao, int idLicitador);

        public Notificacao criarNotificacaoUltrapassada(int idLicitador, string titulo);

        public Dictionary<int, Notificacao> criarNotificacaoPerdedora(HashSet<int> idLicitadores, int idLeilao,
            string titulo, float valor);

        public Dictionary<int, Licitacao> saberLeiloesParticipa_Licitacao(int idLicitador);

    }
}