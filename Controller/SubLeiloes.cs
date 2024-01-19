
/*
using System.Runtime.CompilerServices;

namespace RhythmsOfGiving.Controller{
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
            this.generos = new Dictionary<int, GeneroMusical>(); // Prencher Map de Generos no leilãoDAO
            this.instituicaoDAO = InstituicaoDAO.getInstance();
            //preencher o map generos
            //ver classe SubServicos no trabalho DSS para ajudar
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
            
            GeneroMusical novoGenero = new GeneroMusical(nome, idAdmin);
            // Mudar a condição
            if (!generos.ContainsKey(novoGenero.getIdGenero()))
            {
                
                generos.Add(novoGenero.getIdGenero(), novoGenero);
                // Aqui fazer put
                

                return true;
            }

            return false;
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
        

        public void registarLeilao(float valorBase, DateTime dataHoraFinal, string titulo, string descricao, string imagem, string localizacao, int idArtista, int idGenero, int idAdmin)
        {
            try
            {
                leilaoDAO.put(l.getId(), l);
            }
            catch (DadosInvalidosException ex)
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

/*

    public Dictionary<Leilao, Artista> filtrarLeiloesPorGenero(List<int> idsGenero){
        // Vou fazer esta função no DAOS
        //  List<Leilao> leiloes = leilaoDAO.ObterLeiloes(idsGenero);

        Dictionary<Leilao, Artista> resultado = new Dictionary<Leilao, Artista>();

        // Substitua o código abaixo conforme a lógica específica da sua aplicação
        foreach (var leilao in leiloes)
        {
            // Suponha que há um método ObterArtistaPorId que retorna o Artista correspondente ao idArtista do Leilao
            Artista artista = artistaDAO.get(leilao.getIdArtista());

            // Verifica se o gênero do artista está na lista de IDs de gênero desejados
            if (artista != null)
            {
                resultado.Add(leilao, artista);
            }
        }

        return resultado;
    }
    #1#

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
                return (leilao.getTitle());
            }
            catch (LeilaoNaoExiste e)
            {
                throw;
            }
        }

        public Dictionary<Leilao, Licitacao> infoLeiloesLicitacoes(Dictionary<int, Licitacao> ultimasLicitações)
        {
            Dictionary<Leilao, Licitacao> resultado = new Dictionary<Leilao, Licitacao>();

            //Tens que filtrar pelos leiloes que já estão terminados
            // No map que recebes tens que verficar so o leilão está terminado
            foreach (int idLeilao in ultimasLicitações.Keys)
            {
                resultado.Add(leilaoDAO.get(idLeilao), ultimasLicitações[idLeilao]);
            }

            return resultado;
        }


        public float calcularValorMinimo(int idLeilao)
        {
            try
            {
                Leilao leilao = this.leilaoDAO.get(idLeilao);
                return (float)Math.Round(leilao.getValorAtual() * 1.01f, 2);
            }
            catch (LeilaoNaoExiste e)
            {
                throw;
            }
        }


        public float getValorFimLeilao (int idLeilao)
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

    }

}*/