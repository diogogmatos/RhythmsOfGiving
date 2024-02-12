namespace RhythmsOfGiving.Controller.Excecoes;

public class AdministradorNaoExisteException : Exception
{
    public AdministradorNaoExisteException() { }

    public AdministradorNaoExisteException(string message) : base(message) { }

    public AdministradorNaoExisteException(string message, Exception innerException)
        : base(message, innerException) { }
}