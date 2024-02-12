namespace RhythmsOfGiving.Controller.Excecoes;

public class NaoExistemNotificacao : Exception
{
    public NaoExistemNotificacao() { }

    public NaoExistemNotificacao(string message) : base(message) { }

    public NaoExistemNotificacao(string message, Exception innerException)
        : base(message, innerException) { }
}