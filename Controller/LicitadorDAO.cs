using System.Data.SqlClient;
using System.Drawing;

namespace RhythmsOfGiving.Controller
{
    public class LicitadorDAO
    {
        private static LicitadorDAO? singleton = null;

        private LicitadorDAO()
        {
        }

        public static LicitadorDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LicitadorDAO();
            }

            return singleton;
        }

        public static int size()
        {
            return 0; // depois usar a query necessária
        }

    internal Licitador get(string email)
    {
        using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
        {
            connection.Open();
            try
            {

                string sql = "SELECT * FROM Licitador WHERE email = @Email";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idLicitador = Convert.ToInt32(reader["id"]);
                            String nome = Convert.ToString(reader["nome"]);
                            String palavraPasse = Convert.ToString(reader["palavraPasse"]);
                            DateOnly dataNascimento =
                                DateOnly.FromDateTime(Convert.ToDateTime(reader["dataNascimento"]));
                            int nrCartao = Convert.ToInt32(reader["nrCartao"]);
                            int nif = Convert.ToInt32(reader["nif"]);
                            Int64 nCC = Convert.ToInt64(reader["numeroCC"]);
                            HashSet<int> minhasLicitacoes = new HashSet<int>();
                            HashSet<int> minhasFaturas = new HashSet<int>();



                            string sql2 = "SELECT * FROM Licitacao  WHERE  idLicitador= @IdLicitador ";
                            using (SqlCommand command2 = new SqlCommand(sql2, connection))
                            {
                                command2.Parameters.AddWithValue("@IdLicitador", idLicitador);

                                using (SqlDataReader readerLicitacao = command2.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        minhasLicitacoes.Add(Convert.ToInt32(readerLicitacao["idLicitador"]));
                                    }
                                }
                            }

                            string sql3 = "SELECT * FROM Fatura WHERE idLicitador= @IdLicitador ";
                            using (SqlCommand command3 = new SqlCommand(sql3, connection))
                            {
                                command3.Parameters.AddWithValue("@IdLicitador", idLicitador);

                                using (SqlDataReader readerFatura = command3.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        minhasFaturas.Add(Convert.ToInt32(readerFatura["idLicitador"]));
                                    }
                                }
                            }

                            Licitador licitador = new Licitador(idLicitador, nome, palavraPasse, dataNascimento,
                                nrCartao, email, nif,
                                nCC, minhasLicitacoes, minhasFaturas);

                            return licitador;
                        }
                    }
                }
            }
            catch
            {
                throw new LicitadorNaoExisteException($"O licitador com o email '{email}' não existe!");
            }
        }

        return null;
    }

        public Licitador get(int id)
        {
         
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                connection.Open();
                try
                {

                    string sql = "SELECT * FROM Licitador WHERE id = @Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idLicitador = Convert.ToInt32(reader["id"]);
                                String nome = Convert.ToString(reader["nome"]);
                                String email = Convert.ToString(reader["email"]);
                                String palavraPasse = Convert.ToString(reader["palavraPasse"]);
                                DateOnly dataNascimento =
                                    DateOnly.FromDateTime(Convert.ToDateTime(reader["dataNascimento"]));
                                int nrCartao = Convert.ToInt32(reader["nrCartao"]);
                                int nif = Convert.ToInt32(reader["nif"]);
                                Int64 nCC = Convert.ToInt64(reader["numeroCC"]);
                                HashSet<int> minhasLicitacoes = new HashSet<int>();
                                HashSet<int> minhasFaturas = new HashSet<int>();



                                string sql2 = "SELECT * FROM Licitacao  WHERE  idLicitador= @IdLicitador ";
                                using (SqlCommand command2 = new SqlCommand(sql2, connection))
                                {
                                    command2.Parameters.AddWithValue("@IdLicitador", idLicitador);

                                    using (SqlDataReader readerLicitacao = command2.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            minhasLicitacoes.Add(Convert.ToInt32(readerLicitacao["idLicitador"]));
                                        }
                                    }
                                }

                                string sql3 = "SELECT * FROM Fatura WHERE idLicitador= @IdLicitador ";
                                using (SqlCommand command3 = new SqlCommand(sql3, connection))
                                {
                                    command3.Parameters.AddWithValue("@IdLicitador", idLicitador);

                                    using (SqlDataReader readerFatura = command3.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            minhasFaturas.Add(Convert.ToInt32(readerFatura["idLicitador"]));
                                        }
                                    }
                                }

                                Licitador licitador = new Licitador(idLicitador, nome, palavraPasse, dataNascimento,
                                    nrCartao, email, nif,
                                    nCC, minhasLicitacoes, minhasFaturas);

                                return licitador;
                            }
                        }
                    }
                }
                catch
                {
                    throw new LicitadorNaoExisteException("O licitador com o id " + id + " não existe!");
                }

            }
            return null;
        }

       internal void put(string email, Licitador licitador)
        {
            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                connection.Open();
                
                    try
                    {
                        string mergeSql = "MERGE INTO Licitador AS target " +
                                          "USING (VALUES (@Email)) AS source (Email) " +
                                          "ON target.Email = source.Email " +
                                          "WHEN MATCHED THEN " +
                                          "    UPDATE SET " +
                                          "        nome = @Nome, " +
                                          "        palavraPasse = @PalavraPasse, " +
                                          "        dataNascimento = @DataNascimento, " +
                                          "        nrCartao = @NrCartao, " +
                                          "        nif = @Nif, " +
                                          "        numeroCC = @NumeroCC " +
                                          "WHEN NOT MATCHED THEN " +
                                          "    INSERT (nome, palavraPasse, dataNascimento, nrCartao, nif, numeroCC, email) " +
                                          "    VALUES (@Nome, @PalavraPasse, @DataNascimento, @NrCartao, @Nif, @NumeroCC, @Email);";

                        using (SqlCommand updateCommand = new SqlCommand(mergeSql, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Nome", licitador.getNome());
                            updateCommand.Parameters.AddWithValue("@PalavraPasse", licitador.getPalavraPasse());
                            updateCommand.Parameters.AddWithValue("@DataNascimento", new DateTime(licitador.getDataNascimento().Year, licitador.getDataNascimento().Month, licitador.getDataNascimento().Day));
                            updateCommand.Parameters.AddWithValue("@NrCartao", licitador.getNrCartao());
                            updateCommand.Parameters.AddWithValue("@Nif", licitador.getNIF());
                            updateCommand.Parameters.AddWithValue("@NumeroCC", licitador.getNcc());
                            updateCommand.Parameters.AddWithValue("@Email", email);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DadosInvalidosException("Erro ao atualizar o licitador.", ex);
                    }
            }
        }


        public bool VerificarUnicoNumeroCC(int novoNumeroCC)
        {
            bool numeroCCUnico = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Licitador WHERE numeroCC = @NovoNumeroCC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int count = (int)command.ExecuteScalar();

                        numeroCCUnico = count == 0;
                    }
                }
            }
            catch (VerificarNãoUnicoNumeroCcException ex)
            {
                throw new VerificarNãoUnicoNumeroCcException("O NumeroCC já existe na base de dados");
            }

            return numeroCCUnico;
        }

        public IEnumerable<int> keySet()
        {
            List<int> keySet = new List<int>();

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                connection.Open();

                string query = "SELECT id FROM Licitador";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            keySet.Add(id);
                        }
                    }
                }
            }
            return keySet;
        }
    }
}