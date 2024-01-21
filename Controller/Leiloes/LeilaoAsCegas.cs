
namespace RhythmsOfGiving.Controller.Leiloes
{
    public class LeilaoAsCegas : Leilao
    {
        const int Tipo = 0;

        public override int GetTipo()
        {
            return Tipo;
        }

        //Construtor get
        public LeilaoAsCegas(bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal, string titulo,
            DateTime dataHoraContador, int idAdmin, int idInstituicao, List<int> minhasLicitacoes, Experiencia experiencia)
            : base(ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin, idInstituicao, minhasLicitacoes, experiencia)
        {
        }

        //Construtor criação
        public LeilaoAsCegas(int idLeilao, bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal,
            string titulo, DateTime dataHoraContador, int idAdmin, int idInstituicao, List<int> minhasLicitacoes,
            Experiencia experiencia)
            : base(idLeilao, ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin,
                idInstituicao, minhasLicitacoes, experiencia)
        {
        }
        
        // Contrutor para leilões que não terminaram
        public LeilaoAsCegas(int idLeilao, bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal,
            string titulo, DateTime dataHoraContador, int idAdmin, List<int> minhasLicitacoes,
            Experiencia experiencia)
            : base(idLeilao, ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin,
                minhasLicitacoes, experiencia)
        {
        }

    }
}
