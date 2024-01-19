namespace RhythmsOfGiving.Controller
{
    public class LeilaoIngles : Leilao
    {
        const int tipo = 1;

        public abstract int GetTipo()
        {
            return tipo;
        }

        public LeilaoIngles(bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal, string titulo,
            DateTime dataHoraContador, int idAdmin, Experiencia experiencia)
            : base(ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin, experiencia)
        {
        }

        public LeilaoIngles(int idLeilao, bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal,
            string titulo, DateTime dataHoraContador, int idAdmin, int idInstituicao, List<int> minhasLicitacoes,
            Experiencia experiencia)
            : base(idLeilao, ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin,
                idInstituicao, minhasLicitacoes, experiencia)
        {
        }
    }
}