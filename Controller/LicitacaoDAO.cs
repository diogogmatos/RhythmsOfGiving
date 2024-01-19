
using System.Data.SqlClient;
using RhythmsOfGiving.Controller;

public class LicitacaoDAO{
    private static LicitacaoDAO? singleton = null;
        private LicitacaoDAO() { }

        public static LicitacaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LicitacaoDAO();
            }
            return singleton;
        }

    
        internal Licitacao get(int idLicitacao)
        {
            // Create a new instance of Licitacao to hold the result
            Licitacao result = null;
            try
            {
                // SQL query to retrieve Licitacao by idLicitacao
                string query = "SELECT * FROM Licitacao WHERE id = @idLicitacao";

                // Create a SqlConnection and a SqlCommand
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the parameter for idLicitacao
                    command.Parameters.AddWithValue("@idLicitacao", idLicitacao);

                    // Open the connection
                    connection.Open();

                    // Execute the query
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there are any rows
                        if (reader.Read())
                        {
                            // Retrieve values from the database
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            DateTime dataHora = reader.GetDateTime(reader.GetOrdinal("dataHora"));
                            float valor = (float)reader.GetDecimal(reader.GetOrdinal("valor"));
                            int idLicitador = reader.GetInt32(reader.GetOrdinal("idLicitador"));
                            int idLeilao = reader.GetInt32(reader.GetOrdinal("idLeilao"));

                            // Create a new Licitacao object
                            result = new Licitacao(id, dataHora, valor, idLeilao, idLicitador);
                        }
                    }
                }
            }
            catch
            {
                 throw new LicitacaoNaoExisteException("A licitação que se deseja tirar não existe" + idLicitacao);
            }

            return result;
        }

        

        internal void put(int idLicitacao, Licitacao l)
        {
            try
            {
                // SQL query to insert a new Licitacao
                string query =  @"
                        MERGE INTO Licitacao AS target
                        USING (VALUES (@id, @dataHora, @valor, @idLicitador, @idLeilao)) AS source (id, dataHora, valor, idLicitador, idLeilao)
                        ON target.id = source.id
                        WHEN MATCHED THEN
                            UPDATE SET
                                dataHora = source.dataHora,
                                valor = source.valor,
                                idLicitador = source.idLicitador,
                                idLeilao = source.idLeilao
                        WHEN NOT MATCHED THEN
                            INSERT (id, dataHora, valor, idLicitador, idLeilao)
                            VALUES (source.id, source.dataHora, source.valor, source.idLicitador, source.idLeilao);";
                // Create a SqlConnection and a SqlCommand
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters for the new Licitacao
                    command.Parameters.AddWithValue("@id", idLicitacao);
                    command.Parameters.AddWithValue("@dataHora", l.GetDataHora());
                    command.Parameters.AddWithValue("@valor", l.GetValor());
                    command.Parameters.AddWithValue("@idLicitador", l.GetIdLicitacao());
                    command.Parameters.AddWithValue("@idLeilao", l.GetIdLeilao());

                    // Open the connection
                    connection.Open();

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception();

            }
        }
    
        public static int size()
        {
            int rowCount = 0;

            try
            {
                string query = "SELECT COUNT(*) FROM Licitacao";

                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    rowCount = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
            }

            return rowCount;
        }
}