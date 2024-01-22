using System.Data.SqlClient;
using RhythmsOfGiving.Controller.Excecoes;
using RhythmsOfGiving.Controller.Utilizadores;

namespace RhythmsOfGiving.Controller.Dados
{
    public class LicitadorDao
    {
        private static LicitadorDao? _singleton = null;

        private LicitadorDao()
        {
        }

        public static LicitadorDao GetInstance()
        {
            if (_singleton == null)
            {
                _singleton = new LicitadorDao();
            }

            return _singleton;
        }

        public static int Size()
        {
            int totalRows = 0;

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) AS TotalRows FROM Licitador";

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

        internal Licitador Get(string email)
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
                                DateOnly dataNascimento = DateOnly.FromDateTime(Convert.ToDateTime(reader["dataNascimento"]));
                                int nrCartao = Convert.ToInt32(reader["nrCartao"]);
                                int nif = Convert.ToInt32(reader["nif"]);
                                Int64 nCc = Convert.ToInt64(reader["numeroCC"]);
                                HashSet<int> minhasLicitacoes = new HashSet<int>();
                                HashSet<int> minhasFaturas = new HashSet<int>();

                                reader.Close();

                                string sql2 = "SELECT * FROM Licitacao WHERE idLicitador = @IdLicitador";
                                using (SqlCommand command2 = new SqlCommand(sql2, connection))
                                {
                                    command2.Parameters.AddWithValue("@IdLicitador", idLicitador);

                                    using (SqlDataReader readerLicitacao = command2.ExecuteReader())
                                    {
                                        while (readerLicitacao.Read())
                                        {
                                            minhasLicitacoes.Add(Convert.ToInt32(readerLicitacao["id"]));
                                        }
                                    }
                                }

                                // Close the second SqlDataReader before executing the next query
                                reader.Close();

                                string sql3 = "SELECT * FROM Fatura WHERE idLicitador = @IdLicitador";
                                using (SqlCommand command3 = new SqlCommand(sql3, connection))
                                {
                                    command3.Parameters.AddWithValue("@IdLicitador", idLicitador);

                                    using (SqlDataReader readerFatura = command3.ExecuteReader())
                                    {
                                        while (readerFatura.Read())
                                        {
                                            minhasFaturas.Add(Convert.ToInt32(readerFatura["id"]));
                                        }
                                    }
                                }

                                Licitador licitador = new Licitador(idLicitador, nome, palavraPasse, dataNascimento,
                                    nrCartao, email, nif, nCc, minhasLicitacoes, minhasFaturas);

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
            
            throw new LicitadorNaoExisteException($"O licitador com o email '{email}' não existe!");
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
                                Int64 nCc = Convert.ToInt64(reader["numeroCC"]);
                                HashSet<int> minhasLicitacoes = new HashSet<int>();
                                HashSet<int> minhasFaturas = new HashSet<int>();

                                // Close the first SqlDataReader before executing the next query
                                reader.Close();

                                string sql2 = "SELECT * FROM Licitacao  WHERE  Licitacao.idLicitador = @IdLicitador ";
                                using (SqlCommand command2 = new SqlCommand(sql2, connection))
                                {
                                    command2.Parameters.AddWithValue("@IdLicitador", id);

                                    using (SqlDataReader readerLicitacao = command2.ExecuteReader())
                                    {
                                        while (readerLicitacao.Read())
                                        {
                                            minhasLicitacoes.Add(Convert.ToInt32(readerLicitacao["id"]));
                                        }
                                    }
                                }

                                // Close the second SqlDataReader before executing the next query
                                reader.Close();

                                string sql3 = "SELECT * FROM Fatura WHERE idLicitador= @IdLicitador ";
                                using (SqlCommand command3 = new SqlCommand(sql3, connection))
                                {
                                    command3.Parameters.AddWithValue("@IdLicitador", id);

                                    using (SqlDataReader readerFatura = command3.ExecuteReader())
                                    {
                                        while (readerFatura.Read())
                                        {
                                            minhasFaturas.Add(Convert.ToInt32(readerFatura["id"]));
                                        }
                                    }
                                }

