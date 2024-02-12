namespace RhythmsOfGiving.Controller.Leiloes
{
    public class LeilaoIngles : Leilao
    {
        const int Tipo = 1;

        public override int GetTipo()
        {
            return Tipo;
        }
        

        //Construtor get
        public LeilaoIngles(bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal, string titulo,
            DateTime dataHoraContador, int idAdmin, int idInstituicao, List<int> minhasLicitacoes, Experiencia experiencia)
            : base(ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin, idInstituicao, minhasLicitacoes, experiencia)
        {
        }

        //Construtor criação
        public LeilaoIngles(int idLeilao, bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal,
            string titulo, DateTime dataHoraContador, int idAdmin, int idInstituicao, List<int> minhasLicitacoes,
            Experiencia experiencia)
            : base(idLeilao, ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin,
                idInstituicao, minhasLicitacoes, experiencia)
        {
        }
        
        // Contrutor para leilões que não terminaram
        public LeilaoIngles(int idLeilao, bool ativo, float valorAtual, float valorBase, DateTime dataHoraFinal,
            string titulo, DateTime dataHoraContador, int idAdmin, List<int> minhasLicitacoes,
            Experiencia experiencia)
            : base(idLeilao, ativo, valorAtual, valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin,
                minhasLicitacoes, experiencia)
        {
        }
        
    }
}