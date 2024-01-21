namespace RhythmsOfGiving.Controller.Excecoes;

public class InstituicaoNaoEncontradaException : Exception
{
    public InstituicaoNaoEncontradaException() { }

    public InstituicaoNaoEncontradaException(string message) : base(message) { }

    public InstituicaoNaoEncontradaException(string message, Exception innerException)
        : base(message, innerException) { }
}