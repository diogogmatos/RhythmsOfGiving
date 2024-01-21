

using System.Text;
using RhythmsOfGiving.Controller.Dados;

namespace RhythmsOfGiving.Controller
{
    public class Experiencia
    {
        private int idArtista;
        private string descricao;
        private string imagem;
        private string localizacao;
        private GeneroMusical generoMusical;
        private ArtistaDao artistaDao;

        public Experiencia(string descricao, string imagem, string localizacao, int idArtista, GeneroMusical generoMusical)
        {
            this.descricao = descricao;
            this.imagem = imagem;
            this.localizacao = localizacao;
            this.idArtista = idArtista;
            this.generoMusical = generoMusical;
            this.artistaDao = ArtistaDao.GetInstance();
        }

        public string GetDescricao()
        {
            return this.descricao;
        }

        public string GetImagem()
        {
            return this.imagem;
        }

        public string GetLocalizacao()
        {
            return this.localizacao;
        }

        public int GetIdArtista()
        {
            return this.idArtista;
        }

        public GeneroMusical GetGeneroMusical()
        {
            return this.generoMusical;
        }

        public void SetDescricao(string descricao)
        {
            this.descricao = descricao;
        }

        public void SetImagem(string imagem)
        {
            this.imagem = imagem;
        }

        public void SetLocalizacao(string localizacao)
        {
            this.localizacao = localizacao;
        }

        public void SetIdArtista(int id)
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
            return (this.idArtista == other.GetIdArtista() &&
                    this.descricao.Equals(other.GetDescricao()) &&
                    this.imagem.Equals(other.GetImagem()) &&
                    this.localizacao.Equals(other.GetLocalizacao()));
        }
    }
}