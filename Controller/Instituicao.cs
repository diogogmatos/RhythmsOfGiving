
using System.Text;

namespace RhythmsOfGiving.Controller
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

        private int contadorInstituicaoes = InstituicaoDAO.size();

        //Construtor para o get
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

        public int getId()
        {
            return id;
        }

        public string getNome()
        {
            return nome;
        }

        public string getDescricao()
        {
            return descricao;
        }

        public string getLogoPath()
        {
            return logoPath;
        }

        public string getLink()
        {
            return link;
        }

        public string getIban()
        {
            return this.iban;
        }

        public int getIdAdmin()
        {
            return this.idAdmin;
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
            return (this.id == other.getId() &&
                    this.nome.Equals(other.getNome()) &&
                    this.descricao.Equals(other.getDescricao()) &&
                    this.link.Equals(other.link) &&
                    this.iban.Equals(other.iban) &&
                    this.idAdmin == other.getIdAdmin());
        }
    }
}