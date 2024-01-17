namespace RhythmsOfGiving.Controller {
    public class Leilao {
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

        public Leilao(int id, string artista, string title, string localizacao, string genero, string tipo, string fim, string shortDescricao, string descricao, float valorBase, string imagePath, string authorImagePath, bool asCegas) {
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
        }
        
        //Para a base de dados
        public Leilao(int id, string artista, string title, string localizacao, string genero, string tipo, DateTime fim, string shortDescricao, string descricao, float valorBase, string imagePath, string authorImagePath, bool asCegas) {
            this.id = id;
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
    }
}