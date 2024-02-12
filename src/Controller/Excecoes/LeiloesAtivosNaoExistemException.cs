namespace RhythmsOfGiving.Controller.Excecoes;

public class LeiloesAtivosNaoExistemException : Exception
{
    public LeiloesAtivosNaoExistemException() { }

    public LeiloesAtivosNaoExistemException(string message) : base(message) { }

    public LeiloesAtivosNaoExistemException(string message, Exception innerException)
        : base(message, innerException) { }
}