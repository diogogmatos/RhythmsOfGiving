

using System.Text;

namespace RhythmsOfGiving.Controller
{
    public class Experiencia
    {
        private int idArtista;
        private string descricao;
        private string imagem;
        private string localizacao;
        private GeneroMusical generoMusical;
        private ArtistaDAO artistaDao;

        public Experiencia(string descricao, string imagem, string localizacao, int idArtista, GeneroMusical generoMusical)
        {
            this.descricao = descricao;
            this.imagem = imagem;
            this.localizacao = localizacao;
            this.idArtista = idArtista;
            this.generoMusical = generoMusical;
            this.artistaDao = ArtistaDAO.getInstance();
        }

        public string getDescricao()
        {
            return this.descricao;
        }

        public string getImagem()
        {
            return this.imagem;
        }

        public string getLocalizacao()
        {
            return this.localizacao;
        }

        public int getIdArtista()
        {
            return this.idArtista;
        }

        public void setDescricao(string descricao)
        {
            this.descricao = descricao;
        }

        public void setImagem(string imagem)
        {
            this.imagem = imagem;
        }

        public void setLocalizacao(string localizacao)
        {
            this.localizacao = localizacao;
        }

        public void setIdArtista(int id)
        {
            this.idArtista = id;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Experiencia:: {");
            sb.Append(" Descrição: ").Append(this.descricao);
            sb.Append(" Localização: ").Append(this.localizacao);
            sb.Append(" Genero Musical: ").Append(this.generoMusical.ToString());
            sb.Append(" Id Artista: ").Append(this.idArtista).Append(" }");

            return sb.ToString();
        } 

        public override bool Equals(object obj){
            // Verifica se o objeto é o próprio objeto
            if (obj == this)
                return true;

            // Verifica se o objeto é nulo ou pertence a uma classe diferente
            if (obj == null || obj.GetType() != this.GetType())
                return false;

            // Faz a conversão segura do objeto para a classe Licitador
            Experiencia other = (Experiencia)obj;

            // Verifica a igualdade dos atributos
            return (this.idArtista == other.getIdArtista() &&
                    this.descricao.Equals(other.getDescricao()) &&
                    this.imagem.Equals(other.getImagem()) &&
                    this.localizacao.Equals(other.getLocalizacao()));
        }
    }
}