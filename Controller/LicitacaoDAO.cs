
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
        throw new NotImplementedException();
    }

    internal Licitacao getPorID(int idLicitacao)
    {
        throw new NotImplementedException();
    }

    internal void put(string v, Licitador l)
    {
        throw new NotImplementedException();
    }
}