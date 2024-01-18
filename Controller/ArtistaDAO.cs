using System.Data.SqlClient;
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

       public Artista get(string nome){
            //falta definir a lógica
            throw new ArtistaNaoExisteException("O artista com o nome, " + nome + " não existe!");
       }
       public Artista get(int nome){
           //falta definir a lógica
           throw new ArtistaNaoExisteException("O artista com o nome, " + nome + " não existe!");
       } 

       public Artista put (int id, Artista a)
       {
           //falta definir a lógica
           return null;
       }

       public bool existeArtista(string nome)
       {
           using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
           {
               try
               {
                   connection.Open();

                   string query = "SELECT COUNT(*) FROM Artista WHERE nome = @Nome";
                
                   using (SqlCommand command = new SqlCommand(query, connection))
                   {
                       command.Parameters.AddWithValue("@Nome", nome);

                       int count = Convert.ToInt32(command.ExecuteScalar());

                       if (count > 0)
                       {
                           return true; // já existe artista na base de dados
                       }
                       else
                       {
                           return false; // Não existe
                       }
                   }
               }
               catch (Exception ex)
               {
                   // Trate a exceção conforme necessário, ou apenas a lance novamente.
                   throw;
               }
           }
       }
}
}