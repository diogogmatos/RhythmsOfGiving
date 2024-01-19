
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
        
        public static int size(){
            return 0; // depois usar a query necessária
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


        public Notificacao put (int id, Notificacao n)
        {
            //falta definir a lógica
            return null;
        }
}