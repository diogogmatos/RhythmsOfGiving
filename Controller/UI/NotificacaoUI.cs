using RhythmsOfGiving.Controller.Utilizadores;

namespace RhythmsOfGiving.Controller.UI;

public class NotificacaoUi
{
    private string title;
    private string message;
    private DateTime date;
    private int tipo; // 0 -> ultrapassada, 1 -> vencedora, 2 -> perdedora
    private int idLeilao;

    public NotificacaoUi(string title, string message, DateTime date, int tipo, int idLeilao)
    {
        this.title = title;
        this.message = message;
        this.date = date;
        this.tipo = tipo;
        this.idLeilao = idLeilao;
    }

    public NotificacaoUi(Notificacao notificacao)
    {
        this.title = notificacao.GetTitle();
        this.message = notificacao.GetMessage();
        this.date = notificacao.GetDate();
        this.tipo = notificacao.GetTipo();
        this.idLeilao = notificacao.GetId(); // GetIdLeilao()
    }
    
    public int GetIdLeilao()
    {
        return idLeilao;
    }

    public string GetTitle() {
        return title;
    }

    public string GetMessage() {
        return message;
    }

    public DateTime GetDate() {
        return date;
    }

    public int GetTipo()
    {
        return tipo;
    }
}
