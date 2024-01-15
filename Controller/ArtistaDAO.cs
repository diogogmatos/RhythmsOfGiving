using System.Drawing;

namespace RhythmsOfGiving.Controller{
public class ArtistaDAO{
    private static ArtistaDAO? singleton = null;
        private ArtistaDAO() { }

        public static ArtistaDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new ArtistaDAO();
            }
            return singleton;
        }

        public static int size(){
            return 0; // depois usar a query necessária
        }

       public Licitador get(string nome){
            //falta definir a lógica
            throw new ArtistaNaoExisteException("O artista com o nome, " + nome + " não existe!");
       } 
}
}