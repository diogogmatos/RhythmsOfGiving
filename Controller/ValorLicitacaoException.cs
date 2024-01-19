namespace RhythmsOfGiving.Controller;

public class ValorLicitacaoException : Exception
{
    public ValorLicitacaoException() { }

    public ValorLicitacaoException(string message) : base(message) { }

    public ValorLicitacaoException(string message, Exception innerException)
        : base(message, innerException) { }
}