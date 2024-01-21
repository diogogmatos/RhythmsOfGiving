using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RhythmsOfGiving.Controller
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


        public bool RegistarArtista(string nome, string imagem, int idAdmin)
        {
            bool existe = artistaDao.ExisteArtista(nome);
            if (!existe)
            {
                Artista artista = new Artista(nome, imagem, idAdmin);
                artistaDao.Put(artista.GetIdArtista(), artista);
            }

            return existe;

        }


        public bool RegistarGeneroMusical(string nome, int idAdmin)
        {
            foreach (var generoExistente in generos.Values)
            {
                if (generoExistente.GetNome().Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            int size = this.generos.Count;

            GeneroMusical novoGenero = new GeneroMusical(++size, nome, idAdmin);

            generos.Add(novoGenero.GetIdGenero(), novoGenero);
            this.leilaoDao.putGeneroMusical(novoGenero.GetIdGenero(), novoGenero);

            return true;
        }

        public bool RegistarInstituicao(string nome, string descricao, string logoPath, string link, string iban,
            int idAdmin)
        {
            bool existe = instituicaoDao.ExisteInstituicao(nome);
            if (!existe)
            {
                Instituicao instituicao = new Instituicao(nome, descricao, logoPath, link, iban, idAdmin);
                instituicaoDao.Put(instituicao.GetId(), instituicao);
            }

            return existe;

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
            this.leilaoDao.put(leilao.IdLeilao, leilao);

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
            int idLicitacao = leilao.VerificarLicitacao(idLicitador, valorLicitacao, valorMinimo);
            return idLicitacao;
        }

        public string GetTituloLeilao(int idLeilao)
        {
            Leilao leilao = this.leilaoDao.get(idLeilao);
            return (leilao.Titulo);
        }

        public Dictionary<Leilao, Licitacao> InfoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações)
        {
            Dictionary<Leilao, Licitacao> resultado = new Dictionary<Leilao, Licitacao>();

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
            Dictionary<Instituicao, float> valoresInstituicoes = new Dictionary<Instituicao, float>();

            foreach (int idLeilao in leilaoDao.keySet())
            {
                Leilao leilao = leilaoDao.get(idLeilao);

                if (leilao.Ativo == false)
                {
                    if (leilao.IdInstituicao != -1)
                    {
                        Instituicao analisarInstituicao = instituicaoDao.Get(leilao.IdInstituicao);

                        if (valoresInstituicoes.ContainsKey(analisarInstituicao))
                        {
                            valoresInstituicoes[analisarInstituicao] += leilao.ValorAtual;
                        }
                        else
                        {
                            valoresInstituicoes[analisarInstituicao] = leilao.ValorAtual;
                        }
                    }
                }
            }

            return valoresInstituicoes;
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

        public List<string> GetNomesGenerosMusicais()
        {
            List<string> nomes = new List<string>();

            foreach (GeneroMusical genero in generos.Values)
            {
                string nomeDoGenero = genero.GetNome();
                nomes.Add(nomeDoGenero);
            }

            return nomes;
        }

        public List<string> GetNomesArtistasMusicais()
        {
            
            List<string> nomes = new List<string>();

            foreach (int idartista in artistaDao.keySet())
            {
                Artista artista = this.artistaDao.Get(idartista);
                string nome = artista.GetNome();
                nomes.Add(nome);
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

    }
}
