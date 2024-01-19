
namespace RhythmsOfGiving.Controller {
    public class Notificacao {
        private bool hasButton; // tirar
        private string buttonTitle; // tirar
        private int idNotificacao;
        private string title;
        private string message;
        private DateTime date;
        private int tipo; //0-> ultrapassada, 1-> vencedora, 2-> perdedora Vale a pena Enum??

        private static int contadorId = NotificacaoDAO.size();
        
        //Constutor para criar
        public Notificacao(string title, string message, bool hasButton, string buttonTitle, string date, int tipo)
        {
            this.idNotificacao = ++contadorId;
            this.title = title;
            this.message = message;
            this.hasButton = hasButton;
            this.buttonTitle = buttonTitle;
            this.date = DateTime.Parse(date);
            this.tipo = tipo;
        }
        
        //Construtor para get DAO
        public Notificacao(int idNotificacao, string title, string message, bool hasButton, string buttonTitle, DateTime date, int tipo)
        {
            this.idNotificacao = idNotificacao;
            this.title = title;
            this.message = message;
            this.hasButton = hasButton;
            this.buttonTitle = buttonTitle;
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

        public bool getHasButton() {
            return hasButton;
        }

        public string getButtonTitle() {
            return buttonTitle;
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