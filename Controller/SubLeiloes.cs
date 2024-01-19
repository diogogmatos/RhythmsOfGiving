
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RhythmsOfGiving.Controller
{
    public class SubLeiloes : ISubLeiloes
    {

        private LeilaoDAO leilaoDAO;
        private ArtistaDAO artistaDAO;
        private Dictionary<int, GeneroMusical> generos;
        private InstituicaoDAO instituicaoDAO;

        //Construtor
        public SubLeiloes()
        {
            this.leilaoDAO = LeilaoDAO.getInstance();
            this.artistaDAO = ArtistaDAO.getInstance();
            this.preencherGeneros(); // Prencher Map de Generos no leilãoDAO
            this.instituicaoDAO = InstituicaoDAO.getInstance();
            //preencher o map generos
            //ver classe SubServicos no trabalho DSS para ajudar
        }

        private void preencherGeneros()
        {
            this.generos = this.leilaoDAO.preencherGeneros();
        }


        public bool registarArtista(string nome, string imagem, int idAdmin)
        {
            bool existe = artistaDAO.existeArtista(nome);
            if (!existe)
            {
                Artista artista = new Artista(nome, imagem, idAdmin);
                artistaDAO.put(artista.getIdArtista(), artista);
            }

            return existe;

        }


        public bool registarGeneroMusical(string nome, int idAdmin)
        {
            foreach (var generoExistente in generos.Values)
            {
                if (generoExistente.getNome().Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            int size = this.generos.Count;

            GeneroMusical novoGenero = new GeneroMusical(++size, nome, idAdmin);

            generos.Add(novoGenero.getIdGenero(), novoGenero);
            this.leilaoDAO.putGeneroMusical(novoGenero.getIdGenero(), novoGenero);

            return true;
        }

        public bool registarInstituicao(string nome, string descricao, string logoPath, string link, string iban,
            int idAdmin)
        {
            bool existe = instituicaoDAO.existeInstituicao(nome);
            if (!existe)
            {
                Instituicao instituicao = new Instituicao(nome, descricao, logoPath, link, iban, idAdmin);
                instituicaoDAO.put(instituicao.getId(), instituicao);
            }

            return existe;

        }



        public void registarLeilao(float valorBase, DateTime dataHoraFinal, string titulo, string descricao, string imagem, string localizacao, int idArtista, int idGenero, int idAdmin, int tipoLeilao)
        { 
            try
            {
                GeneroMusical g = this.leilaoDAO.getGenero(idGenero);
                Experiencia e = new Experiencia(descricao, imagem, localizacao, idArtista, g);
                if (tipoLeilao == 0)
                {
                    Leilao l = new LeilaoAsCegas(true, valorBase, valorBase, dataHoraFinal, titulo, DateTime.Now,
                        idAdmin, -1, new List<int>(), e);
                    this.leilaoDAO.put(l.IdLeilao, l);
                }
                else
                {
                    Leilao l = new LeilaoIngles(true, valorBase, valorBase, dataHoraFinal, titulo, DateTime.Now,
                        idAdmin, -1, new List<int>(), e);
                    this.leilaoDAO.put(l.IdLeilao, l);
                }
                

            }
            catch (GeneroMusicalNaoExisteException e)
            {

                throw;
            }

        }

        public int GetLicitadorGanhador(int idLeilao)
        {
            try
            {
                Leilao leilao = this.leilaoDAO.get(idLeilao);
                this.leilaoDAO.put(leilao.IdLeilao, leilao);

                return leilao.GetLicitadorGanhador();

            }
            catch (LeilaoNaoExiste ex)
            {

                throw;
            }
        }

        public List<Instituicao> ApresentarInstituicoes()
        {
            List<Instituicao> resultados = new List<Instituicao>();

            foreach (int idInstituicao in instituicaoDAO.keySet())
            {
                Instituicao i = instituicaoDAO.get(idInstituicao);
                if (i != null)
                {
                    resultados.Add(i);
                }
            }

            return resultados;
        }



        public Dictionary<Leilao, Artista> filtrarLeiloesPorGenero(List<int> idsGenero)
        {
            List<Leilao> leiloes = leilaoDAO.obterLeiloesPorIdsGenero(idsGenero);
            Dictionary<Leilao, Artista> resultado = new Dictionary<Leilao, Artista>();

            foreach (var leilao in leiloes)
            {
                Experiencia ex = leilao.Experiencia;
                Artista artista = artistaDAO.get(ex.getIdArtista());

                if (artista != null)
                {
                    resultado.Add(leilao, artista);
                }
            }

            return resultado;
        }

        //Função definida no DAO do leilao
        public Dictionary<Leilao, Artista> consultarLeiloesAtivos()
        {
            Dictionary<Leilao, Artista> resultado = this.leilaoDAO.leiloesAtivos();
            if (resultado.Count == 0)
            {
                throw new LeiloesAtivosNaoExistemException("Não existem leilões ativos neste momento.");
            }

            return resultado;
        }

        public Dictionary<Leilao, Artista> filtrarLeiloesPorArtista(string nome)
        {
            Dictionary<Leilao, Artista> resultado = this.leilaoDAO.filtrarLeiloesPorArtista(nome);
            if (resultado.Count == 0)
            {
                throw new LeiloesArtistaNaoExisteException("De momento, não existem leilões ativos com o artista " +
                                                           nome);
            }

            return resultado;
        }

        public int criarLicitacao(int idLicitador, int idLeilao, float valorLicitacao, float valorMinimo)
        {
            try
            {
                Leilao leilao = this.leilaoDAO.get(idLeilao);
                int idLicitacao = leilao.verificarLicitacao(idLicitador, valorLicitacao, valorMinimo);
                return idLicitacao;
            }
            catch (LeilaoNaoExiste ex)
            {
                throw;
            }
        }

        public string getTituloLeilao(int idLeilao)
        {
            try
            {
                Leilao leilao = this.leilaoDAO.get(idLeilao);
                return (leilao.Titulo);
            }
            catch (LeilaoNaoExiste e)
            {
                throw;
            }
        }

        public Dictionary<Leilao, Licitacao> infoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações)
        {
            Dictionary<Leilao, Licitacao> resultado = new Dictionary<Leilao, Licitacao>();

            foreach (int idLeilao in ultimasLicitações.Keys)
            {
                Leilao leilao = leilaoDAO.get(idLeilao);
                if (leilao.Ativo == false)
                {
                    resultado.Add(leilaoDAO.get(idLeilao), ultimasLicitações[idLeilao]);
                }
            }

            return resultado;
        }


        public float calcularValorMinimo(int idLeilao)
        {
            try
            {
                Leilao leilao = this.leilaoDAO.get(idLeilao);
                return (float)Math.Round(leilao.ValorAtual * 1.01f, 2);
            }
            catch (LeilaoNaoExiste e)
            {
                throw;
            }
        }


        public float getValorFimLeilao(int idLeilao)
        {
            try
            {
                Leilao leilao = this.leilaoDAO.get(idLeilao);
                return (leilao.getValorFim());
            }
            catch (LeilaoNaoExiste e)
            {
                throw;
            }
            catch (NaoExistemLicitacoesException e)
            {
                throw;
            }
            catch (LicitacaoNaoExisteException e)
            {
                throw;
            }
        }


        public HashSet<int> getLicitadoresPerdedores(int idLeilao, int idLicitadorGanhou)
        {
            try
            {
                Leilao leilao = this.leilaoDAO.get(idLeilao);
                HashSet<int> licitadores = leilao.getLicitadores();
                licitadores.Remove(idLicitadorGanhou);
                return licitadores;
            }
            catch (LeilaoNaoExiste e)
            {
                throw;
            }
            catch (NaoExistemLicitacoesException e)
            {
                throw;
            }
            catch (LicitacaoNaoExisteException e)
            {
                throw;
            }
        }


        public Dictionary<Leilao, Licitacao> getLeiloesAtivosInfos(Dictionary<int, Licitacao> leiloesLicitacoes)
        {
            Dictionary<Leilao, Licitacao> resultado = new Dictionary<Leilao, Licitacao>();

            foreach (int idLeilao in leiloesLicitacoes.Keys)
            {
                Leilao leilao = leilaoDAO.get(idLeilao);
                if (leilao.Ativo == true)
                {
                    resultado.Add(leilaoDAO.get(idLeilao), leiloesLicitacoes[idLeilao]);
                }
            }

            return resultado;
        }

        public Dictionary<Instituicao, float> getValoresInstituicoes()
        {
            Dictionary<Instituicao, float> valoresInstituicoes = new Dictionary<Instituicao, float>();

            foreach (int idLeilao in leilaoDAO.keySet())
            {
                Leilao leilao = leilaoDAO.get(idLeilao);

                if (leilao.Ativo == false)
                {
                    if (leilao.IdInstituicao != -1)
                    {
                        Instituicao analisarInstituicao = instituicaoDAO.get(leilao.IdInstituicao);

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


        public void preencherInstituicaoLeilao(int idLeilao, int idInstituicao)
        {
            try
            {
                Leilao l = this.leilaoDAO.get(idLeilao);
                l.setIdInstituicao(idInstituicao);
                this.leilaoDAO.put(l.IdLeilao, l);
            }
            catch (LeilaoNaoExiste e)
            {
                throw;
            }
        }

        public Dictionary<Leilao, Artista> filtrarLeiloesPorTipo(List<int> tipos)
        {
            List<Leilao> leiloes = leilaoDAO.obterLeiloesPorTipo(tipos);
            Dictionary<Leilao, Artista> resultado = new Dictionary<Leilao, Artista>();

            foreach (var leilao in leiloes)
            {
                Experiencia ex = leilao.Experiencia;
                Artista artista = artistaDAO.get(ex.getIdArtista());

                if (artista != null)
                {
                    resultado.Add(leilao, artista);
                }
            }

            return resultado;
        }
        


        public TimeSpan calcularTempoRestante (int idLeilao)
        {
            try
            {
                Leilao l = this.leilaoDAO.get(idLeilao);
                DateTime atual = DateTime.Now;
                DateTime fim = l.DataHoraFinal;
                DateTime contador = l.DataHoraContador;

                if (fim > contador)
                {
                    if (atual < fim)
                    {
                        return (fim - atual);
                    }
                    else
                    {
                        throw new LeilaoJaAcabouException();
                    }
                }
                else
                {
                    if (atual < contador)
                    {
                        return (contador - atual);
                    }
                    else
                    {
                        throw new LeilaoJaAcabouException();
                    }
                }

            }
            catch (LeilaoNaoExiste e)
            {
                throw;
            }
        }

    }
}