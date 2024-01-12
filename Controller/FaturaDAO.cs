
public class FaturaDAO{

    private static FaturaDAO? singleton = null;
        private FaturaDAO() { }

        public static FaturaDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new FaturaDAO();
            }
            return singleton;
        }

}