
using System.Data.SqlClient;
using RhythmsOfGiving.Controller;
using RhythmsOfGiving.Controller.Dados;
using RhythmsOfGiving.Controller.Excecoes;
using RhythmsOfGiving.Controller.Leiloes;

namespace RhythmsOfGiving.Controller.Dados
{
    public class LicitacaoDao
    {
        private static LicitacaoDao? _singleton = null;

        private LicitacaoDao()
        {
        }

        public static LicitacaoDao GetInstance()
        {
            if (_singleton == null)
            {
                _singleton = new LicitacaoDao();
            }

            return _singleton;
        }


        internal Licitacao Get(int idLicitacao)
        {
            Licitacao result = null;
            try
            {
                string query = "SELECT * FROM Licitacao WHERE id = @idLicitacao";

                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idLicitacao", idLicitacao);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            DateTime dataHora = reader.GetDateTime(reader.GetOrdinal("dataHora"));
                            float valor = (float)reader.GetDecimal(reader.GetOrdinal("valor"));
                            int idLicitador = reader.GetInt32(reader.GetOrdinal("idLicitador"));
                            int idLeilao = reader.GetInt32(reader.GetOrdinal("idLeilao"));

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



        internal void Put(int idLicitacao, Licitacao l)
        {
            try
            {
                string query = "MERGE INTO Licitacao AS Target\n"
                               + "USING (VALUES (@id, @dataHora, @valor, @idLicitador, @idLeilao)) AS Source (id, dataHora, valor, idLicitador, idLeilao)\n"
                               + "ON Target.id = Source.id -- Considering 'id' as the primary key\n"
                               + "WHEN MATCHED THEN\n"
                               + "    UPDATE SET\n"
                               + "        dataHora = Source.dataHora,\n"
                               + "        valor = Source.valor,\n"
                               + "        idLicitador = Source.idLicitador,\n"
                               + "        idLeilao = Source.idLeilao\n"
                               + "WHEN NOT MATCHED THEN\n"
                               + "    INSERT (id, dataHora, valor, idLicitador, idLeilao)\n"
                               + "    VALUES (Source.id, Source.dataHora, Source.valor, Source.idLicitador, Source.idLeilao);";

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
                throw new Exception("Não fui possivel fazer o put licitação");

            }
        }

        public static int Size()
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
}