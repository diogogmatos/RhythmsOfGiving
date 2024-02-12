
using System.Text;

namespace RhythmsOfGiving.Controller.Leiloes
{
    public class GeneroMusical
    {
        private int idGenero;
        private string nome;
        private int idAdmin;
        
        private static int _contadorGeneros = 0;
        
        //Construtor
        public GeneroMusical(string nome, int idAdmin)
        {
            this.idGenero = ++_contadorGeneros;
            this.nome = nome;
            this.idAdmin = idAdmin;
        }

        public GeneroMusical(int idGenero, string nome, int idAdmin)
        {
            this.idGenero = idGenero;
            this.nome = nome;
            this.idAdmin = idAdmin;
        }

        public int GetIdGenero()
        {
            return this.idGenero;
        }

        public string GetNome()
        {
            return this.nome;
        }

        public int GetIdAdmin()
        {
            return this.idAdmin;
        }

        public void SetIdGenero(int id)
        {
            this.idGenero = id;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public void SetIdAdmin(int idAdmin)
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
            return (this.idGenero == other.GetIdGenero() &&
                    this.nome.Equals(other.GetNome()) &&
                    this.idAdmin == other.GetIdAdmin());
        }
        
    }
   

}