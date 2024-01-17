
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
        if (existe)
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

}
}