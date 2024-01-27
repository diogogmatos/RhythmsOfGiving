namespace RhythmsOfGiving.Controller.Excecoes;

public class LeiloesSemLicitacoes : Exception
{
    public LeiloesSemLicitacoes() { }

    public LeiloesSemLicitacoes(string message) : base(message) { }

    public LeiloesSemLicitacoes(string message, Exception innerException)
        : base(message, innerException) { }
}