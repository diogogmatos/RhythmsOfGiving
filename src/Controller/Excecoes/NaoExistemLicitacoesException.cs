namespace RhythmsOfGiving.Controller.Excecoes{
public class NaoExistemLicitacoesException : Exception
{
    public NaoExistemLicitacoesException() { }

    public NaoExistemLicitacoesException(string message) : base(message) { }

    public NaoExistemLicitacoesException(string message, Exception innerException)
        : base(message, innerException) { }
}
}