using System;
using System.Text;


public class Licitacao
{
    // Propriedades
    public int IdLicitacao;
    public DateTime DataHora;
    public float Valor;
    public int IdLeilao;
    public int IdLicitador;
    public Licitacao(int idLicitacao, DateTime dataHora, float valor, int idLeilao, int idLicitador)
    {
        IdLicitacao = idLicitacao;
        DataHora = dataHora;
        Valor = valor;
        IdLeilao = idLeilao;
        IdLicitador = idLicitador;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Licitacao:: { ");
        sb.Append("IdLicitacao: ").Append(IdLicitacao).Append(", ");
        sb.Append("DataHora: ").Append(DataHora).Append(", ");
        sb.Append("Valor: ").Append(Valor).Append(", ");
        sb.Append("IdLeilao: ").Append(IdLeilao).Append(", ");
        sb.Append("IdLicitador: ").Append(IdLicitador).Append(" }");

        return sb.ToString();
    }

     public int GetIdLicitacao()
    {
        return IdLicitacao;
    }

    public DateTime GetDataHora()
    {
        return DataHora;
    }

    public float GetValor()
    {
        return Valor;
    }

    public int GetIdLeilao()
    {
        return IdLeilao;
    }

    public int GetIdLicitador()
    {
        return IdLicitador;
    }
}
