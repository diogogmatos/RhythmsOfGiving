namespace RhythmsOfGiving.Controller.Excecoes;

public class InstituicaoJaExisteException : Exception
{
    public InstituicaoJaExisteException() { }

    public InstituicaoJaExisteException(string message) : base(message) { }

    public InstituicaoJaExisteException(string message, Exception innerException)
        : base(message, innerException) { }
}