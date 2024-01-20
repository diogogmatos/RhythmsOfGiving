
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using RhythmsOfGiving.Controller.UI;

namespace RhythmsOfGiving.Controller{
public class Licitador{

    private int idLicitador;
    private string nome;
    private string palavraPasse;
    private DateOnly dataNascimento;
    private int nrCartao;
    private string email;
    private int nif;
    private Int64 nCc;
    private LicitacaoDao licitacaoDao;
    private HashSet<int> minhasLicitacoes;
    private FaturaDAO faturaDao;
    private HashSet<int> minhasFaturas;
    private NotificacaoDao notificacaoDao;

    public Licitador()
    {
        this.licitacaoDao = LicitacaoDao.GetInstance();
        this.notificacaoDao = NotificacaoDao.GetInstance();
    }

    public Licitacao teste()
    {
        return licitacaoDao.Get(1);
    }
    

    private static int contadorLicitadores = LicitadorDao.Size();

    //Construtor para fazer get
    public Licitador (int idLicitador, string nome, string palavraPasse, DateOnly data, int nrCartao, string email, int nif, Int64 nCc, HashSet<int> minhasLicitacoes, HashSet<int> minhasFaturas){
        this.idLicitador = idLicitador;
        this.nome = nome;
        this.palavraPasse = palavraPasse;
        this.dataNascimento = data;
        this.nrCartao = nrCartao;
        this.email = email;
        this.nif = nif;
        this.nCc = nCc;
        this.licitacaoDao = LicitacaoDao.GetInstance();
        this.minhasLicitacoes = minhasLicitacoes;
        this.faturaDao = FaturaDAO.getInstance();
        this.minhasFaturas = minhasFaturas;
        this.notificacaoDao = NotificacaoDao.GetInstance();
    }

    //Construtor para criar o Licitador
    public Licitador(string nome, string palavraPasse, DateOnly data, int nrCartao, string email, int nif, Int64 nCc){
        this.idLicitador = ++contadorLicitadores;
        this.nome = nome;
        this.palavraPasse = palavraPasse;
        this.dataNascimento = data;
        this.nrCartao = nrCartao;
        this.email = email;
        this.nif= nif;
        this.nCc = nCc;
        this.licitacaoDao = LicitacaoDao.GetInstance();
        this.minhasLicitacoes = new HashSet<int>();
        this.faturaDao = FaturaDAO.getInstance();
        this.minhasFaturas = new HashSet<int>();
        this.notificacaoDao = NotificacaoDao.GetInstance();
    }

    public int GetIdLicitador()
        {
            return idLicitador;
        }

    public string GetNome()
        {
            return nome;
        }

    public string GetPalavraPasse()
        {
            return palavraPasse;
        }

    public DateOnly GetDataNascimento()
        {
            return dataNascimento;
        }

    public int GetNrCartao()
        {
            return nrCartao;
        }

    public string GetEmail()
        {
            return email;
        }

    public int GetNif()
        {
            return nif;
        }

    public Int64 GetNcc()
        {
            return nCc;
        }

    public HashSet<int> GetMinhasLicitacoes()
        {
            return minhasLicitacoes;
        }

    public HashSet<int> GetMinhasFaturas()
        {
            return minhasFaturas;
        }

    public void SetNome(string nome)
        {
            this.nome = nome;
        }

    public void SetPalavraPasse(string palavraPasse)
        {
            this.palavraPasse = palavraPasse;
        }

    public void SetDataNascimento(DateOnly data)
        {
            this.dataNascimento = data;
        }

    public void SetNrCartao(int nr)
        {
            this.nrCartao = nr;
        }

    public void SetEmail(string email)
        {
            this.email = email;
        }

    public void SetNif(int nif)
        {
            this.nif = nif;
        }

    public void SetNcc(int ncc)
        {
            this.nCc = ncc;
        }

    public void SetMinhasLicitacoes(HashSet<int> licitacoes)
        {
            this.minhasLicitacoes = licitacoes;
        }

