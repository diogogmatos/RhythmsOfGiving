
using System.Data.SqlClient;
using RhythmsOfGiving.Controller;
using RhythmsOfGiving.Controller.Dados;
using RhythmsOfGiving.Controller.Excecoes;
using RhythmsOfGiving.Controller.Utilizadores;

namespace RhythmsOfGiving.Controller.Dados{
    public class NotificacaoDao
    {
        private static NotificacaoDao? _singleton = null;

        private NotificacaoDao()
        {
        }

        public static NotificacaoDao GetInstance()
        {
            if (_singleton == null)
            {
                _singleton = new NotificacaoDao();
            }

            return _singleton;
        }

        public static int Size()
        {
            int rowCount = 0;

            string query = "SELECT COUNT(*) FROM Notificação";

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                rowCount = (int)command.ExecuteScalar();
            }

            return rowCount;
        }

        public Notificacao Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
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
                                    Convert.ToInt32(reader["tipo"]),
                                    Convert.ToInt32(reader["idLeilao"]));

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
            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                connection.Open();
                try
                {
                    // Check if the record with the specified id exists
                    bool recordExists = Exite(id, connection);

                    string query;
                    if (recordExists)
                    {
                        // Update the existing record
                        query = "UPDATE Notificação " +
                                "SET titulo = @Titulo, mensagem = @Mensagem, " +
                                "idLicitador = @IdLicitador, dataHora = @DataHora, tipo = @Tipo, idLeilao = @IdLeilao " +
                                "WHERE idNotificacao = @Id";
                    }
                    else
                    {
                        // Insert a new record if the record with the specified id does not exist
                        query =
                            "INSERT INTO Notificação (idNotificacao, titulo, mensagem, idLicitador, dataHora, tipo, idLeilao) " +
                            "VALUES (@Id, @Titulo, @Mensagem, @IdLicitador, @DataHora, @Tipo, @IdLeilao)";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Titulo", n.GetTitle());
                        command.Parameters.AddWithValue("@Mensagem", n.GetMessage());
                        command.Parameters.AddWithValue("@IdLicitador", n.GetIdLicitador());
                        command.Parameters.AddWithValue("@DataHora", n.GetDate());
                        command.Parameters.AddWithValue("@Tipo", n.GetTipo());
                        command.Parameters.AddWithValue("@IdLeilao", n.GetIdLeilao());

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

        private bool Exite(int id, SqlConnection connection)
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


        public List<Notificacao> getNotificacoesLicitador(int idLicitador)
        {
            List<Notificacao> notificacoes = new List<Notificacao>();

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                connection.Open();
                try
                {

                    string query = "SELECT * FROM Notificação WHERE idLicitador = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", idLicitador);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Notificacao notification = new Notificacao(
                                    Convert.ToInt32(reader["idNotificacao"]),
                                    Convert.ToString(reader["titulo"]),
                                    Convert.ToString(reader["mensagem"]),
                                    Convert.ToInt32(reader["idLicitador"]),
                                    Convert.ToDateTime(reader["dataHora"]),
                                    Convert.ToInt32(reader["tipo"]),
                                    Convert.ToInt32(reader["idLeilao"]));

                                notificacoes.Add(notification);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Lidar com exceções, se necessário
                    Console.WriteLine("Erro ao obter notificações: " + ex.Message);
                }
            }

            return notificacoes;
        }

    }
}