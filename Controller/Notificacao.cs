
namespace RhythmsOfGiving.Controller {
    public class Notificacao {
        private int idNotificacao;
        private string title;
        private string message;
        private DateTime date;
        private int tipo; //0-> ultrapassada, 1-> vencedora, 2-> perdedora Vale a pena Enum??

        private static int contadorId = NotificacaoDAO.size();
        
        //Constutor para criar
        public Notificacao(int idNotificacao, string title, string message, DateTime date, int tipo)
        {
            this.idNotificacao = idNotificacao;
            this.title = title;
            this.message = message;
            this.date = date;
            this.tipo = tipo;
        }

        public Notificacao(string message,string title, DateTime date, int tipo)
        {
            this.message = message;
            this.title = title;
            this.date = date;
            this.tipo = tipo;
        }
        
        public int getId()
        {
            return idNotificacao;
        }

        public string getTitle() {
            return title;
        }

        public string getMessage() {
            return message;
        }



        public DateTime getDate() {
            return date;
        }

        public int getTipo()
        {
            return tipo;
        }
        
    }
}