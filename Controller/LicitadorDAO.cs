using System.Drawing;

namespace RhythmsOfGiving.Controller{
public class LicitadorDAO{
    private static LicitadorDAO? singleton = null;
        private LicitadorDAO() { }

        public static LicitadorDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LicitadorDAO();
            }
            return singleton;
        }

        public static int size(){
            return 0; // depois usar a query necessária
        }

        
}
}