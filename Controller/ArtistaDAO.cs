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
            int totalRows = 0;

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) AS TotalRows FROM Artista";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        totalRows = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return totalRows;
        }
        
       public Artista get(int idArtista){
           
           Artista artista = null;

           using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString())) 
           {
               try
               {
                   connection.Open();

                   string query = "SELECT id, imagem, nome, idAdministrador FROM Artista WHERE id = @IdArtista";

                   using (SqlCommand command = new SqlCommand(query, connection))
                   {
                       command.Parameters.AddWithValue("@IdArtista", idArtista);

                       using (SqlDataReader reader = command.ExecuteReader())
                       {
                           if (reader.Read())
                           {
                               {
                                   int id = reader.GetInt32(reader.GetOrdinal("id"));
                                   string imagem = reader.GetString(reader.GetOrdinal("imagem"));
                                   string nome = reader.GetString(reader.GetOrdinal("nome"));
                                   int idAdministrador = reader.GetInt32(reader.GetOrdinal("idAdministrador"));
                                   artista = new Artista(id, imagem, nome, idAdministrador);
                                   // Preencha outros campos do objeto GeneroMusical conforme necessário
                               };
                           }
                       }
                   }
               }
               catch (Exception ex)
               {
                   // Trate exceções conforme necessário (registre, relance, etc.)
                   Console.WriteLine("Error: " + ex.Message);
               }
           }

           if (artista == null)
           {
               throw new ArtistaNaoExisteException("O artista com o id, " + idArtista + " não existe!");
           }

           return artista;
       } 

       public Artista put (int id, Artista a)
       {
           using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
           {
               connection.Open();
                
               string sql = "MERGE INTO Artista AS target " +
                            "USING (VALUES (@IdArtista, @Imagem, @Nome, @IdAdmin)) " +
                            "AS source (id, imagem, nome, idAdministrador) " +
                            "ON target.id = @Id " +
                            "WHEN MATCHED THEN " +
                            " UPDATE SET imagem = source.imagem, nome = source.nome, idAdministrador = source.idAdministrador " +
                            "WHEN NOT MATCHED THEN " +
                            " INSERT (id, imagem, nome, idAdministrador) VALUES (source.id, source.imagem, source.nome, source.idAdministrador);";

               using (SqlCommand cmd = new SqlCommand(sql, connection))
               {
                   cmd.Parameters.AddWithValue("@IdArtista", a.getIdArtista());
                   cmd.Parameters.AddWithValue("@Imagem", a.getImagem());
                   cmd.Parameters.AddWithValue("@Nome", a.getNome());
                   cmd.Parameters.AddWithValue("@IdAdmin", a.getIdAdmin());
                    
                   cmd.Parameters.AddWithValue("@Id", id);
                    

                   cmd.ExecuteNonQuery();
               }
           }

           return a;
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