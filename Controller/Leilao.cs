/*
using System;
using System.Collections.Generic;
using RhythmsOfGiving.Controller;

public abstract class Leilao
{
    // Atributos privados
    private int idLeilao;
    private bool ativo;
    private float valorAtual;
    private float valorBase;
    private DateTime dataHoraFinal;
    private string titulo;
    private DateTime dataHoraContador;
    private int idAdmin;
    private int idInstituicao;
    private List<int> minhasLicitacoes;
    private LicitacaoDAO licitacaoDAO;
    private Experiencia experiencia;
    private InstituicaoDAO instituicaoDAO;

    public static int percentagemAumento = 1;
    private static int contador = LeilaoDAO.size();
    

    //método abstract
    public abstract int GetTipo();

    // Construtor de criação
    public Leilao( bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal,
                  string titulo, DateTime dataHoraContador, int idAdmin, int idInstituicao, List<int> minhasLicitacoes,  Experiencia experiencia)

    {
        this.idLeilao = ++contador;
        this.ativo = ativo;
        this.valorAtual = valorAtual;
        this.valorBase = valorBase;
        this.dataHoraFinal = dataHoraFinal;
        this.titulo = titulo;
        this.dataHoraContador = dataHoraContador;
        this.idAdmin = idAdmin;
        this.idInstituicao = -1;
        this.minhasLicitacoes = new List<int>();
        this.licitacaoDAO = LicitacaoDAO.getInstance();
        this.experiencia = experiencia;
        this.instituicaoDAO = InstituicaoDAO.getInstance();
    }
    
    public Leilao(int idLeilao, bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal,
        string titulo, DateTime dataHoraContador, int idAdmin,
        int idInstituicao, List<int> minhasLicitacoes,  Experiencia experiencia)
    {
        this.idLeilao = idLeilao;
        this.ativo = ativo;
        this.valorAtual = valorAtual;
        this.valorBase = valorBase;
        this.dataHoraFinal = dataHoraFinal;
        this.titulo = titulo;
        this.dataHoraContador = dataHoraContador;
        this.idAdmin = idAdmin;
        this.idInstituicao = idInstituicao;
        this.minhasLicitacoes = minhasLicitacoes;
        this.licitacaoDAO = LicitacaoDAO.getInstance();
        this.experiencia = experiencia;
        this.instituicaoDAO = InstituicaoDAO.getInstance();
    }

    // Contrutor para leilões que não terminaram
    public Leilao(int idLeilao, bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal,
        string titulo, DateTime dataHoraContador, int idAdmin,
        List<int> minhasLicitacoes,  Experiencia experiencia)
    {
        this.idLeilao = idLeilao;
        this.ativo = ativo;
        this.valorAtual = valorAtual;
        this.valorBase = valorBase;
        this.dataHoraFinal = dataHoraFinal;
        this.titulo = titulo;
        this.dataHoraContador = dataHoraContador;
        this.idAdmin = idAdmin;
        this.minhasLicitacoes = minhasLicitacoes;
        this.IdInstituicao = -1;
        this.licitacaoDAO = LicitacaoDAO.getInstance();
        this.experiencia = experiencia;
        this.instituicaoDAO = InstituicaoDAO.getInstance();
    }


    // Propriedades Get e Set
    public int IdLeilao
    {
        get { return idLeilao; }
        set { idLeilao = value; }
    }
    public bool Ativo
    {
        get { return ativo; }
        set { ativo = value; }
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

    public void setIdInstituicao(int id)
    {
        this.idInstituicao = id;
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
        return $"Leilao [Id: {IdLeilao}, Ativo: {ativo}, Valor Atual: {valorAtual}, " +
               $"Valor Base: {valorBase}, Data/Hora Final: {dataHoraFinal}, Título: {titulo}, " +
               $"Data/Hora Contador: {dataHoraContador}, Id Admin: {idAdmin}, " +
               $"Percentagem Aumento: {percentagemAumento}, Id Instituicao: {idInstituicao}, " +
               $"Minhas Licitacoes: {string.Join(", ", minhasLicitacoes)}, " +
               $"Experiencia: {Experiencia}]";
    }
    
    
      public int GetLicitadorGanhador()
    {
        int idLicitadorVencedor = -1;
        this.ativo = false;
        

            List<int> licitacoes = this.minhasLicitacoes;
            float valorMaior = 0;

            foreach (int idLicitacao in licitacoes)
            {
                Licitacao l = licitacaoDAO.get(idLicitacao);

                if (l != null)
                {
                    float valorAtual = l.GetValor();

                    if (valorAtual > valorMaior)
                    {
                        valorMaior = valorAtual;
                        idLicitadorVencedor = l.GetIdLicitador();
                    }
                }
            }

        return idLicitadorVencedor;
    }

    public int criarLicitacao(float valorLicitacao, int idLicitador)
    {
        Licitacao novaLicitacao = new Licitacao(DateTime.Now, valorLicitacao, this.idLeilao, idLicitador);
        this.minhasLicitacoes.Add(novaLicitacao.GetIdLicitacao());
        this.licitacaoDAO.put(novaLicitacao.GetIdLicitacao(), novaLicitacao);
        return novaLicitacao.GetIdLicitacao();
    }

    //Verifiquem se está certo
    public int verificarLicitacao(int idLicitador, float valorLicitacao, float valorMinimo)
    {
        DateTime atual = DateTime.Now;
        if (atual < this.dataHoraFinal)
        {
            if (valorLicitacao >= valorMinimo)
            {

                //Acrescentar 5 min ao contador
                this.dataHoraContador = atual.AddMinutes(5);
                return this.criarLicitacao(valorLicitacao, idLicitador);
            }

            throw new ValorLicitacaoException("O valor introduzido é inválido.\n" +
                                              "O valor mínimo da licitação é de: " + valorMinimo);
        }
        else
        {
            TimeSpan diferenca = this.dataHoraContador - (this.dataHoraFinal.AddHours(24));
            double diferencaEmMinutos = diferenca.TotalMinutes;
            
            if (atual < this.dataHoraContador)
            {
                if (diferencaEmMinutos >= 5)
                {
                    if (valorLicitacao >= valorMinimo)
                    {

                        //Acrescentar 5 min ao contador
                        this.dataHoraContador = atual.AddMinutes(5);
                        return this.criarLicitacao(valorLicitacao, idLicitador);
                    }

                    throw new ValorLicitacaoException("O valor introduzido é inválido.\n" +
                                                      "O valor mínimo da licitação é de: " + valorMinimo);
                }
                else
                {
                    if (valorLicitacao >= valorMinimo)
                    {

                        //Acrescentar diferençaTempo ao contador
                        this.dataHoraContador = atual.AddMinutes(diferencaEmMinutos);
                        return this.criarLicitacao(valorLicitacao, idLicitador);
                    }

                    throw new ValorLicitacaoException("O valor introduzido é inválido.\n" +
                                                      "O valor mínimo da licitação é de: " + valorMinimo);
                }
                
            }
            else
            {
                throw new TempoExcedidoException("O tempo do leilão " + this.idLeilao + " foi excedido.");
            }
        }
    }

    public float getValorFim()
    {
        if (minhasLicitacoes.Count == 0)
        {
            throw new NaoExistemLicitacoesException("O leilão não teve qualquer licitação");
        }
        else
        {
            float resultado = -1;
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

    public Licitacao LicitacaoAtualAnterior()
    {
        float valor;
        if (minhasLicitacoes.Count <= 1)
        {
            throw new NaoExistemLicitacoesException("Não houve uma licitaçao atual anterior");
        }
        else
        {
            Licitacao atual = null;
            Licitacao anterior = null;
            foreach (int idLicitacao in minhasLicitacoes)
            {
                try
                {
                    Licitacao licitacao = licitacaoDAO.get(idLicitacao);
                    valor = licitacao.GetValor();
                    if (atual == null)
                    {
                        atual = licitacao;
                    }
                    else if (anterior == null)
                    {
                        if (valor < atual.GetValor())
                        {
                            anterior = licitacao;
                        }
                        else
                        {
                            anterior = atual;
                            atual = licitacao;
                        }
                    }
                    else
                    {
                        if (valor > atual.GetValor())
                        {
                            anterior = atual;
                            atual = licitacao;
                        }
                        else if (valor > anterior.GetValor())
                        {
                            anterior = licitacao;
                        }
                    }
                }
                catch (LicitacaoNaoExisteException ex){
                    throw;
                }
            }
            return anterior;
        }
    }
}
*/
