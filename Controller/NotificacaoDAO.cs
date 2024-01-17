
using RhythmsOfGiving.Controller;

public class NotificacaoDAO{
    private static NotificacaoDAO? singleton = null;
        private NotificacaoDAO() { }

        public static NotificacaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new NotificacaoDAO();
            }
            return singleton;
        }
        
        public static int size(){
            return 0; // depois usar a query necessária
        }

        public Notificacao get(int id){
            //falta definir a lógica
            return null;
        } 

        public Notificacao put (int id, Notificacao n)
        {
            //falta definir a lógica
            return null;
        }
}