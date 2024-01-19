using System;
using System.Text;


public class Licitacao : IComparable<Licitacao>
{
    // Propriedades
    private int idLicitacao;
    private DateTime dataHora;
    private float valor;
    private int idLeilao;
    private int idLicitador;

    private static int contadorID = LicitacaoDAO.size();
    
    //Para o get
    public Licitacao(int idLicitacao, DateTime dataHora, float valor, int idLeilao, int idLicitador)
    {
        this.idLicitacao = idLicitacao;
        this.dataHora = dataHora;
        this.valor = valor;
        this.idLeilao = idLeilao;
        this.idLicitador = idLicitador;
    }
    
    //Para criar
    public Licitacao(DateTime dataHora, float valor, int idLeilao, int idLicitador)
    {
        this.idLicitacao = ++contadorID;
        this.dataHora = dataHora;
        this.valor = valor;
        this.idLeilao = idLeilao;
        this.idLicitador = idLicitador;
    }
    

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Licitacao:: { ");
        sb.Append(" IdLicitacao: ").Append(this.idLeilao).Append(", ");
        sb.Append(" DataHora: ").Append(this.dataHora).Append(", ");
        sb.Append(" Valor: ").Append(this.valor).Append(", ");
        sb.Append(" IdLeilao: ").Append(this.idLeilao).Append(", ");
        sb.Append(" IdLicitador: ").Append(this.idLicitador).Append(" }");

        return sb.ToString();
    }

     public int GetIdLicitacao()
    {
        return idLeilao;
    }

    public DateTime GetDataHora()
    {
        return dataHora;
    }

    public float GetValor()
    {
        return valor;
    }

    public int GetIdLeilao()
    {
        return idLeilao;
    }

    public int GetIdLicitador()
    {
        return idLicitador;
    }


    //Licitações serão ordenadas cronologicamente (da mais antiga para a mais recente)
    public int CompareTo(Licitacao outra)
    {
        return dataHora.CompareTo(outra.GetDataHora());
    }
}
