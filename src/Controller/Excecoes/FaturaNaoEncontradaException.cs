namespace RhythmsOfGiving.Controller.Excecoes;

public class FaturaNaoEncontradaException : Exception
{
    public FaturaNaoEncontradaException() { }

    public FaturaNaoEncontradaException(string message) : base(message) { }

    public FaturaNaoEncontradaException(string message, Exception innerException)
        : base(message, innerException) { }
}