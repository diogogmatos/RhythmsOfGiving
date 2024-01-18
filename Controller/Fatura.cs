
using System.Text;
using Microsoft.Extensions.Primitives;

namespace RhythmsOfGiving.Controller
{
    public class Fatura
    {
        private int idFatura;
        private DateTime dataHoraEmissao;
        private int idInstituicao;
        private string nomeLicitador;
        private int nif;
        private InstituicaoDAO instituicaoDAO;

        private static int contadorFat = FaturaDAO.size();

        //Construtor para criar
        public Fatura(DateTime dataHora, int idInstituicao, string nomeLicitador, int nif)
        {
            this.idFatura = ++contadorFat;
            this.dataHoraEmissao = dataHora;
            this.idInstituicao = idInstituicao;
            this.nomeLicitador = nomeLicitador;
            this.nif = nif;
            this.instituicaoDAO = InstituicaoDAO.getInstance();
        }

        //Construtor para get
        public Fatura(int id, DateTime dataHora, int idInstituicao, string nomeLicitador, int nif)
        {
            this.idFatura = id;
            this.dataHoraEmissao = dataHora;
            this.idInstituicao = idInstituicao;
            this.nomeLicitador = nomeLicitador;
            this.nif = nif;
            this.instituicaoDAO = InstituicaoDAO.getInstance();
        }

        public int getIdFatura()
        {
            return idFatura;
        }

        public DateTime getDataHoraEmissao()
        {
            return dataHoraEmissao;
        }

        public int getIdInstituicao()
        {
            return idInstituicao;
        }

        public string getNomeLicitador()
        {
            return nomeLicitador;
        }

        public int getNIF()
        {
            return nif;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Fatura:: {");
            sb.Append(" IdFatura: ").Append(this.idFatura);
            sb.Append(" Data e hora emissão: ").Append(this.dataHoraEmissao.ToString());
            sb.Append(" IdInstituição: ").Append(this.idInstituicao);
            sb.Append(" Nome do licitador: ").Append(this.nomeLicitador);
            sb.Append(" NIF licitador: ").Append(this.nif).Append(" }");

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
            Fatura other = (Fatura)obj;

            // Verifica a igualdade dos atributos
            return (this.idFatura == other.getIdFatura() &&
                    this.dataHoraEmissao.Equals(other.dataHoraEmissao) &&
                    this.idInstituicao == other.getIdInstituicao() &&
                    this.nomeLicitador.Equals(other.getNomeLicitador()) &&
                    this.nif == other.getNIF()
                );

        }

    }
}