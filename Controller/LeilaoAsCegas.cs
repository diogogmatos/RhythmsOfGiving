
public class LeilaoAsCegas : Leilao
{
    const int tipo = 0;

    public abstract int GetTipo()
    {
        return tipo;
    }

    public LeilaoAsCegas(bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal, string titulo, DateTime dataHoraContador, int idAdmin, Experiencia experiencia)
        : base(ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin, experiencia)
    {}

    public LeilaoAsCegas(int idLeilao, bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal, string titulo, DateTime dataHoraContador, int idAdmin, int idInstituicao, List<int> minhasLicitacoes, Experiencia experiencia)
        : base(idLeilao, ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin, idInstituicao, minhasLicitacoes, experiencia)
    {}

}
