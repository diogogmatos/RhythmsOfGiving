
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
    private Int64 nCC;
    private LicitacaoDAO licitacaoDAO;
    private HashSet<int> minhasLicitacoes;
    private FaturaDAO faturaDAO;
    private HashSet<int> minhasFaturas;
    private NotificacaoDAO notificacaoDAO;

    private static int contadorLicitadores = LicitadorDAO.size();

    //Construtor para fazer get
    public Licitador (int idLicitador, string nome, string palavraPasse, DateOnly data, int nrCartao, string email, int nif, Int64 nCC, HashSet<int> minhasLicitacoes, HashSet<int> minhasFaturas){
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
    public Licitador(string nome, string palavraPasse, DateOnly data, int nrCartao, string email, int nif, Int64 nCC){
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

    public Int64 getNcc()
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
            this.nome.Equals(other.nome) &&
            this.palavraPasse.Equals(other.palavraPasse) &&
            this.dataNascimento == other.dataNascimento &&
            this.nrCartao == other.nrCartao &&
            this.email.Equals(other.email) &&
            this.nif == other.nif &&
            this.nCC == other.nCC);
    }

        internal void setDataNascimento(DateTime novaDataNascimento)
        {
            throw new NotImplementedException();
        }

        public Notificacao criarNotificacaoUltrapassada(string titulo)
        {
            DateTime data = DateTime.Now;
            Notificacao ultrapassada = new Notificacao("A sua licitação foi ultrapassada", titulo, this.idLicitador,data, 0);
            this.notificacaoDAO.put(ultrapassada.getId(), ultrapassada);
            //  Fazer put do licitador

            return ultrapassada;
        }

        public Notificacao criarNotificacaoPerdedora(int idLeilao, string titulo, float valor)
        {
            DateTime data = DateTime.Now;
            string mensagem = "Leilão " + idLeilao + " terminado! Infelizmente não ganhou. Obrigado por ter participado.";
            string titulo2 = titulo + " foi vendido por " + valor;
            Notificacao perdedora = new Notificacao(mensagem, titulo2,this.idLicitador, data, 2);
            this.notificacaoDAO.put(perdedora.getId(), perdedora);
            //  Fazer put do licitador


            return perdedora;
        }
        
        public Notificacao criarNotificacaoVencedora(int idLeilao, string titulo, float valor)
        {
            DateTime data = DateTime.Now;
            string mensagem = "Parabéns! Você ganhou o leilão " + idLeilao + ". " +
                              "Obrigado por participar e por sua oferta de " + valor + ".";

            string tituloNotificacao = "Você é o Vencedor - " + titulo;
            Notificacao vencedora = new Notificacao(mensagem, tituloNotificacao, this.idLicitador,data, 1);
            this.notificacaoDAO.put(vencedora.getId(), vencedora);
            return vencedora;
        }


        public Dictionary<int, Licitacao> saberLeiloesParticipa_Licitacao()
        {
            Dictionary<int, Licitacao> ultimasLicitacoes = new Dictionary<int, Licitacao>();
            
            foreach (int idLicitacao in this.minhasLicitacoes)
            {
                try
                {
                    Licitacao atual = this.licitacaoDAO.get(idLicitacao);
                    if (!ultimasLicitacoes.ContainsKey(atual.GetIdLeilao()))
                    {
                        ultimasLicitacoes.Add(atual.GetIdLeilao(), atual);
                    }
                    else
                    {
                        Licitacao presente = ultimasLicitacoes[atual.GetIdLeilao()];
                        if (atual.GetDataHora() > presente.GetDataHora())
                            ultimasLicitacoes[atual.GetIdLeilao()] = atual;
                    }

                }
                catch (LicitadorNaoExisteException e)
                {
                    throw;
                }
                

            }

            return ultimasLicitacoes;
        }

        public SortedSet<Fatura> getFaturas()
        {
            SortedSet<Fatura> faturasOrdenadas = new SortedSet<Fatura>();
            
            foreach (int id in this.minhasFaturas)
            {
                Fatura f = this.faturaDAO.get(id);
                faturasOrdenadas.Add(f);
            }

            return faturasOrdenadas;
        }

        public float valorTotalDoado()
        {
            float valorTotal = 0;
            foreach (int idFatura in this.minhasFaturas)
            {
                Fatura f = this.faturaDAO.get(idFatura);
                valorTotal += f.getValor();
            }

            return valorTotal;
        }

        public void adicionarFatura(Fatura f)
        {
            try
            {
                faturaDAO.put(f.getIdFatura(),f);
            }
            catch (DadosInvalidosException e)
            {
                throw;
            }
        }

        public SortedSet<Licitacao> getLicitacoesLeilao(int idLeilao)
        {
            SortedSet<Licitacao> licitacoes = new SortedSet<Licitacao>;

            foreach (int idLicitacao in minhasLicitacoes)
            {
                Licitacao l = licitacaoDAO.get(idLicitacao);
                if (l.GetIdLeilao() == idLeilao)
                {
                    licitacoes.Add(l);
                }
            }

            return licitacoes;
        }
}
}