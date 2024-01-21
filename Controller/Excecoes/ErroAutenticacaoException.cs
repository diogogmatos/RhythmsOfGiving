namespace RhythmsOfGiving.Controller.Excecoes;

public class ErroAutenticacaoException: Exception
{
    public ErroAutenticacaoException() { }
    public ErroAutenticacaoException(string message) : base(message) { }

    public ErroAutenticacaoException(string message, Exception innerException)
        : base(message, innerException) { }
}