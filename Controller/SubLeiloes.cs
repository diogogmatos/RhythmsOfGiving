
using System.Runtime.CompilerServices;

namespace RhythmsOfGiving.Controller{
public class SubLeiloes: ISubLeiloes {

    private LeilaoDAO leilaoDAO;
    private ArtistaDAO artistaDAO;
    private Dictionary<int, GeneroMusical> generos;
    private InstituicaoDAO instituicaoDAO;

    //Construtor
    public SubLeiloes(){
        this.leilaoDAO = LeilaoDAO.getInstance();
        this.artistaDAO = ArtistaDAO.getInstance();
        this.generos = new Dictionary<int, GeneroMusical>();
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

        if (!generos.ContainsKey(novoGenero.getIdGenero()))
        {
            generos.Add(novoGenero.getIdGenero(), novoGenero);

            return true;
        }

        return false;
    }

    public int GetLicitadorGanhador(int idLeilao)
    {
        Leilao leilao = this.leilaoDAO.get(idLeilao);
        if(leilao != null ){
        return leilao.GetLicitadorGanhador();
        }
        return -1;
    }

    public List<Instituicao> ApresentarInstituicoes()
    {
        List<Instituicao> resultados = new List<Instituicao>();

        foreach (int idInstituicao in instituicaoDAO.containsKeys())
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
            Artista artista = artistaDAO.get(leilao.getId());

            // Verifica se o gênero do artista está na lista de IDs de gênero desejados
            if (artista != null)
            {
                resultado.Add(leilao, artista);
            }
        }

        return resultado;       
    }
    */

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
            throw new LeiloesArtistaNaoExisteException("De momento, não existem leilões ativos com o artista " + nome);
        }

        return resultado;
    }

}
}