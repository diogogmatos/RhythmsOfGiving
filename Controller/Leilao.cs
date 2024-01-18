using System;
using System.Collections.Generic;
using RhythmsOfGiving.Controller;

public class Leilao
{
    // Atributos privados
    private int idLeilao;
    private bool estado;
    private float valorAtual;
    private float valorBase;
    private DateTime dataHoraFinal;
    private string titulo;
    private DateTime dataHoraContador;
    private int idAdmin;
    private int percentagemAumento;
    private int idInstituicao;
    private List<int> minhasLicitacoes;
    private LicitacaoDAO licitacaoDAO;
    private Experiencia experiencia;

    private static int contador = LeilaoDAO.size();
    

    // Construtor
    public Leilao( bool estado, float valorAtual, float valorBase, DateTime dataHoraFinal,
                  string titulo, DateTime dataHoraContador, int idAdmin, int percentagemAumento,
                  int idInstituicao, List<int> minhasLicitacoes,  Experiencia experiencia)
    {
        this.idLeilao = ++contador;
        this.estado = estado;
        this.valorAtual = valorAtual;
        this.valorBase = valorBase;
        this.dataHoraFinal = dataHoraFinal;
        this.titulo = titulo;
        this.dataHoraContador = dataHoraContador;
        this.idAdmin = idAdmin;
        this.percentagemAumento = percentagemAumento;
        this.idInstituicao = idInstituicao;
        this.minhasLicitacoes = minhasLicitacoes;
        this.licitacaoDAO = LicitacaoDAO.getInstance();
        this.experiencia = experiencia;
    }
    
    public Leilao(int idLeilao, bool estado, float valorAtual, float valorBase, DateTime dataHoraFinal,
        string titulo, DateTime dataHoraContador, int idAdmin, int percentagemAumento,
        int idInstituicao, List<int> minhasLicitacoes,  Experiencia experiencia)
    {
        this.idLeilao = idLeilao;
        this.estado = estado;
        this.valorAtual = valorAtual;
        this.valorBase = valorBase;
        this.dataHoraFinal = dataHoraFinal;
        this.titulo = titulo;
        this.dataHoraContador = dataHoraContador;
        this.idAdmin = idAdmin;
        this.percentagemAumento = percentagemAumento;
        this.idInstituicao = idInstituicao;
        this.minhasLicitacoes = minhasLicitacoes;
        this.licitacaoDAO = LicitacaoDAO.getInstance();
        this.experiencia = experiencia;
    }

    // Propriedades Get e Set
    public int IdLeilao
    {
        get { return idLeilao; }
        set { idLeilao = value; }
    }
    public bool Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    public float ValorAtual
    {
        get { return valorAtual; }
        set { valorAtual = value; }
    }

    public float ValorBase
    {
        get { return valorBase; }
        set { valorBase = value; }
    }

    public DateTime DataHoraFinal
    {
        get { return dataHoraFinal; }
        set { dataHoraFinal = value; }
    }

    public string Titulo
    {
        get { return titulo; }
        set { titulo = value; }
    }

    public DateTime DataHoraContador
    {
        get { return dataHoraContador; }
        set { dataHoraContador = value; }
    }

    public int IdAdmin
    {
        get { return idAdmin; }
        set { idAdmin = value; }
    }

    public int PercentagemAumento
    {
        get { return percentagemAumento; }
        set { percentagemAumento = value; }
    }

    public int IdInstituicao
    {
        get { return idInstituicao; }
        set { idInstituicao = value; }
    }

    public List<int> MinhasLicitacoes
    {
        get { return minhasLicitacoes; }
        set { minhasLicitacoes = value; }
    }

    // Método ToString
    public Experiencia Experiencia
    {
        get { return experiencia; }
        set { experiencia = value; }
    }

    // Sobrescreva o método ToString
    public override string ToString()
    {
        return $"Leilao [Id: {IdLeilao}, Estado: {estado}, Valor Atual: {valorAtual}, " +
               $"Valor Base: {valorBase}, Data/Hora Final: {dataHoraFinal}, Título: {titulo}, " +
               $"Data/Hora Contador: {dataHoraContador}, Id Admin: {idAdmin}, " +
               $"Percentagem Aumento: {percentagemAumento}, Id Instituicao: {idInstituicao}, " +
               $"Minhas Licitacoes: {string.Join(", ", minhasLicitacoes)}, " +
               $"Experiencia: {Experiencia}]";
    }

    public float getValorFim()
    {
        if (minhasLicitacoes.Count == 0)
        {
            throw new NaoExistemLicitacoesException("O leilão não teve qualquer licitação");
        }
        else
        {
            float resultado = -1
            foreach (int idLicitacao in minhasLicitacoes)
            {
                try
                {
                    Licitacao licitacao = licitacaoDAO.get(idLicitacao);
                    valorAtual = licitacao.GetValor();
                    if (valorAtual>resultado)
                    {
                        resultado = valorAtual;
                    }
                }
                catch (LicitacaoNaoExisteException ex){
                    throw;
                }
            }
            return resultado;
        }
    }

    public HashSet<int> getLicitadores()
    {
        if (minhasLicitacoes.Count == 0)
        {
            throw new NaoExistemLicitacoesException("O leilão não teve qualquer licitação");
        }
        else
        {
            HashSet<int> licitadores = new HashSet<int>();
            foreach (int idLicitacao in minhasLicitacoes)
            {
                try
                {
                    Licitacao licitacao = licitacaoDAO.get(idLicitacao);
                    licitadores.Add(licitacao.GetIdLicitador());
                }
                catch (LicitacaoNaoExisteException ex){
                    throw;
                }
            }
            return licitadores;
        }
    }
}
