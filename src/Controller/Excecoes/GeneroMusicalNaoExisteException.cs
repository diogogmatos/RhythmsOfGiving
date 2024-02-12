namespace RhythmsOfGiving.Controller.Excecoes;

public class GeneroMusicalNaoExisteException : Exception
{
    public GeneroMusicalNaoExisteException() { }

    public GeneroMusicalNaoExisteException(string message) : base(message) { }

    public GeneroMusicalNaoExisteException(string message, Exception innerException)
        : base(message, innerException) { }
}