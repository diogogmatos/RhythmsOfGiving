namespace RhythmsOfGiving.Controller.Excecoes;

public class TempoExcedidoException : Exception
{
    public TempoExcedidoException() { }
    public TempoExcedidoException(string message) : base(message) { }

    public TempoExcedidoException(string message, Exception innerException)
        : base(message, innerException) { }
    
}
