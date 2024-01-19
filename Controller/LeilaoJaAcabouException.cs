using System;

namespace RhythmsOfGiving.Controller{
public class LeilaoJaAcabouException : Exception
{
    public LeilaoJaAcabouException() { }

    public LeilaoJaAcabouException(string message) : base(message) { }

    public LeilaoJaAcabouException(string message, Exception innerException)
        : base(message, innerException) { }
}
}