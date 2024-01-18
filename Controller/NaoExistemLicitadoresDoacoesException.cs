namespace RhythmsOfGiving.Controller;

public class NaoExistemLicitadoresDoacoesException : Exception
{
    public NaoExistemLicitadoresDoacoesException() { }

    public NaoExistemLicitadoresDoacoesException(string message) : base(message) { }

    public NaoExistemLicitadoresDoacoesException(string message, Exception innerException)
        : base(message, innerException) { }
}