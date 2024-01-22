namespace RhythmsOfGiving.Controller.Excecoes{
public class NaoExisteLicitacaoAtualAnteriorException : Exception
{
    public NaoExisteLicitacaoAtualAnteriorException() { }

    public NaoExisteLicitacaoAtualAnteriorException(string message) : base(message) { }

    public NaoExisteLicitacaoAtualAnteriorException(string message, Exception innerException)
        : base(message, innerException) { }
}
}