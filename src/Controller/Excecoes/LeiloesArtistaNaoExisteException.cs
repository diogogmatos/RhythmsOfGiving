namespace RhythmsOfGiving.Controller.Excecoes;

public class LeiloesArtistaNaoExisteException : Exception
{
    public LeiloesArtistaNaoExisteException() { }
    public LeiloesArtistaNaoExisteException(string message) : base(message) { }

    public LeiloesArtistaNaoExisteException(string message, Exception innerException)
        : base(message, innerException) { }
       
}