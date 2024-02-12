namespace RhythmsOfGiving.Controller.Excecoes{
    public class DataNascimentoMenor18 : Exception
    {
        public DataNascimentoMenor18() { }

        public DataNascimentoMenor18(string message) : base(message) { }

        public DataNascimentoMenor18(string message, Exception innerException)
            : base(message, innerException) { }
    }
}