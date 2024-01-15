using System;

namespace RhythmsOfGiving.Controller{
public class ArtistaJaExistenteException : Exception
{
    public ArtistaJaExistenteException() { }

    public ArtistaJaExistenteException(string message) : base(message) { }

    public ArtistaJaExistenteException(string message, Exception innerException)
        : base(message, innerException) { }
}
}