
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

    internal Licitador get(string email)
    {
        throw new NotImplementedException();
    }
}