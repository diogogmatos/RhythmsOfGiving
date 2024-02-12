
using RhythmsOfGiving.Controller.Leiloes;

namespace RhythmsOfGiving.Controller.Utilizadores
{
    public interface ISubUtilizadores
    {
        public int RegistarLicitador (string nome, string email, string palavraPasse, int nCc, int nif, DateOnly dataNascimento, int nrCartao);

        public Dictionary<int, Boolean> ValidarAutenticacao(string email, string palavraPasse);

        public void AlterarInfosPessoais(string email, string novoNome, DateOnly novaDataNascimento, int nrCredito, string novaPalavraPasse);

        public void AdicionarLicitacao(int idLicitacao, int idLicitador);

        public Notificacao CriarNotificacaoUltrapassada(int idLicitador, string titulo, int idLeilao);

        public Dictionary<int, Notificacao> CriarNotificacaoPerdedora(HashSet<int> idLicitadores, int idLeilao,
            string titulo, float valor);

        public Notificacao CriarNotificacaoVencedora(int idLicitador, int idLeilao, string titulo, float valor);

        public Dictionary<int, Licitacao> saberLeiloesParticipa_Licitacao(int idLicitador);

        public SortedSet<Fatura> MinhasFaturas(int idLicitador);

        public Dictionary<Licitador, float> LicitadoresTop10();
        
        public void CriarFatura (int idInstituicao, int idLicitacao, int idLicitador);
        
        public SortedSet<Licitacao> PesquisarLicitacoes (int idLicitador, int idLeilao);

        public Licitacao GetUltimaLicitacaoUtilizador(int idLicitador, int idLeilao);

        public string GetNomeUtilizador(int idLicitador);

        public List<Notificacao> GetNotificacoesUtilizador(int idLicitador);

        public int ConverterEmailparaID(string email);

        public Licitador GetLicitadorById(int idLicitador);

    }
}