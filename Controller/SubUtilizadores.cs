namespace RhythmsOfGiving.Controller
{
    public class SubUtilizadores: ISubUtilizadores {
        private LicitadorDao licitadores;
        private  Dictionary<int, Administrador> administradores;


        //Construtor
        public SubUtilizadores()
        {
            this.licitadores = LicitadorDao.GetInstance();
            this.administradores = new Dictionary<int, Administrador>();
            //preencher o map administradores
            //ver classe SubServicos no trabalho DSS para ajudar
        }


        //DUVIDA É preciso alguma verificação dos dados?
        public void RegistarLicitador (string nome, string email, string palavraPasse, int nCc, int nif, DateOnly dataNascimento, int nrCartao)
        {
            DateOnly idadeAdulta = dataNascimento.AddYears(18);
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            if (dataAtual < idadeAdulta)
            {
                throw new DataNascimentoMenor18("O utilizador é menor de idade");
            }

            Licitador l = new Licitador(nome, palavraPasse, dataNascimento, nrCartao, email, nif, nCc);
            licitadores.Put(email,l);
        }

        public bool ValidarAutenticacao(string email, string palavraPasse)
        {
            Licitador l = this.licitadores.Get(email);

            if (l.GetPalavraPasse().Equals(palavraPasse))
            {
                return true;
            }
            return false;
        }

        
        public void AlterarInfosPessoais(string email, string novoNome, DateTime novaDataNascimento, int novoNumeroCc, string novaPalavraPasse)
        {
            Licitador l = licitadores.Get(email);
            l.SetNome(novoNome);
            l.SetPalavraPasse(novaPalavraPasse);


            // Check if the person is at least 18 years old
            if (DateTime.Now.Subtract(novaDataNascimento).TotalDays / 365.25 >= 18)
            {
                l.SetDataNascimento(novaDataNascimento);
                        // Ver se é necessario
                        if (licitadores.VerificarUnicoNumeroCC(novoNumeroCC))
                        {
                            l.setNcc(novoNumeroCC);
            
                        licitadores.put(l.getEmail(), l);
                          }

            }
            else
            {
                throw new DataNascimentoMenor18("A pessoa deve ter pelo menos 18 anos.");
            }
        }

        public void AdicionarLicitacao(int idLicitacao, int idLicitador)
        {
            Licitador l  = this.licitadores.Get(idLicitacao);
            l.GetMinhasLicitacoes().Add(idLicitacao);
            licitadores.Put(l.GetEmail(),l);

        }

        public Notificacao CriarNotificacaoUltrapassada(int idLicitador, string titulo)
        {
            Licitador l = this.licitadores.Get(idLicitador);
            return l.CriarNotificacaoUltrapassada(titulo);
        }
        
        public Dictionary<int, Notificacao> CriarNotificacaoPerdedora (HashSet<int> idLicitadores, int idLeilao, string titulo, float valor)
        {
            Dictionary<int, Notificacao> notificacoesPerdedoras = new Dictionary<int, Notificacao>();
            
            foreach (int id in idLicitadores)
            {
                Licitador l = this.licitadores.Get(id);
                Notificacao n = l.CriarNotificacaoPerdedora(idLeilao, titulo, valor);
                notificacoesPerdedoras.Add(id, n);
            }

            return notificacoesPerdedoras;
        }

        public Dictionary<int, Licitacao> saberLeiloesParticipa_Licitacao(int idLicitador)
        {
            Licitador l = this.licitadores.Get(idLicitador);
            Dictionary<int, Licitacao> resultado = l.saberLeiloesParticipa_Licitacao();
            return resultado;
        }
        
        public  Notificacao CriarNotificacaoVencedora(int idLicitador, int idLeilao, string titulo, float valor)
        {
            Licitador l = licitadores.Get(idLicitador);
            Notificacao notificacao = l.CriarNotificacaoVencedora(idLeilao, titulo, valor);
            return notificacao;
        }

        public SortedSet<Fatura> MinhasFaturas(int idLicitador)
        {
            Licitador l = this.licitadores.Get(idLicitador);
            SortedSet<Fatura> resultado = l.GetFaturas();

            if (resultado.Count == 0)
            {
                throw new SemFaturasException("Não tem faturas disponíveis.");
            }
                
            return resultado;
        }

        public Dictionary<Licitador, float> LicitadoresTop10()
        {
            Dictionary<Licitador, float> resultado = new Dictionary<Licitador, float>();
            
            foreach (int id in this.licitadores.KeySet())
            {
                Licitador l = this.licitadores.Get(id);
                float valorTotal = l.ValorTotalDoado();
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

        public void CriarFatura (int idInstituicao, int idLicitacao, int idLicitador)
        {
            DateTime dataHoraAtual = DateTime.Now;

            Licitador l = this.licitadores.Get(idLicitador);
            string nomeLicitador = l.GetNome();
            int nif = l.GetNif();

            Fatura f = new Fatura(dataHoraAtual, idInstituicao, nomeLicitador, nif, idLicitacao, idLicitador);
            l.AdicionarFatura(f);
        }
        
        public SortedSet<Licitacao> PesquisarLicitacoes (int idLicitador, int idLeilao)
        {
            Licitador l = this.licitadores.Get(idLicitador);
            return l.GetLicitacoesLeilao(idLeilao);
        }
        
    }
}