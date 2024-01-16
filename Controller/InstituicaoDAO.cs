
public class InstituicaoDAO{
     private static InstituicaoDAO? singleton = null;
        private InstituicaoDAO() { }

        public static InstituicaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new InstituicaoDAO();
            }
            return singleton;
        }
        
        public static int size(){
            return 0; // depois usar a query necessária
        }
}