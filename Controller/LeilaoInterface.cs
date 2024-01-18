namespace RhythmsOfGiving.Controller;

public class LeilaoInterface
{
    private int id;
    private string artista;
    private string title;
    private string localizacao;
    private string genero;
    private string tipo;
    private DateTime fim;
    private string shortDescricao;
    private string descricao;
    private float valorBase;
    private float valorAtual; // falta adicionar ao construtor, não fiz porque muda o PaginaLeiloes
    private string imagePath;
    private string authorImagePath;
    private bool asCegas;
    private int contador; // falta adicionar ao construtor, não fiz porque muda o PaginaLeiloes
    private List<int> minhasLicitacoes;

    private LicitacaoDAO licitacaoDAO;

    //Alterar para ficar com o valor do maior id dos elilões existentes
    private static int contadorLeiloes = 0;

    public LeilaoInterface(int id, string artista, string title, string localizacao, string genero, string tipo, string fim, string shortDescricao, string descricao, float valorBase, string imagePath, string authorImagePath, bool asCegas)
    {
        this.id = id;
        this.artista = artista;
        this.title = title;
        this.localizacao = localizacao;
        this.genero = genero;
        this.tipo = tipo;
        this.fim = DateTime.Parse(fim);
        this.shortDescricao = shortDescricao;
        this.descricao = descricao;
        this.valorBase = valorBase;
        this.imagePath = imagePath;
        this.authorImagePath = authorImagePath;
        this.asCegas = asCegas;
        this.minhasLicitacoes = new List<int>();
        this.licitacaoDAO = LicitacaoDAO.getInstance();

    }
    
    public LeilaoInterface(string artista, string title, string localizacao, string genero, string tipo, DateTime fim, string shortDescricao, string descricao, float valorBase, string imagePath, string authorImagePath, bool asCegas)
    {
        this.id = ++contadorLeiloes;
        this.artista = artista;
        this.title = title;
        this.localizacao = localizacao;
        this.genero = genero;
        this.tipo = tipo;
        this.fim = fim;
        this.shortDescricao = shortDescricao;
        this.descricao = descricao;
        this.valorBase = valorBase;
        this.imagePath = imagePath;
        this.authorImagePath = authorImagePath;
        this.asCegas = asCegas;
        this.minhasLicitacoes = new List<int>();
        this.licitacaoDAO = LicitacaoDAO.getInstance();

    }

    // Método para adicionar uma licitação à lista
    public void AdicionarMinhaLicitacao(int valor)
    {
        minhasLicitacoes.Add(valor);
    }

        public int getId() {
            return id;
        }

        public string getArtista() {
            return artista;
        }

        public string getTitle() {
            return title;
        }
        public List<int> getMinhasLicitacoes()
        {
        return minhasLicitacoes;
        }

        public string getLocalizacao() {
            return localizacao;
        }

        public string getGenero() {
            return genero;
        }

        public string getTipo() {
            return tipo;
        }

        public DateTime getFim() {
            return fim;
        }

        public string getDescricao() {
            return descricao;
        }

        public string getShortDescricao() {
            return shortDescricao;
        }

        public float getValorBase() {
            return valorBase;
        }

        public float getValorAtual()
        {
            return valorAtual;
        }

        public string getImagePath() {
            return imagePath;
        }

        public string getAuthorImagePath() {
            return authorImagePath;
        }

        public bool getAsCegas() {
            return asCegas;
        }

        public int getContador()
        {
            return contador;
        }

        public void setContador(int m)
        {
            this.contador = m;
        }
}