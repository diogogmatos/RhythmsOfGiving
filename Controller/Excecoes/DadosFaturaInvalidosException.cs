namespace RhythmsOfGiving.Controller.Excecoes{
public class DadosFaturaInvalidosException : Exception
{
    public DadosFaturaInvalidosException() { }

    public DadosFaturaInvalidosException(string message) : base(message) { }

    public DadosFaturaInvalidosException(string message, Exception innerException)
        : base(message, innerException) { }
}
}