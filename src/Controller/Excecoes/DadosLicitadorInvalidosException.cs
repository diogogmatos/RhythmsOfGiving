namespace RhythmsOfGiving.Controller.Excecoes{
public class DadosLicitadorInvalidosException : Exception
{
    public DadosLicitadorInvalidosException() { }

    public DadosLicitadorInvalidosException(string message) : base(message) { }

    public DadosLicitadorInvalidosException(string message, Exception innerException)
        : base(message, innerException) { }
}
}