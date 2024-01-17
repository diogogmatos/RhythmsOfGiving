namespace RhythmsOfGiving.Controller {
    public class Leilao
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
    private string imagePath;
    private string authorImagePath;
    private bool asCegas;
    private List<int> minhasLicitacoes;

    private LicitacaoDAO licitacaoDAO;


    public Leilao(int id, string artista, string title, string localizacao, string genero, string tipo, string fim, string shortDescricao, string descricao, float valorBase, string imagePath, string authorImagePath, bool asCegas)
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

        public string getImagePath() {
            return imagePath;
        }

        public string getAuthorImagePath() {
            return authorImagePath;
        }

        public bool getAsCegas() {
            return asCegas;
        }
    public int GetLicitadorGanhador()
    {
        int idLicitadorVencedor = -1;

        if (this.fim != null)
        {
            List<int> licitacoes = this.minhasLicitacoes;
            float valorMaior = 0;

            foreach (int idLicitacao in licitacoes)
            {
                Licitacao l = licitacaoDAO.get(idLicitacao);

                if (l != null)
                {
                    float valorAtual = l.GetValor();

                    if (valorAtual > valorMaior)
                    {
                        valorMaior = valorAtual;
                        idLicitadorVencedor = l.GetIdLicitador();
                    }
                }
            }
        }

        return idLicitadorVencedor;
    }
    }
}