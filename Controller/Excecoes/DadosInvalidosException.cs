namespace RhythmsOfGiving.Controller.Excecoes{
public class DadosInvalidosException : Exception
{
    public DadosInvalidosException() { }

    public DadosInvalidosException(string message) : base(message) { }

    public DadosInvalidosException(string message, Exception innerException)
        : base(message, innerException) { }
}
}