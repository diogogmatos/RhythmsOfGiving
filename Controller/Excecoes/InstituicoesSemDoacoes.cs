namespace RhythmsOfGiving.Controller.Excecoes;

public class InstituicoesSemDoacoes : Exception
{
    public InstituicoesSemDoacoes() { }

    public InstituicoesSemDoacoes(string message) : base(message) { }

    public InstituicoesSemDoacoes(string message, Exception innerException)
        : base(message, innerException) { }
}