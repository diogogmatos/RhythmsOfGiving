
using System;
using System.Data.SqlClient;
using RhythmsOfGiving.Controller;

namespace RhythmsOfGiving.Controller
{
    public class InstituicaoDAO
    {
        private static InstituicaoDAO? singleton = null;

        private InstituicaoDAO()
        {
        }

        public static InstituicaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new InstituicaoDAO();
            }
        }

        public static int size()
        {
            int totalRows = 0;

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) AS TotalRows FROM Instituicao";

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

        internal List<int> keySet()
        {
            List<int> instituicoesIds = new List<int>();

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id FROM Instituicao";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int instituicaoID = Convert.ToInt32(reader["id"]);
                                instituicoesIds.Add(instituicaoID);
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

            return instituicoesIds;
        }
        
        public Instituicao get(int idInstituicao)
        {

          using SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString());
          connection.Open();

          string query = "SELECT * FROM Instituicao WHERE id = @id";
          using SqlCommand command = new SqlCommand(query, connection);
          command.Parameters.AddWithValue("@id", idInstituicao);

          using SqlDataReader reader = command.ExecuteReader();
          if (reader.Read())
          {
              int id = reader.GetInt32(reader.GetOrdinal("id"));
              string nome = reader.GetString(reader.GetOrdinal("nome"));
              string descricao = reader.GetString(reader.GetOrdinal("descricao"));
              string logoPath = reader.GetString(reader.GetOrdinal("logotipo"));
              string link = reader.GetString(reader.GetOrdinal("hiperligacao"));
              string iban = reader.GetString(reader.GetOrdinal("iban"));
              int idAdmin = reader.GetInt32(reader.GetOrdinal("idAdministrador"));
              return new Instituicao(id,nome,descricao,logoPath,link,iban,idAdmin);
          }
          else
          {

              return null; // Retorna null se a instituição não for encontrada
          }
    }

    public Instituicao put(int id, Instituicao instituicao)
    {

        using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
        {
            connection.Open();
                
            string sql = "MERGE INTO Instituicao AS target " +
                         "USING (VALUES (@IdInstituicao, @Nome, @Descricao, @Logotipo, @Hiperligacao, @IdAdmin, @Iban)) " +
                         "AS source (id, nome, descricao, logotipo, hiperligacao, idAdministrador, iban) " +
                         "ON target.id = @Id " +
                         "WHEN MATCHED THEN " +
                         " UPDATE SET nome = source.nome, descricao = source.descricao, logotipo = source.logotipo, hiperligacao = source.hiperligacao, " +
                         " idAdministrador = source.idAdministrador, iban = source.iban " +
                         "WHEN NOT MATCHED THEN " +
                         " INSERT (id, nome, descricao, logotipo, hiperligacao, idAdministrador, iban) VALUES (source.id, source.nome, source.descricao, " +
                         " source.logotipo, source.hiperligacao, source.idAdministrador, source.iban);";
    
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@IdInstituicao", instituicao.getId());
                cmd.Parameters.AddWithValue("@Nome", instituicao.getNome());
                cmd.Parameters.AddWithValue("@Descricao", instituicao.getDescricao());
                cmd.Parameters.AddWithValue("@Logotipo", instituicao.getLogoPath());
                cmd.Parameters.AddWithValue("@Hiperligacao", instituicao.getLink());
                cmd.Parameters.AddWithValue("@IdAdmin", instituicao.getIdAdmin());
                cmd.Parameters.AddWithValue("@Iban", instituicao.getIban());
                cmd.Parameters.AddWithValue("@Id", id);
                    

                cmd.ExecuteNonQuery();
            }
        }

        return instituicao;
    }

        public bool existeInstituicao(string nome)
        {
            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Instituicao WHERE nome = @Nome";

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