                                Licitador licitador = new Licitador(idLicitador, nome, palavraPasse, dataNascimento,
                                    nrCartao, email, nif,
                                    nCc, minhasLicitacoes, minhasFaturas);

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

            return null; // mudar
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
                                          "        id = @Id, " +
                                          "        nome = @Nome, " +
                                          "        palavraPasse = @PalavraPasse, " +
                                          "        dataNascimento = @DataNascimento, " +
                                          "        nrCartao = @NrCartao, " +
                                          "        nif = @Nif, " +
                                          "        numeroCC = @NumeroCC " +
                                          "WHEN NOT MATCHED THEN " +
                                          "    INSERT (id, nome, palavraPasse, dataNascimento, nrCartao, nif, numeroCC, email) " +
                                          "    VALUES (@Id, @Nome, @PalavraPasse, @DataNascimento, @NrCartao, @Nif, @NumeroCC, @Email);";


                        using (SqlCommand updateCommand = new SqlCommand(mergeSql, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@id", licitador.GetIdLicitador());
                            updateCommand.Parameters.AddWithValue("@Nome", licitador.GetNome());
                            updateCommand.Parameters.AddWithValue("@PalavraPasse", licitador.GetPalavraPasse());
                            updateCommand.Parameters.AddWithValue("@DataNascimento", new DateTime(licitador.GetDataNascimento().Year, licitador.GetDataNascimento().Month, licitador.GetDataNascimento().Day));
                            updateCommand.Parameters.AddWithValue("@NrCartao", licitador.GetNrCartao());
                            updateCommand.Parameters.AddWithValue("@Nif", licitador.GetNif());
                            updateCommand.Parameters.AddWithValue("@NumeroCC", licitador.GetNcc());
                            updateCommand.Parameters.AddWithValue("@Email", email);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new DadosInvalidosException("Informações inválidas, verifique os dados introduzidos.", ex);
                    }
            }
        }


        public bool VerificarUnicoNumeroCc(int novoNumeroCc)
        {
            bool numeroCcUnico = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Licitador WHERE numeroCC = @NovoNumeroCC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NovoNumeroCC", novoNumeroCc);
                        int count = (int)command.ExecuteScalar();

                        numeroCcUnico = count == 0;
                    }
                }
            }
            catch (VerificarNãoUnicoNumeroCcException ex)
            {
                throw new VerificarNãoUnicoNumeroCcException("O NumeroCC já existe na base de dados");
            }

            return numeroCcUnico;
        }
        
        public IEnumerable<int> KeySet()
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
        
        public static int SizeAdmin()
        {
            int totalRows = 0;
            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                Console.WriteLine("depois conexao");
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) AS TotalRows FROM Administrador";

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
        
        public Administrador GetAdmin(int idAdmin)
        {
            Administrador a = null;

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString())) 
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id, palavraPasse, email FROM Administrador WHERE id = @IdAdmin";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdAdmin", idAdmin);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                {
                                    int id = reader.GetInt32(reader.GetOrdinal("id"));
                                    string palavraPasse = reader.GetString(reader.GetOrdinal("palavraPasse"));
                                    string email = reader.GetString(reader.GetOrdinal("email"));
                                    
                                    a = new Administrador(id, palavraPasse, email);
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

            return a;
        }

        //DUVIDA:SABER SE O ID COMEÇA POR ZERO OU NÃO
        public Dictionary<int, Administrador> preencherAdmins()
        {
            Console.WriteLine("entrou no preencher");
            int tamanho = SizeAdmin();
            Console.WriteLine("Tamanho: " + tamanho);
            Dictionary<int, Administrador> resultado = new Dictionary<int, Administrador>();

            for (int i = 0; i < tamanho; i++)
            {
                Administrador a = GetAdmin(i+1);
                resultado.Add(i+1, a);
            }

            return resultado;
        }
    }
}