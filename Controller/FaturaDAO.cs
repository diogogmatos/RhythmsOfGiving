

namespace RhythmsOfGiving.Controller
{
    public class FaturaDAO
    {

        private static FaturaDAO? singleton = null;

        private FaturaDAO()
        {
        }

        public static FaturaDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new FaturaDAO();
            }

            return singleton;
        }
        
        public static int size()
        {
            return 0; // depois usar a query necessária
        }
        
        public Fatura get(int id){
            //falta definir a lógica
            return null;
        } 

        public Fatura put (int id, Fatura f)
        {
            //falta definir a lógica
            return null;
        }

    }
}