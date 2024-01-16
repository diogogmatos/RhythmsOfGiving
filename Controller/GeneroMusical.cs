
using System.Text;

namespace RhythmsOfGiving.Controller
{
    public class GeneroMusical
    {
        private int idGenero;
        private string nome;
        private int idAdmin;
        
        private static int contadorGeneros;
        
        //Construtor
        public GeneroMusical(string nome, int idAdmin)
        {
            this.idGenero = ++contadorGeneros;
            this.nome = nome;
            this.idAdmin = idAdmin;
        }

        public int getIdGenero()
        {
            return this.idGenero;
        }

        public string getNome()
        {
            return this.nome;
        }

        public int getIdAdmin()
        {
            return this.idAdmin;
        }

        public void setIdGenero(int id)
        {
            this.idGenero = id;
        }

        public void setNome(string nome)
        {
            this.nome = nome;
        }

        public void setIdAdmin(int idAdmin)
        {
            this.idAdmin = idAdmin;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("GeneroMusical:: {");
            sb.Append(" Id genero: ").Append(this.idGenero);
            sb.Append(" Nome: ").Append(this.nome);
            sb.Append(" Id admin: ").Append(this.idAdmin).Append(" }");

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
            GeneroMusical other = (GeneroMusical)obj;

            // Verifica a igualdade dos atributos
            return (this.idGenero == other.getIdGenero() &&
                    this.nome.Equals(other.getNome()) &&
                    this.idAdmin == other.getIdAdmin());
        }
        
    }
   

}