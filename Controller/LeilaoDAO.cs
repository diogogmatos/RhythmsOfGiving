
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
}