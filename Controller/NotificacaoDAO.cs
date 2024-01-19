
using System.Data.SqlClient;
using RhythmsOfGiving.Controller;   
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
        
        public static int size()
        {
            int rowCount = 0;
            
            string query = "SELECT COUNT(*) FROM Notificação";

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                rowCount = (int)command.ExecuteScalar();
            }
            return rowCount;
        }
        
         public Notificacao get(int id)
        {
            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                connection.Open();
                try
                {

                    string query = "SELECT * FROM Notificação WHERE idNotificacao = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Notificacao notification = new Notificacao(
                                    Convert.ToInt32(reader["idNotificacao"]),
                                    Convert.ToString(reader["titulo"]),
                                    Convert.ToString(reader["mensagem"]),
                                    Convert.ToInt32(reader["idLicitador"]),
                                    Convert.ToDateTime(reader["dataHora"]),
                                    Convert.ToInt32(reader["tipo"]));

                                return notification;
                            }
                        }
                    }
                }
                catch
                {
                    throw new NaoExistemNotificacao("Não existe notificação para o id=" + id);
                }
            }

            return null;
        }
         
         public void Put(int id, Notificacao n)
{
    using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
    {
        connection.Open();
        try
        {
            // Check if the record with the specified id exists
            bool recordExists = exite(id, connection);

            string query;
            if (recordExists)
            {
                // Update the existing record
                query = "UPDATE Notificação " +
                        "SET titulo = @Titulo, mensagem = @Mensagem, " +
                        "idLicitador = @IdLicitador, dataHora = @DataHora, tipo = @Tipo " +
                        "WHERE idNotificacao = @Id";
            }
            else
            {
                // Insert a new record if the record with the specified id does not exist
                query = "INSERT INTO Notificação (idNotificacao, titulo, mensagem, idLicitador, dataHora, tipo) " +
                        "VALUES (@Id, @Titulo, @Mensagem, @IdLicitador, @DataHora, @Tipo)";
            }

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Titulo", n.getTitle());
                command.Parameters.AddWithValue("@Mensagem", n.getMessage());
                command.Parameters.AddWithValue("@IdLicitador", n.getIdLicitador());
                command.Parameters.AddWithValue("@DataHora", n.getDate());
                command.Parameters.AddWithValue("@Tipo", n.getTipo());

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately, e.g., log or throw a custom exception
            throw new Exception("Não foi possível fazer o put da notificação " + id);
        }
    }
}

    private bool exite(int id, SqlConnection connection)
    {
        // Check if the record with the specified id exists in the database
        string checkQuery = "SELECT COUNT(*) FROM Notificação WHERE idNotificacao = @Id";
        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
        {
            checkCommand.Parameters.AddWithValue("@Id", id);
            int count = (int)checkCommand.ExecuteScalar();
            return count > 0;
        }
    }


}