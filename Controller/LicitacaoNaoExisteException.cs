using System;

namespace RhythmsOfGiving.Controller{
public class LicitacaoNaoExisteException : Exception
{
    public LicitacaoNaoExisteException() { }

    public LicitacaoNaoExisteException(string message) : base(message) { }

    public LicitacaoNaoExisteException(string message, Exception innerException)
        : base(message, innerException) { }
}
}