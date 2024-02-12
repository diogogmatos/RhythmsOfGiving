namespace RhythmsOfGiving.Controller.Excecoes;

public class GeneroMusicalJaExisteException : Exception
{
    public GeneroMusicalJaExisteException() { }

    public GeneroMusicalJaExisteException(string message) : base(message) { }

    public GeneroMusicalJaExisteException(string message, Exception innerException)
        : base(message, innerException) { }
}