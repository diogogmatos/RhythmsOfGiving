
using RhythmsOfGiving.Controller.Dados;

namespace RhythmsOfGiving.Controller {
    public class Notificacao {
        private int idNotificacao;
        private string title;
        private string message;
        private int idLicatdor;
        private DateTime date;
        private int tipo; //0-> ultrapassada, 1-> vencedora, 2-> perdedora Vale a pena Enum??
        private int idLeilao;

        private static int _contadorId = NotificacaoDao.Size();
        
        //Constutor para criar

        public Notificacao(int idNotificacao, string title, string message, int idLicatdor, DateTime date, int tipo, int idLeilao)
        {
            this.idNotificacao = idNotificacao;
            this.title = title;
            this.message = message;
            this.idLicatdor = idLicatdor;
            this.date = date;
            this.tipo = tipo;
            this.idLeilao = idLeilao;
        }

        public Notificacao(string title, string message, int idLicatdor, DateTime date, int tipo, int idLeilao)
        {
            this.idNotificacao = ++_contadorId;
            this.title = title;
            this.message = message;
            this.idLicatdor = idLicatdor;
            this.date = date;
            this.tipo = tipo;
            this.idLeilao = idLeilao;
        }


        public int GetId()
        {
            return idNotificacao;
        }

        public string GetTitle() {
            return title;
        }

        public string GetMessage() {
            return message;
        }



        public DateTime GetDate() {
            return date;
        }

        public int GetIdLicitador()
        {
            return this.idLicatdor;
        }

        public int GetTipo()
        {
            return tipo;
        }

        public int GetIdLeilao()
        {
            return idLeilao;
        }
    }
}