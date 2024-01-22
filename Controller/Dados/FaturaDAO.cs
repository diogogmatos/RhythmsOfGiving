using System.Data.SqlClient;
using RhythmsOfGiving.Controller.Excecoes;
using RhythmsOfGiving.Controller.Utilizadores;

namespace RhythmsOfGiving.Controller.Dados
{
    public class FaturaDAO
    {

        private static FaturaDAO? singleton = null;

        private FaturaDAO()
        {
        }

        public static FaturaDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new FaturaDAO();
            }

            return singleton;
        }
        
        public static int size()
        {
            int totalRows = 0;

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) AS TotalRows FROM Fatura";

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
        
        public Fatura get(int id){
            Fatura fatura = null;

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString())) 
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Fatura.* , Licitador.id AS LicitadorID , Licitador.nif AS Nif, Licitador.nome AS NomeL " +
                                   "FROM Fatura " +
                                   "INNER JOIN Licitador ON Licitador.id = Fatura.idLicitador " +
                                   "WHERE Fatura.id = @IdFatura";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdFatura", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                {
                                    int idFatura = id;
                                    DateTime dataHoraEmissao = reader.GetDateTime(reader.GetOrdinal("dataHora"));
                                    int idInstituicao = reader.GetInt32(reader.GetOrdinal("idInstituicao"));
                                    string nomeLicitador = reader.GetString(reader.GetOrdinal("NomeL"));
                                    int idLicitador = reader.GetInt32(reader.GetOrdinal("LicitadorID"));
                                    int nif = reader.GetInt32(reader.GetOrdinal("Nif"));
                                    int idLicitacao = reader.GetInt32(reader.GetOrdinal("idLicitacao"));
                                    fatura = new Fatura(idFatura, dataHoraEmissao, idInstituicao, nomeLicitador, nif,
                                        idLicitacao, idLicitador);
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

            if (fatura == null)
            {
                throw new FaturaNaoEncontradaException("A fatura com o id " + id + " não existe!");
            }

            return fatura;
        } 

        public Fatura put (int id, Fatura f)
        { 
            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    string sql = "MERGE INTO Fatura AS target " +
                                 "USING (VALUES (@IdFatura, @DataHora, @IdLicitador, @IdLicitacao, @IdInstituicao)) " +
                                 "AS source (id, dataHora, idLicitador, idLicitacao, idInstituicao) " +
                                 "ON target.id = @Id " +
                                 "WHEN MATCHED THEN " +
                                 " UPDATE SET dataHora = source.dataHora, idLicitador = source.idLicitador, idLicitacao = source.idLicitacao, idInstituicao = source.idInstituicao " +
                                 "WHEN NOT MATCHED THEN " +
                                 " INSERT (id, dataHora, idLicitador, idLicitacao, idInstituicao) VALUES (source.id, source.dataHora, source.idLicitador, source.idLicitacao, source.idInstituicao);";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdFatura", f.GetIdFatura());
                        cmd.Parameters.AddWithValue("@DataHora", f.GetDataHoraEmissao());
                        cmd.Parameters.AddWithValue("@IdLicitador", f.GetIdLicitador());
                        cmd.Parameters.AddWithValue("@IdLicitacao", f.GetIdLicitacao());
                        cmd.Parameters.AddWithValue("@IdInstituicao", f.GetIdInstituicao());

                        cmd.Parameters.AddWithValue("@Id", id);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Retornar a instituição atualizada ou algo apropriado para sua lógica
                return f;
        
            }
            catch (Exception ex) {
                throw new DadosFaturaInvalidosException("Não foi possível inserir a fatura");
            }
           
        }

    }
}