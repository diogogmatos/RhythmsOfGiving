namespace RhythmsOfGiving.Controller.Excecoes;

public class VerificarNãoUnicoNumeroCcException : Exception
{
    public VerificarNãoUnicoNumeroCcException() { }

    public VerificarNãoUnicoNumeroCcException(string message) : base(message) { }

    public VerificarNãoUnicoNumeroCcException(string message, Exception innerException)
        : base(message, innerException) { }
}