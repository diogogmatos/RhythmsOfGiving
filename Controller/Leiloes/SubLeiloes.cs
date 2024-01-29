using RhythmsOfGiving.Controller.Dados;
using RhythmsOfGiving.Controller.Excecoes;

namespace RhythmsOfGiving.Controller.Leiloes
{
    public class SubLeiloes : ISubLeiloes
    {

        private LeilaoDAO leilaoDao;
        private ArtistaDao artistaDao;
        private Dictionary<int, GeneroMusical> generos;
        private InstituicaoDao instituicaoDao;

        //Construtor
        public SubLeiloes()
        {
            this.leilaoDao = LeilaoDAO.getInstance();
            this.artistaDao = ArtistaDao.GetInstance();
            this.PreencherGeneros(); // Prencher Map de Generos no leilãoDAO
            this.instituicaoDao = InstituicaoDao.GetInstance();
            //preencher o map generos
            //ver classe SubServicos no trabalho DSS para ajudar
        }

        private void PreencherGeneros()
        {
            this.generos = this.leilaoDao.preencherGeneros();
        }


        public void RegistarArtista(string nome, string imagem, int idAdmin)
        {
            bool existe = artistaDao.ExisteArtista(nome);
            if (!existe)
            {
                Artista artista = new Artista(nome, imagem, idAdmin);
                artistaDao.Put(artista.GetIdArtista(), artista);
                return;
            }

            throw new ArtistaJaExisteException("O artista com o nome " + nome + " já existe");

        }


        public void RegistarGeneroMusical(string nome, int idAdmin)
        {
            foreach (var generoExistente in generos.Values)
            {
                if (generoExistente.GetNome().Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    throw new GeneroMusicalJaExisteException("O genero musical com o nome " + nome + " já existe");
                }
            }

            int size = this.generos.Count;

            GeneroMusical novoGenero = new GeneroMusical(++size, nome, idAdmin);

            generos.Add(novoGenero.GetIdGenero(), novoGenero);
            this.leilaoDao.putGeneroMusical(novoGenero.GetIdGenero(), novoGenero);
            
        }

        public void RegistarInstituicao(string nome, string descricao, string logoPath, string link, string iban,
            int idAdmin)
        {
            bool existe = instituicaoDao.ExisteInstituicao(nome);
            if (!existe)
            {
                Instituicao instituicao = new Instituicao(nome, descricao, logoPath, link, iban, idAdmin);
                instituicaoDao.Put(instituicao.GetId(), instituicao);
                return;
            }

            throw new InstituicaoJaExisteException("A instituição com o nome " + nome + " já existe");

        }



        public void RegistarLeilao(float valorBase, DateTime dataHoraFinal, string titulo, string descricao, string imagem, string localizacao, int idArtista, int idGenero, int idAdmin, int tipoLeilao)
        {
            GeneroMusical g = this.leilaoDao.getGenero(idGenero);
            Experiencia e = new Experiencia(descricao, imagem, localizacao, idArtista, g);
            if (tipoLeilao == 0)
            {
                Leilao l = new LeilaoAsCegas(true, valorBase, valorBase, dataHoraFinal, titulo, DateTime.Now,
                    idAdmin, -1, new List<int>(), e);
                this.leilaoDao.put(l.IdLeilao, l);
            }
            else
            {
                Leilao l = new LeilaoIngles(true, valorBase, valorBase, dataHoraFinal, titulo, DateTime.Now,
                    idAdmin, -1, new List<int>(), e);
                this.leilaoDao.put(l.IdLeilao, l);
            }
        }

        public int GetLicitadorGanhador(int idLeilao)
        {
            Leilao leilao = this.leilaoDao.get(idLeilao);

            return leilao.GetLicitadorGanhador();
        }

        public List<Instituicao> ApresentarInstituicoes()
        {
            List<Instituicao> resultados = new List<Instituicao>();

            foreach (int idInstituicao in instituicaoDao.KeySet())
            {
                Instituicao i = instituicaoDao.Get(idInstituicao);
                if (i != null)
                {
                    resultados.Add(i);
                }
            }

            return resultados;
        }



       /* public Dictionary<Leilao, Artista> FiltrarLeiloesPorGenero(List<int> idsGenero)
        {
            List<Leilao> leiloes = leilaoDao.obterLeiloesPorIdsGenero(idsGenero);
            Dictionary<Leilao, Artista> resultado = new Dictionary<Leilao, Artista>();

            foreach (var leilao in leiloes)
            {
                Experiencia ex = leilao.Experiencia;
                Artista artista = artistaDao.Get(ex.GetIdArtista());

                if (artista != null)
                {
                    resultado.Add(leilao, artista);
                }
            }
            return resultado;
        }*/

        //Função definida no DAO do leilao
        public Dictionary<Leilao, Artista> ConsultarLeiloesAtivos()
        {
            Dictionary<Leilao, Artista> resultado = this.leilaoDao.leiloesAtivos();
            if (resultado.Count == 0)
            {
                throw new LeiloesAtivosNaoExistemException("Não existem leilões ativos neste momento.");
            }

            return resultado;
        }

