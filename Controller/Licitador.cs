
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using RhythmsOfGiving.Controller;

namespace RhythmsOfGiving.Controller{
public class Licitador{

    private int idLicitador;
    private string nome;
    private string palavraPasse;
    private DateOnly dataNascimento;
    private int nrCartao;
    private string email;
    private int nif;
    private int nCC;
    private LicitacaoDAO licitacaoDAO;
    private HashSet<int> minhasLicitacoes;
    private FaturaDAO faturaDAO;
    private HashSet<int> minhasFaturas;
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

    public int getIdLicitador()
        {
            return idLicitador;
        }

    public string getNome()
        {
            return nome;
        }

    public string getPalavraPasse()
        {
            return palavraPasse;
        }

    public DateOnly getDataNascimento()
        {
            return dataNascimento;
        }

    public int getNrCartao()
        {
            return nrCartao;
        }

    public string getEmail()
        {
            return email;
        }

    public int getNIF()
        {
            return nif;
        }

    public int getNcc()
        {
            return nCC;
        }

    public HashSet<int> getMinhasLicitacoes()
        {
            return minhasLicitacoes;
        }

    public HashSet<int> getMinhasFaturas()
        {
            return minhasFaturas;
        }

    public void setNome(string nome)
        {
            this.nome = nome;
        }

    public void setPalavraPasse(string palavraPasse)
        {
            this.palavraPasse = palavraPasse;
        }

    public void setDataNascimento(DateOnly data)
        {
            this.dataNascimento = data;
        }

    public void setNrCartao(int nr)
        {
            this.nrCartao = nr;
        }

    public void setEmail(string email)
        {
            this.email = email;
        }

    public void setNIF(int nif)
        {
            this.nif = nif;
        }

    public void setNcc(int ncc)
        {
            this.nCC = ncc;
        }

    public void setMinhasLicitacoes(HashSet<int> licitacoes)
        {
            this.minhasLicitacoes = licitacoes;
        }

    public void setMinhasFaturas(HashSet<int> faturas)
        {
            this.minhasFaturas = faturas;
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
            this.nome.Equals(other.nome) &&
            this.palavraPasse.Equals(other.palavraPasse) &&
            this.dataNascimento == other.dataNascimento &&
            this.nrCartao == other.nrCartao &&
            this.email.Equals(other.email) &&
            this.nif == other.nif &&
            this.nCC == other.nCC);
    }

}
}