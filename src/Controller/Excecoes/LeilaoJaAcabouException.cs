namespace RhythmsOfGiving.Controller.Excecoes{
public class LeilaoJaAcabouException : Exception
{
    public LeilaoJaAcabouException() { }

    public LeilaoJaAcabouException(string message) : base(message) { }

    public LeilaoJaAcabouException(string message, Exception innerException)
        : base(message, innerException) { }
}
}