        public Dictionary<Leilao, Artista> FiltrarLeiloesPorArtista(string nome)
        {
            Dictionary<Leilao, Artista> resultado = this.leilaoDao.filtrarLeiloesPorArtista(nome);
            if (resultado.Count == 0)
            {
                throw new LeiloesArtistaNaoExisteException("De momento, não existem leilões ativos com o artista " +
                                                           nome);
            }

            return resultado;
        }

        public int CriarLicitacao(int idLicitador, int idLeilao, float valorLicitacao, float valorMinimo)
        {
            Leilao leilao = this.leilaoDao.get(idLeilao);
            try
            {
                int idLicitacao = leilao.VerificarLicitacao(idLicitador, valorLicitacao, valorMinimo);

                if (leilao.GetTipo() == 1 || (leilao.GetTipo() == 0 && valorLicitacao >= leilao.ValorAtual))
                {
                    leilao.SetValorAtual(valorLicitacao);
                }
                this.leilaoDao.put(leilao.IdLeilao, leilao);

                return idLicitacao;
            }
            catch (ValorLicitacaoException ex)
            {
                throw;
            }
            catch (TempoExcedidoException)
            {
                throw;
            }
        }

        public string GetTituloLeilao(int idLeilao)
        {
            Leilao leilao = this.leilaoDao.get(idLeilao);
            return (leilao.Titulo);
        }

        public SortedDictionary<Leilao, Licitacao> InfoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações)
        {
            SortedDictionary<Leilao, Licitacao> resultado = new SortedDictionary<Leilao, Licitacao>();

            foreach (int idLeilao in ultimasLicitações.Keys)
            {
                Leilao leilao = leilaoDao.get(idLeilao);
                if (leilao.Ativo == false)
                {
                    resultado.Add(leilaoDao.get(idLeilao), ultimasLicitações[idLeilao]);
                }
            }

            return resultado;
        }


        public float CalcularValorMinimo(int idLeilao)
        {
            Leilao leilao = this.leilaoDao.get(idLeilao);
            return (float)Math.Round(leilao.ValorAtual * 1.01f, 2);
        }


        public float GetValorFimLeilao(int idLeilao)
        {
            Leilao leilao = this.leilaoDao.get(idLeilao);
            return (leilao.GetValorUltimaLicitacao());
        }


        public HashSet<int> GetLicitadoresPerdedores(int idLeilao, int idLicitadorGanhou)
        {
            Leilao leilao = this.leilaoDao.get(idLeilao);
            HashSet<int> licitadores = leilao.GetLicitadores();
            licitadores.Remove(idLicitadorGanhou);
            return licitadores;
        }

        public Dictionary<Leilao, Licitacao> GetLeiloesAtivosInfos(Dictionary<int, Licitacao> leiloesLicitacoes)
        {
            Dictionary<Leilao, Licitacao> resultado = new Dictionary<Leilao, Licitacao>();

            foreach (int idLeilao in leiloesLicitacoes.Keys)
            {
                Leilao leilao = leilaoDao.get(idLeilao);
                if (leilao.Ativo == true)
                {
                    resultado.Add(leilaoDao.get(idLeilao), leiloesLicitacoes[idLeilao]);
                }
            }

            return resultado;
        }

        public Dictionary<Instituicao, float> GetValoresInstituicoes()
        {
            Dictionary<int, float> valoresid = new Dictionary<int, float>();
            Dictionary<Instituicao, float> valoresInstituicoes = new Dictionary<Instituicao, float>();

            HashSet<int> set = leilaoDao.keySet();
            foreach (int idLeilao in set)
            {
                
                Leilao leilao = leilaoDao.get(idLeilao);

                if (leilao.Ativo == false)
                {
                    Console.WriteLine("Adiconar a um existente" + leilao);

                    
                    if (leilao.IdInstituicao != -1)
                    {
                        Instituicao analisarInstituicao = instituicaoDao.Get(leilao.IdInstituicao);
                        if (valoresid.ContainsKey(analisarInstituicao.GetId()))
                        {
                            valoresid[analisarInstituicao.GetId()] += leilao.ValorAtual;
                            
                        }
                        else
                        {
                            valoresid[analisarInstituicao.GetId()] = leilao.ValorAtual;
                        }
                    }
                }
            }

            foreach (var var in valoresid)
            {
                valoresInstituicoes.Add( instituicaoDao.Get(var.Key),var.Value);
            }
            
            Console.WriteLine(valoresInstituicoes);
            // Se estiver a dar erro é aqui:
            if (valoresInstituicoes.Any())
            {
                var listaOrdenada = valoresInstituicoes.OrderByDescending(x => x.Value).ToList();
                Dictionary<Instituicao, float> 
                    dicionarioOrdenado = listaOrdenada.ToDictionary(x => x.Key, x => x.Value);
                return dicionarioOrdenado;

            }

            throw new InstituicoesSemDoacoes(
                "De momento, não existem instituições que recebem doações. Seja o primeiro!");

        }


