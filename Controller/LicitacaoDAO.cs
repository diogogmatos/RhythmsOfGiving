
using RhythmsOfGiving.Controller;

public class LicitacaoDAO{
    private static LicitacaoDAO? singleton = null;
        private LicitacaoDAO() { }

        public static LicitacaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LicitacaoDAO();
            }
            return singleton;
        }

    internal Licitacao get(int idLicitacao)
    {
        throw new LicitacaoNaoExisteException();
    }

    internal void put(int v, Licitacao l)
    {
        throw new NotImplementedException();
    }
    
    public static int size(){
        return 0; // depois usar a query necessária
    }
}