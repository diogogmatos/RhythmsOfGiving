using System;

namespace RhythmsOfGiving.Controller{
public class DadosInvalidosException : Exception
{
    public DadosInvalidosException() { }

    public DadosInvalidosException(string message) : base(message) { }

    public DadosInvalidosException(string message, Exception innerException)
        : base(message, innerException) { }
}
}