        public void PreencherInstituicaoLeilao(int idLeilao, int idInstituicao)
        {
            Leilao l = this.leilaoDao.get(idLeilao);
            l.SetIdInstituicao(idInstituicao);
            this.leilaoDao.put(l.IdLeilao, l);
        }
    /*
        public Dictionary<Leilao, Artista> FiltrarLeiloesPorTipo(List<int> tipos)
        {
            List<Leilao> leiloes = leilaoDao.ObterLeiloesPorTipo(tipos);
            Dictionary<Leilao, Artista> resultado = new Dictionary<Leilao, Artista>();

            foreach (var leilao in leiloes)
            {
                Experiencia ex = leilao.Experiencia;
                Artista artista = artistaDao.Get(ex.GetIdArtista());

                if (artista != null)
                {
                    resultado.Add(leilao, artista);
                }
            }

            return resultado;
        }*/
        


        public TimeSpan CalcularTempoRestante (int idLeilao)
        {
            Leilao l = this.leilaoDao.get(idLeilao);
            DateTime atual = DateTime.Now;
            DateTime fim = l.DataHoraFinal;
            DateTime contador = l.DataHoraContador;

            if (fim > contador)
            {
                if (atual < fim)
                {
                    return (fim - atual);
                }

                throw new LeilaoJaAcabouException();
            }

            if (atual < contador)
            {
                return (contador - atual);
            }

            throw new LeilaoJaAcabouException();
        }

        public Licitacao ProcurarLicitacaoAtual(int idLeilao)
        {
            Leilao l = this.leilaoDao.get(idLeilao);
            return (l.LicitacaoAtualAnterior());
        }

        public float GetTotalValorDoado()
        {
            float valor = 0;

            foreach (int idLeilao in leilaoDao.keySet())
            {
                Leilao leilao = leilaoDao.get(idLeilao);

                if (leilao.Ativo == false)
                {
                    if (leilao.IdInstituicao != -1) //A instituição pode ainda não ter sido escolhida
                    {
                        valor += leilao.ValorAtual;
                    }
                }
            }

            if (valor == 0)
            {
                throw new LeiloesSemLicitacoes("De momento, não existem doações no sistema");
            }
            
            return valor;
        }

        public Leilao GetLeilaoById(int id){
            return leilaoDao.get(id);
        }

        public Dictionary<Leilao, Artista> GetLeilaoArtistaById(int idLeilao)
        {
            Leilao l = this.leilaoDao.get(idLeilao);
            int idArtista = l.Experiencia.GetIdArtista();
            Artista a = this.artistaDao.Get(idArtista);
            Dictionary<Leilao, Artista> leilaoArtista = new Dictionary<Leilao, Artista>();
            leilaoArtista.Add(l,a);
            return leilaoArtista;
        }

        public Dictionary<int, string> GetNomesGenerosMusicais()
        {
            Dictionary<int, string> nomes = new Dictionary<int, string>();

            foreach (GeneroMusical genero in generos.Values)
            {
                int idGenero = genero.GetIdGenero();
                string nomeDoGenero = genero.GetNome();
                nomes.Add(idGenero, nomeDoGenero);
            }

            return nomes;
        }


        public Dictionary<int, string> GetNomesArtistasMusicais()
        {
            Dictionary<int, string> nomes = new Dictionary<int, string>();

            foreach (int idArtista in artistaDao.keySet())
            {
                Artista artista = this.artistaDao.Get(idArtista);
                string nome = artista.GetNome();
                nomes.Add(artista.GetIdArtista(), nome);
            }

            return nomes;
        }


        public List<Leilao> LeiloesAtivosParaTerminados()
        {
            Dictionary<Leilao, Artista> leiloesArtistaAtivos = this.ConsultarLeiloesAtivos();
            DateTime atual = DateTime.Now;
            List<Leilao> leiloesTerminados = new List<Leilao>();

            foreach (Leilao l in leiloesArtistaAtivos.Keys)
            {
                if (l.DataHoraFinal < atual && l.DataHoraContador < atual)
                {
                    l.SetAtivo(false);
                    this.leilaoDao.put(l.IdLeilao, l);
                    leiloesTerminados.Add(l);
                }
            }

            return leiloesTerminados;
        }

        public Leilao DesativarLeilao(int idLeilao)
        {
            Leilao l = this.leilaoDao.get(idLeilao);
            l.SetAtivo(false);
            this.leilaoDao.put(l.IdLeilao, l);
            return l;
        }

        public Licitacao GetUltimaLicitacao(int idLeilao)
        {
            Leilao l = this.leilaoDao.get(idLeilao);
            return l.GetUltimaLicitacao();
        }

        public string GetInstituicaoById(int idInstituicao)
        {
            Instituicao i = this.instituicaoDao.Get(idInstituicao);
            return i.GetNome();
        }

    }
}
