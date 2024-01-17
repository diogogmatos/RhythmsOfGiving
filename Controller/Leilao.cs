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
    private float valorAtual; // falta adicionar ao construtor, não fiz porque muda o PaginaLeiloes
    private string imagePath;
    private string authorImagePath;
    private bool asCegas;
    private int contador; // falta adicionar ao construtor, não fiz porque muda o PaginaLeiloes
    private List<int> minhasLicitacoes;

    private LicitacaoDAO licitacaoDAO;

    //Alterar para ficar com o valor do maior id dos elilões existentes
    private static int contadorLeiloes = 0;

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
    
    public Leilao(string artista, string title, string localizacao, string genero, string tipo, DateTime fim, string shortDescricao, string descricao, float valorBase, string imagePath, string authorImagePath, bool asCegas)
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

    public int criarLicitacao(float valorLicitacao, int idLicitador)
    {
        Licitacao novaLicitacao = new Licitacao(DateTime.Now, valorLicitacao, this.id, idLicitador);
        this.minhasLicitacoes.Add(novaLicitacao.GetIdLicitacao());
        this.licitacaoDAO.put(novaLicitacao.GetIdLicitacao(), novaLicitacao);
        return novaLicitacao.GetIdLicitacao();
    }

    //Verifiquem se está certo
    public int verificarLicitacao(int idLicitador, float valorLicitacao, float valorMinimo)
    {
        bool contadorBloqueado = false;
        //Verificar se dentro do tempo
        DateTime atual = DateTime.Now; 
        if (atual < this.fim)
        {
            if (valorLicitacao >= valorMinimo)
            {
                //Calcular a diferença entre a atual e o tempo final
                TimeSpan diferenca = this.fim - atual;
                if (diferenca.TotalMinutes > 5)
                {
                    //criar a licitação
                    return this.criarLicitacao(valorLicitacao, idLicitador);
                }
                else
                {
                    //Acrescentar no contador
                    this.contador = 5; // DUVIDA: 5 + TEMPO QUE FALTA PARA TERMINAR
                    //criar a licitação
                    return this.criarLicitacao(valorLicitacao, idLicitador);
                }
            }

            throw new ValorLicitacaoException("O valor introduzido é inválido.\n" +
                                              "O valor mínimo da licitação é de: " + valorMinimo);

        }
        else
        {
            if(this.contador == 0)
                throw new TempoExcedidoException("O tempo do leilão " + this.id + " foi excedido.");
            else
            {
                TimeSpan diferenca = atual - this.fim;
                if (diferenca.TotalHours < 24 && diferenca.TotalMinutes < (24 * 60 - 5))
                {
                    if (contadorBloqueado == false)
                    {
                        DateTime dataAnterior = atual.AddHours(-24);
                        TimeSpan diferenca2 = this.fim - dataAnterior;
                        this.contador = Convert.ToInt32(diferenca2.TotalMinutes);
                        contadorBloqueado = true;
                    }
                    
                    if(valorLicitacao >= valorMinimo){
                        //criar a licitação
                        return this.criarLicitacao(valorLicitacao, idLicitador);
                    }

                    throw new ValorLicitacaoException("O valor introduzido é inválido.\n" +
                                                      "O valor mínimo da licitação é de: " + valorMinimo);

                }
                else
                {
                    this.contador = 5;
                    if(valorLicitacao >= valorMinimo){
                        //criar a licitação
                        return this.criarLicitacao(valorLicitacao, idLicitador);
                    }

                    throw new ValorLicitacaoException("O valor introduzido é inválido.\n" +
                                                      "O valor mínimo da licitação é de: " + valorMinimo);
                }
            }
        }
    }
}
}