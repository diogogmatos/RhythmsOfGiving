namespace RhythmsOfGiving.Controller.Excecoes{
public class LicitadorNaoExisteException : Exception
{
    public LicitadorNaoExisteException() { }

    public LicitadorNaoExisteException(string message) : base(message) { }

    public LicitadorNaoExisteException(string message, Exception innerException)
        : base(message, innerException) { }
}
}
