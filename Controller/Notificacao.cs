
namespace RhythmsOfGiving.Controller {
    public class Notificacao {
        private readonly string title;
        private readonly string message;
        private readonly bool hasButton;
        private readonly string buttonTitle;
        private readonly DateTime date;
        
        public Notificacao(string title, string message, bool hasButton, string buttonTitle, string date) {
            this.title = title;
            this.message = message;
            this.hasButton = hasButton;
            this.buttonTitle = buttonTitle;
            this.date = DateTime.Parse(date);
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
    }
}