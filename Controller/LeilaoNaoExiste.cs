using System;

namespace RhythmsOfGiving.Controller{
public class LeilaoNaoExiste : Exception
{
    public LeilaoNaoExiste() { }

    public LeilaoNaoExiste(string message) : base(message) { }

    public LeilaoNaoExiste(string message, Exception innerException)
        : base(message, innerException) { }
}
}