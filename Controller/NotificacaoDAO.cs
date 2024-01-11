
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
}