    public void SetMinhasFaturas(HashSet<int> faturas)
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
        sb.Append(" NCC: ").Append(this.nCc);
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
            this.nCc == other.nCc);
    }

        internal void SetDataNascimento(DateTime novaDataNascimento)
        {
            this.dataNascimento = novaDataNascimento
            //throw new NotImplementedException();
        }

        public Notificacao CriarNotificacaoUltrapassada(string titulo)
        {
            DateTime data = DateTime.Now;
            Notificacao ultrapassada = new Notificacao("A sua licitação foi ultrapassada", titulo, this.idLicitador,data, 0);
            this.notificacaoDao.Put(ultrapassada.GetId(), ultrapassada);
            //  Fazer put do licitador

            return ultrapassada;
        }

        public Notificacao CriarNotificacaoPerdedora(int idLeilao, string titulo, float valor)
        {
            DateTime data = DateTime.Now;
            string mensagem = "Leilão " + idLeilao + " terminado! Infelizmente não ganhou. Obrigado por ter participado.";
            string titulo2 = titulo + " foi vendido por " + valor;
            Notificacao perdedora = new Notificacao(mensagem, titulo2,this.idLicitador, data, 2);
            this.notificacaoDao.Put(perdedora.GetId(), perdedora);
            //  Fazer put do licitador


            return perdedora;
        }
        
        public Notificacao CriarNotificacaoVencedora(int idLeilao, string titulo, float valor)
        {
            DateTime data = DateTime.Now;
            string mensagem = "Parabéns! Você ganhou o leilão " + idLeilao + ". " +
                              "Obrigado por participar e por sua oferta de " + valor + ".";

            string tituloNotificacao = "Você é o Vencedor - " + titulo;
            Notificacao vencedora = new Notificacao(mensagem, tituloNotificacao, this.idLicitador,data, 1);
            this.notificacaoDao.Put(vencedora.GetId(), vencedora);
            return vencedora;
        }


        public Dictionary<int, Licitacao> saberLeiloesParticipa_Licitacao()
        {
            Dictionary<int, Licitacao> ultimasLicitacoes = new Dictionary<int, Licitacao>();
            
            foreach (int idLicitacao in this.minhasLicitacoes)
            {
                Licitacao atual = this.licitacaoDao.Get(idLicitacao);
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

            return ultimasLicitacoes;
        }

        public SortedSet<Fatura> GetFaturas()
        {
            SortedSet<Fatura> faturasOrdenadas = new SortedSet<Fatura>();
            
            foreach (int id in this.minhasFaturas)
            {
                Fatura f = this.faturaDao.get(id);
                faturasOrdenadas.Add(f);
            }

            return faturasOrdenadas;
        }

        public float ValorTotalDoado()
        {
            float valorTotal = 0;
            foreach (int idFatura in this.minhasFaturas)
            {
                Fatura f = this.faturaDao.get(idFatura);
                valorTotal += f.GetValor();
            }

            return valorTotal;
        }

        public void AdicionarFatura(Fatura f)
        {
            faturaDao.put(f.GetIdFatura(),f);
        }

        public SortedSet<Licitacao> GetLicitacoesLeilao(int idLeilao)
        {
            SortedSet<Licitacao> licitacoes = new SortedSet<Licitacao>();

            foreach (int idLicitacao in minhasLicitacoes)
            {
                Licitacao l = licitacaoDao.Get(idLicitacao);
                if (l.GetIdLeilao() == idLeilao)
                {
                    licitacoes.Add(l);
                }
            }

            return licitacoes;
        }


        public float GetUltimaLicitacao (int idLeilao)
        {
            float valor = -1;
            
            foreach (int idLicitacao in this.minhasLicitacoes)
            {
                Licitacao atual = this.licitacaoDao.Get(idLicitacao);
                if (atual.GetIdLeilao() == idLeilao)
                {
                    if (atual.GetValor() > valor)
                    {
                        valor = atual.GetValor();
                    }
                }
            }
            if (valor == -1)
            {
                throw new NaoExistemLicitacoesException("O licitador " + idLicitador + " não tem licitações no leilão " + idLeilao);
            }
            else
            {
                return valor;
            }
        }

        public List<Notificacao> GetNotificacoes()
        {
            return notificacaoDao.getNotificacoesLicitador(idLicitador);
        }
}
}