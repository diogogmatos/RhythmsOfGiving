
using RhythmsOfGiving.Controller;

public class LeilaoDAO{
    private static LeilaoDAO? singleton = null;
        private LeilaoDAO() { }

        public static LeilaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LeilaoDAO();
            }
            return singleton;
        }

        public Leilao get(int idLeilao){
            throw new LeilaoNaoExiste("O leilão de id" + idLeilao + "não existe!");
            
        }
}