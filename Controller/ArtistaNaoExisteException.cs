using System;

namespace RhythmsOfGiving.Controller{
public class ArtistaNaoExisteException : Exception
{
    public ArtistaNaoExisteException() { }

    public ArtistaNaoExisteException(string message) : base(message) { }

    public ArtistaNaoExisteException(string message, Exception innerException)
        : base(message, innerException) { }
}
}