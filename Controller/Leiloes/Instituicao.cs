using System.Text;
using RhythmsOfGiving.Controller.Dados;

namespace RhythmsOfGiving.Controller.Leiloes
{
    public class Instituicao
    {
        private int id;
        private string nome;
        private string descricao;
        private string logoPath;
        private string link;
        private string iban;
        private int idAdmin;

        private int contadorInstituicaoes = InstituicaoDao.Size();

        //Construtor para o Get
        public Instituicao(int id, string nome, string descricao, string logoPath, string link, string iban,
            int idAdmin)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
            this.logoPath = logoPath;
            this.link = link;
            this.iban = iban;
            this.idAdmin = idAdmin;
        }

        //Construtor para criar
        public Instituicao(string nome, string descricao, string logoPath, string link, string iban, int idAdmin)
        {
            this.id = ++contadorInstituicaoes;
            this.nome = nome;
            this.descricao = descricao;
            this.logoPath = logoPath;
            this.link = link;
            this.iban = iban;
            this.idAdmin = idAdmin;
        }

        public int GetId()
        {
            return id;
        }

        public string GetNome()
        {
            return nome;
        }

        public string GetDescricao()
        {
            return descricao;
        }

        public string GetLogoPath()
        {
            return logoPath;
        }

        public string GetLink()
        {
            return link;
        }

        public string GetIban()
        {
            return this.iban;
        }

        public int GetIdAdmin()
        {
            return this.idAdmin;
        }
        
        public void SetId(int id)
        {
            this.id = id;
        }
        
             
        public void SetIban(string iban)
        {
            this.iban = iban;
        }
        
        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Instituição:: {");
            sb.Append(" IdInstituicao: ").Append(this.id);
            sb.Append(" Nome: ").Append(this.nome);
            sb.Append(" Descrição: ").Append(this.descricao);
            sb.Append(" Hiperligação: ").Append(this.link);
            sb.Append(" IBAN: ").Append(this.iban);
            sb.Append(" Id Admin: ").Append(this.idAdmin).Append(" }");

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            // Verifica se o objeto é o próprio objeto
            if (obj == this)
                return true;

            // Verifica se o objeto é nulo ou pertence a uma classe diferente
            if (obj == null || obj.GetType() != this.GetType())
                return false;

            // Faz a conversão segura do objeto para a classe Licitador
            Instituicao other = (Instituicao)obj;

            // Verifica a igualdade dos atributos
            return (this.id == other.GetId());
        }
    }
}