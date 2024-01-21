
using System.Text;
using Microsoft.Extensions.Primitives;
using RhythmsOfGiving.Controller.Dados;

namespace RhythmsOfGiving.Controller
{
    public class Fatura : IComparable<Fatura>
    {
        private int idFatura;
        private DateTime dataHoraEmissao;
        private int idInstituicao;
        private string nomeLicitador;
        private int nif;
        private int idLicitacao;
        private int idLicitador;
        private LicitacaoDao licitacaoDao;
        private InstituicaoDao instituicaoDao;

        private static int _contadorFat = FaturaDAO.size();

        //Construtor para criar
        public Fatura(DateTime dataHora, int idInstituicao, string nomeLicitador, int nif, int idLicitacao, int idLicitador)
        {
            this.idFatura = ++_contadorFat;
            this.dataHoraEmissao = dataHora;
            this.idInstituicao = idInstituicao;
            this.nomeLicitador = nomeLicitador;
            this.nif = nif;
            this.idLicitacao = idLicitacao;
            this.idLicitador = idLicitador;
            this.licitacaoDao = LicitacaoDao.GetInstance();
            this.instituicaoDao = InstituicaoDao.GetInstance();
        }

        //Construtor para get
        public Fatura(int id, DateTime dataHora, int idInstituicao, string nomeLicitador, int nif, int idLicitacao, int idLicitador)
        {
            this.idFatura = id;
            this.dataHoraEmissao = dataHora;
            this.idInstituicao = idInstituicao;
            this.nomeLicitador = nomeLicitador;
            this.nif = nif;
            this.idLicitacao = idLicitacao;
            this.idLicitador = idLicitador;
            this.licitacaoDao = LicitacaoDao.GetInstance();
            this.instituicaoDao = InstituicaoDao.GetInstance();
        }

        public int GetIdFatura()
        {
            return idFatura;
        }

        public DateTime GetDataHoraEmissao()
        {
            return dataHoraEmissao;
        }

        public int GetIdInstituicao()
        {
            return idInstituicao;
        }

        public string GetNomeLicitador()
        {
            return nomeLicitador;
        }

        public int GetNif()
        {
            return nif;
        }

        public int GetIdLicitacao()
        {
            return idLicitacao;
        }
        
        public int GetIdLicitador()
        {
            return idLicitador;
        }

        public float GetValor()
        {
            Licitacao l = this.licitacaoDao.Get(this.idLicitacao);
            return (l.GetValor());
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Fatura:: {");
            sb.Append(" IdFatura: ").Append(this.idFatura);
            sb.Append(" Data e hora emissão: ").Append(this.dataHoraEmissao.ToString());
            sb.Append(" IdInstituição: ").Append(this.idInstituicao);
            sb.Append(" Nome do licitador: ").Append(this.nomeLicitador);
            sb.Append(" IdLicitação: ").Append(this.idLicitacao);
            sb.Append(" IdLicitador: ").Append(this.idLicitador);
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
            return (this.idFatura == other.GetIdFatura() &&
                    this.dataHoraEmissao.Equals(other.GetDataHoraEmissao()) &&
                    this.idInstituicao == other.GetIdInstituicao() &&
                    this.nomeLicitador.Equals(other.GetNomeLicitador()) &&
                    this.nif == other.GetNif() &&
                    this.idLicitacao == other.GetIdLicitacao()
                );

        }
        
        public int CompareTo(Fatura other)
        {
            // Comparação com base na data invertida para ordenar em ordem decrescente
            return other.GetDataHoraEmissao().CompareTo(this.dataHoraEmissao);
        }

    }
}