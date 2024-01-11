
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using RhythmsOfGiving.Controller;

public class Licitador{

    private int idLicitador { get; set; }
    private string nome { get; set; }
    private string palavraPasse { get; set; }
    private DateOnly dataNascimento { get; set; }
    private int nrCartao { get; set; }
    private string email { get; set; }
    private int nif { get; set; }
    private int nCC { get; set; }
    private LicitacaoDAO licitacaoDAO;
    private HashSet<int> minhasLicitacoes { get; set; }
    private FaturaDAO faturaDAO;
    private HashSet<int> minhasFaturas { get; set; }
    private NotificacaoDAO notificacaoDAO;

    private static int contadorLicitadores = LicitadorDAO.size();

    //Construtor para fazer get
    public Licitador (int idLicitador, string nome, string palavraPasse, DateOnly data, int nrCartao, string email, int nif, int nCC, HashSet<int> minhasLicitacoes, HashSet<int> minhasFaturas){
        this.idLicitador = idLicitador;
        this.nome = nome;
        this.palavraPasse = palavraPasse;
        this.dataNascimento = data;
        this.nrCartao = nrCartao;
        this.email = email;
        this.nif = nif;
        this.nCC = nCC;
        this.licitacaoDAO = LicitacaoDAO.getInstance();
        this.minhasLicitacoes = minhasLicitacoes;
        this.faturaDAO = FaturaDAO.getInstance();
        this.minhasFaturas = minhasFaturas;
        this.notificacaoDAO = NotificacaoDAO.getInstance();
    }

    //Construtor para criar o Licitador
    public Licitador(string nome, string palavraPasse, DateOnly data, int nrCartao, string email, int nif, int nCC){
        this.idLicitador = ++contadorLicitadores;
        this.nome = nome;
        this.palavraPasse = palavraPasse;
        this.dataNascimento = data;
        this.nrCartao = nrCartao;
        this.email = email;
        this.nif= nif;
        this.nCC = nCC;
        this.licitacaoDAO = LicitacaoDAO.getInstance();
        this.minhasLicitacoes = new HashSet<int>();
        this.faturaDAO = FaturaDAO.getInstance();
        this.minhasFaturas = new HashSet<int>();
        this.notificacaoDAO = NotificacaoDAO.getInstance();
    }

    //Representar o Licitador em string
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Licitador:: {");
        sb.Append(" IdLicitador: ").Append(this.idLicitador);
        sb.Append(" Nome: ").Append(this.nome);
        sb.Append(" Palavra passe: ").Append(this.palavraPasse);
        sb.Append(" Data nascimento: ").Append(this.dataNascimento.ToString());
        sb.Append(" Nr cartão credito: ").Append(this.nrCartao);
        sb.Append(" Email: ").Append(this.email);
        sb.Append(" NIF: ").Append(this.nif);
        sb.Append(" NCC: ").Append(this.nCC);
        sb.Append(" Ids licitações: ").Append(this.minhasLicitacoes.ToString());
        sb.Append(" Ids faturas: ").Append(this.minhasFaturas.ToString()).Append(" }");

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
    Licitador other = (Licitador)obj;

    // Verifica a igualdade dos atributos
    return (this.idLicitador == other.idLicitador &&
            this.nome == other.nome &&
            this.dataNascimento == other.dataNascimento &&
            this.nrCartao == other.nrCartao &&
            this.email == other.email &&
            this.nif == other.nif &&
            this.nCC == other.nCC);
    }

}