namespace RhythmsOfGiving.Controller.Excecoes;

public class ArtistaJaExisteException : Exception
{
    public ArtistaJaExisteException() { }

    public ArtistaJaExisteException(string message) : base(message) { }

    public ArtistaJaExisteException(string message, Exception innerException)
        : base(message, innerException) { }
}
