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
            licitadores.put(email,l);
        }

        public int ValidarAutenticacao(string email, string palavraPasse)
        {
            Licitador l = this.licitadores.Get(email);

            if (l.GetPalavraPasse().Equals(palavraPasse))
            {
                return l.GetIdLicitador();
            }
            return -1;
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
                        if (licitadores.VerificarUnicoNumeroCc(novoNumeroCc))
                        {
                            l.SetNcc(novoNumeroCc);
            
                        licitadores.put(l.GetEmail(), l);
                          }

            }
            else
            {
                throw new DataNascimentoMenor18("A pessoa deve ter pelo menos 18 anos.");
            }
        }
        
        public void AdicionarLicitacao(int idLicitacao, int idLicitador)
        {
            Licitador l  = this.licitadores.get(idLicitacao); 
            l.GetMinhasLicitacoes().Add(idLicitacao);
            licitadores.put(l.GetEmail(),l);
            throw new NotImplementedException();
        }

        public Notificacao CriarNotificacaoUltrapassada(int idLicitador, string titulo)
        {
            Licitador l = this.licitadores.get(idLicitador); 
            return l.CriarNotificacaoUltrapassada(titulo);
            throw new NotImplementedException();
        }
        
        public Dictionary<int, Notificacao> CriarNotificacaoPerdedora (HashSet<int> idLicitadores, int idLeilao, string titulo, float valor)
        {
            Dictionary<int, Notificacao> notificacoesPerdedoras = new Dictionary<int, Notificacao>();
            
            foreach (int id in idLicitadores)
            {
                Licitador l = this.licitadores.get(id); 
                Notificacao n = l.CriarNotificacaoPerdedora(idLeilao, titulo, valor);
                notificacoesPerdedoras.Add(id, n);
                throw new NotImplementedException();
            }

            return notificacoesPerdedoras;
        }

        public Dictionary<int, Licitacao> saberLeiloesParticipa_Licitacao(int idLicitador)
        {
            Licitador l = this.licitadores.get(idLicitador); // ERRO
            Dictionary<int, Licitacao> resultado = l.saberLeiloesParticipa_Licitacao();
            return resultado;
            throw new NotImplementedException();
        }
        
        public  Notificacao CriarNotificacaoVencedora(int idLicitador, int idLeilao, string titulo, float valor)
        {
            Licitador l = licitadores.get(idLicitador);
            Notificacao notificacao = l.CriarNotificacaoVencedora(idLeilao, titulo, valor);
            return notificacao;
            throw new NotImplementedException();
        }

        public SortedSet<Fatura> MinhasFaturas(int idLicitador)
        {
            Licitador l = this.licitadores.get(idLicitador);
            SortedSet<Fatura> resultado = l.GetFaturas();

            if (resultado.Count == 0)
            {
                throw new SemFaturasException("Não tem faturas disponíveis.");
            }
                
            return resultado;
            throw new NotImplementedException();
        }

        public Dictionary<Licitador, float> LicitadoresTop10()
        {
            Dictionary<Licitador, float> resultado = new Dictionary<Licitador, float>();
            
            foreach (int id in this.licitadores.KeySet())
            {
                Licitador l = this.licitadores.get(id);
                float valorTotal = l.ValorTotalDoado();
                resultado.Add(l, valorTotal);
                throw new NotImplementedException();
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
            //
            Licitador l = this.licitadores.get(idLicitador);
            string nomeLicitador = l.GetNome();
            int nif = l.GetNif();
            //
            Fatura f = new Fatura(dataHoraAtual, idInstituicao, nomeLicitador, nif, idLicitacao, idLicitador);
            l.AdicionarFatura(f);
            throw new NotImplementedException();
        }
        
        public SortedSet<Licitacao> PesquisarLicitacoes (int idLicitador, int idLeilao)
        {
            Licitador l = this.licitadores.get(idLicitador);
            return l.GetLicitacoesLeilao(idLeilao);
            throw new NotImplementedException();
        }
    }
}