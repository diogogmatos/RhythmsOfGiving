
using System.Reflection.Metadata;

namespace RhythmsOfGiving.Controller{
    public class SubUtilizadores: ISubUtilizadores {
        private LicitadorDAO licitadores;
        private  Dictionary<int, Administrador> administradores;


        //Construtor
        public SubUtilizadores()
        {
            this.licitadores = LicitadorDAO.getInstance();
            this.administradores = new Dictionary<int, Administrador>();
            //preencher o map administradores
            //ver classe SubServicos no trabalho DSS para ajudar
        }


        //DUVIDA É preciso alguma verificação dos dados?
        public void registarLicitador (string nome, string email, string palavraPasse, int nCC, int nif, DateOnly dataNascimento, int nrCartao)
        {
            DateOnly idade_adulta = dataNascimento.AddYears(18);
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            if (dataAtual < idade_adulta)
            {
                throw new DataNascimentoMenor18("O utilizador é menor de idade");
            }
            
            try
            {
                Licitador l = new Licitador(nome, palavraPasse, dataNascimento, nrCartao, email, nif, nCC);
                licitadores.put(email,l);
            }
            catch (DadosInvalidosException ex)
            {
                throw;
            }
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
                throw;
            }
        }

        
        public void AlterarInfosPessoais(string email, string novoNome, DateTime novaDataNascimento, int novoNumeroCC, string novaPalavraPasse)
        {
            try
            {
                Licitador l = licitadores.get(email);
                l.setNome(novoNome);
                l.setPalavraPasse(novaPalavraPasse);


                    // Check if the person is at least 18 years old
                    if (DateTime.Now.Subtract(novaDataNascimento).TotalDays / 365.25 >= 18)
                    {
                        l.setDataNascimento(novaDataNascimento);

                        // Ver se é necessario
                        if (licitadores.VerificarUnicoNumeroCC(novoNumeroCC))
                        {
                            l.setNcc(novoNumeroCC);
                        }
                        licitadores.put(l.getEmail(), l);

                    }
                    else
                    {
                        throw new DataNascimentoMenor18("A pessoa deve ter pelo menos 18 anos.");
                    }
            }
            catch (LicitadorNaoExisteException ex)
            {
                throw;
            }
        }

        public void AdicionarLicitacao(int idLicitacao, int idLicitador)
        {
            Licitador l  = this.licitadores.get(idLicitacao);
            l.getMinhasLicitacoes().Add(idLicitacao);
            licitadores.put(l.getEmail(),l);

        }

        public Notificacao criarNotificacaoUltrapassada(int idLicitador, string titulo)
        {
            try
            {
                Licitador l = this.licitadores.get(idLicitador);
                return l.criarNotificacaoUltrapassada(titulo);
            }
            catch (LicitadorNaoExisteException e)
            {
                throw;
            }
            
        }
        
        public Dictionary<int, Notificacao> criarNotificacaoPerdedora (HashSet<int> idLicitadores, int idLeilao, string titulo, float valor)
        {
            Dictionary<int, Notificacao> notificacoesPerdedoras = new Dictionary<int, Notificacao>();
            
            foreach (int id in idLicitadores)
            {
                try
                {
                    Licitador l = this.licitadores.get(id);
                    Notificacao n = l.criarNotificacaoPerdedora(idLeilao, titulo, valor);
                    notificacoesPerdedoras.Add(id, n);
                }
                catch (LicitadorNaoExisteException e)
                {
                    throw;
                }
            }

            return notificacoesPerdedoras;
        }

        public Dictionary<int, Licitacao> saberLeiloesParticipa_Licitacao(int idLicitador)
        {
            try
            {
                Licitador l = this.licitadores.get(idLicitador);
                Dictionary<int, Licitacao> resultado = l.saberLeiloesParticipa_Licitacao();
                return resultado;
            }
            catch (LicitadorNaoExisteException e)
            {
                throw;
            }
        }
        
        public  Notificacao criarNotificacaoVencedora(int idLicitador, int idLeilao, string titulo, float valor)
        {
            try
            {
                Licitador l = licitadores.get(idLicitador);
                Notificacao notificacao = l.criarNotificacaoVencedora(idLeilao, titulo, valor);
                return notificacao;
            }
            catch (LicitadorNaoExisteException e)
            {
                throw;
            }
        }

        public SortedSet<Fatura> minhasFaturas(int idLicitador)
        {
            try
            {
                Licitador l = this.licitadores.get(idLicitador);
                SortedSet<Fatura> resultado = l.getFaturas();

                if (resultado.Count == 0)
                {
                    throw new SemFaturasException("Não tem faturas disponíveis.");
                }
                
                return resultado;
            }
            catch (LicitadorNaoExisteException e)
            {
                throw;
            }
        }

        public Dictionary<Licitador, float> licitadoresTop10()
        {
            Dictionary<Licitador, float> resultado = new Dictionary<Licitador, float>();
            
            foreach (int id in this.licitadores.keySet())
            {
                Licitador l = this.licitadores.get(id);
                float valorTotal = l.valorTotalDoado();
                resultado.Add(l, valorTotal);

            }

            if (resultado.Count == 0)
                throw new NaoExistemLicitadoresDoacoesException("Não existem licitadores que fizeram doações.");
            
            // Ordenar o dicionário por valores em ordem decrescente
            Dictionary<Licitador, float> resultadoOrdenado = resultado
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);

            Dictionary<Licitador, float> top10 = new Dictionary<Licitador, float>();
            int tamanho = Math.Min(resultadoOrdenado.Count, 10);

            for (int i = 0; i < tamanho; i++)
            {
                var elemento = resultadoOrdenado.ElementAt(i);
                top10.Add(elemento.Key, elemento.Value);
            }

            return top10;
        }

        public void criarFatura (int idInstituicao, int idLicitacao, int idLicitador)
        {
            DateTime dataHoraAtual = DateTime.Now;

            try
            {
                Licitador l = this.licitadores.get(idLicitador);
                string nomeLicitador = l.getNome();
                int nif = l.getNIF();
                
                try
                {
                    Fatura f = new Fatura(dataHoraAtual, idInstituicao, nomeLicitador, nif, idLicitacao, idLicitador);
                    l.adicionarFatura(f);
                }
                catch (DadosInvalidosException e)
                {
                    throw;
                }
            }
            catch (LicitadorNaoExisteException e)
            {
                throw;
            }
            
        }
        
        public SortedSet<Licitacao> pesquisarLicitacoes (int idLicitador, int idLeilao)
        {
            try
            {
                Licitador l = this.licitadores.get(idLicitador);
                return l.getLicitacoesLeilao(int idLeilao);
            }
            catch (LicitadorNaoExisteException e)
            {
                throw;
            }
        }
        
    }
}