
using System.Data;
using System.Data.SqlClient;
using RhythmsOfGiving.Controller;

namespace RhythmsOfGiving.Controller
{
    public class LeilaoDAO
    {
        private static LeilaoDAO? singleton = null;

        private LeilaoDAO()
        {
        }

        public static LeilaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LeilaoDAO();
            }

            return singleton;
        }

        public Leilao put(int id, Leilao l)
        {
            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                connection.Open();

                Experiencia e = l.Experiencia;
                GeneroMusical g = e.GetGeneroMusical();

                string sql = "MERGE INTO Leilao AS target " +
                             "USING (VALUES (@IdLeilao, @Titulo, @ValorAtual, @ValorMinimo, @DataHoraFinal, " +
                             "@Localizacao, @Ativo, @TipoLeilao, @Descricao, @Imagem, @IdArtista, @IdGenero, " +
                             "@IdInstituicao, @IdAdministrador)) " +
                             "AS source (id, titulo, valorAtual, valorMinimo, dataHoraFinal, " +
                             "localizacao, estado, tipoLeilao, descricao, imagem, idArtista, idGeneroMusical, " +
                             "idInstituicao, idAdministrador) " +
                             "ON target.id = @Id " +
                             "WHEN MATCHED THEN " +
                             "    UPDATE SET " +
                             "    titulo = source.titulo, " +
                             "    valorAtual = source.valorAtual, " +
                             "    valorMinimo = source.valorMinimo, " +
                             "    dataHoraFinal = source.dataHoraFinal, " +
                             "    localizacao = source.localizacao, " +
                             "    estado = source.estado, " +
                             "    tipoLeilao = source.tipoLeilao, " +
                             "    descricao = source.descricao, " +
                             "    imagem = source.imagem, " +
                             "    idArtista = source.idArtista, " +
                             "    idGeneroMusical = source.idGeneroMusical, " +
                             "    idInstituicao = source.idInstituicao, " +
                             "    idAdministrador = source.idAdministrador " +
                             "WHEN NOT MATCHED THEN " +
                             "    INSERT (id, titulo, valorAtual, valorMinimo, dataHoraFinal, " +
                             "    localizacao, estado, tipoLeilao, descricao, imagem, idArtista, idGeneroMusical, " +
                             "    idInstituicao, idAdministrador) " +
                             "    VALUES (source.id, source.titulo, source.valorAtual, source.valorMinimo, " +
                             "    source.dataHoraFinal, source.localizacao, source.estado, source.tipoLeilao, " +
                             "    source.descricao, source.imagem, source.idArtista, source.idGeneroMusical, " +
                             "    source.idInstituicao, source.idAdministrador);";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@IdLeilao", l.IdLeilao);
                    cmd.Parameters.AddWithValue("@Titulo", l.Titulo);
                    cmd.Parameters.AddWithValue("@ValorAtual", l.ValorAtual);
                    cmd.Parameters.AddWithValue("@ValorMinimo", l.ValorBase);
                    cmd.Parameters.AddWithValue("@DataHoraFinal", l.DataHoraFinal);
                    cmd.Parameters.AddWithValue("@Localizacao", e.GetLocalizacao());
                    cmd.Parameters.AddWithValue("@Ativo", l.Ativo);
                    if (l.GetTipo() == 0)
                    {
                        cmd.Parameters.AddWithValue("@tipoLeilao", "asCegas");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@tipoLeilao", "ingles");
                    }

                    cmd.Parameters.AddWithValue("@Descricao", e.GetDescricao());
                    cmd.Parameters.AddWithValue("@Imagem", e.GetImagem());
                    cmd.Parameters.AddWithValue("@IdArtista", e.GetIdArtista());
                    cmd.Parameters.AddWithValue("@IdGenero", g.GetIdGenero());
                    cmd.Parameters.AddWithValue("@idInstituicao",
                        (l.IdInstituicao != -1) ? (object)l.IdInstituicao : DBNull.Value);
                    cmd.Parameters.AddWithValue("@idAdministrador", l.IdAdmin);

                    cmd.Parameters.AddWithValue("@Id", id);


                    cmd.ExecuteNonQuery();
                }
            }

            return l;
        }

        public Leilao get(int idLeilao)
        {
            Leilao leilao = null;

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    //Criar um set para os ids
                    List<int> minhasLicitacoes = new List<int>();

                    string query3 = @"
                            SELECT Leilao.id AS LeilaoID, Licitacao.id AS LicitacaoID, 
                            Licitacao.idLeilao
                            FROM Leilao
                            INNER JOIN Licitacao ON Leilao.id = Licitacao.idLeilao 
                            Where  Leilao.id = @Id";

                    using (SqlCommand command3 = new SqlCommand(query3, connection))
                    {
                        command3.Parameters.AddWithValue("@Id", idLeilao);

                        using (SqlDataReader reader3 = command3.ExecuteReader())
                        {

                            while (reader3.Read())
                            {
                                int idLicitacao = reader3.GetInt32(reader3.GetOrdinal("LicitacaoID"));
                                minhasLicitacoes.Add(idLicitacao);
                            }
                        }
                    }

                    string query = @"
                        SELECT
                            Leilao.*,
                            GeneroMusical.id AS IdGenero,
                            GeneroMusical.nome AS NomeGenero,
                            GeneroMusical.idAdministrador AS AdminGenero
                        FROM
                            Leilao
                            INNER JOIN GeneroMusical ON Leilao.idGeneroMusical = GeneroMusical.id
                        WHERE
                            Leilao.id = @IdLeilao";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdLeilao", idLeilao);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                GeneroMusical generoMusical = null;
                                {
                                    int idGenero = reader.GetInt32(reader.GetOrdinal("IdGenero"));
                                    string nomeGenero = reader.GetString(reader.GetOrdinal("NomeGenero"));
                                    int idAdmin1 = reader.GetInt32(reader.GetOrdinal("AdminGenero"));
                                    generoMusical = new GeneroMusical(idGenero, nomeGenero, idAdmin1);
                                    // Preencha outros campos do objeto GeneroMusical conforme necessário
                                }
                                ;

                                leilao = null;
                                {
                                    int idLeilaoo = reader.GetInt32(reader.GetOrdinal("id"));
                                    string titulo = reader.GetString(reader.GetOrdinal("titulo"));
                                    float licitacaoAtual = (float)reader.GetDecimal(reader.GetOrdinal("valorAtual"));
                                    DateTime dataTermina = reader.GetDateTime(reader.GetOrdinal("dataHoraFinal"));
                                    string tipoLeilao = reader.GetString(reader.GetOrdinal("tipoLeilao"));
                                    string imagem = reader.GetString(reader.GetOrdinal("imagem"));
                                    string localizacao = reader.GetString(reader.GetOrdinal("localizacao"));
                                    string descricao = reader.GetString(reader.GetOrdinal("descricao"));
                                    //int idGeneroMusical = reader.GetInt32(reader.GetOrdinal("idGeneroMusical"));
                                    float valorBase = (float)reader.GetDecimal(reader.GetOrdinal("valorMinimo"));
                                    int idAdmin2 = reader.GetInt32(reader.GetOrdinal("idAdministrador"));
                                    int idInstituicao;
                                    if (!reader.IsDBNull(reader.GetOrdinal("idInstituicao")))
                                    {
                                        idInstituicao = reader.GetInt32(reader.GetOrdinal("idInstituicao"));
                                    }
                                    else
                                    {
                                        idInstituicao = -1;
                                    }

                                    bool ativo = reader.GetBoolean(reader.GetOrdinal("estado"));
                                    int idArtista = reader.GetInt32(reader.GetOrdinal("idArtista"));

                                    Experiencia e = new Experiencia(descricao, imagem, localizacao, idArtista,
                                        generoMusical);

                                    if (tipoLeilao.Equals("asCegas"))
                                    {
                                        leilao = new LeilaoAsCegas(idLeilao, ativo, licitacaoAtual, valorBase,
                                            dataTermina,
                                            titulo, DateTime.Now, idAdmin2, idInstituicao, minhasLicitacoes, e);
                                    }
                                    else
                                    {
                                        leilao = new LeilaoIngles(idLeilao, ativo, licitacaoAtual, valorBase,
                                            dataTermina,
                                            titulo, DateTime.Now, idAdmin2, idInstituicao, minhasLicitacoes, e);
                                    }

                                    // Preencha outros campos do objeto Leilao conforme necessário

                                }
                                ;
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

            if (leilao == null)
            {
                throw new LeilaoNaoExiste("O leilão de id " + idLeilao + " não existe!");
            }

            return leilao;
        }

        public static int size()
        {
            int totalRows = 0;

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) AS TotalRows FROM Leilao";

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


        //Acho que vai ter de alterar por causa dos atributos do leilao, mas a lógica é esta
        public Dictionary<Leilao, Artista> leiloesAtivos()
        {

            Dictionary<Leilao, Artista> leiloesAtivosMap = new Dictionary<Leilao, Artista>();

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query2 = @"
                        SELECT Leilao.id AS LeilaoID, GeneroMusical.id AS GeneroID, GeneroMusical.nome AS GeneroNome, 
                               GeneroMusical.idAdministrador AS GeneroAdmin
                        FROM Leilao
                        INNER JOIN GeneroMusical ON Leilao.idGeneroMusical = GeneroMusical.id
                        WHERE Leilao.Estado = 1";

                    // Criar um dicionário para armazenar o nome do gênero para cada Leilao
                    Dictionary<int, GeneroMusical> generoPorLeilao = new Dictionary<int, GeneroMusical>();

                    // Buscar o nome do gênero musical
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                int idLeilao = reader2.GetInt32(reader2.GetOrdinal("LeilaoID"));
                                int idGenero = reader2.GetInt32(reader2.GetOrdinal("GeneroId"));
                                string nomeGenero = reader2.GetString(reader2.GetOrdinal("GeneroNome"));
                                int idAdmin = reader2.GetInt32(reader2.GetOrdinal("GeneroAdmin"));

                                GeneroMusical g = new GeneroMusical(idGenero, nomeGenero, idAdmin);

                                generoPorLeilao[idLeilao] = g;
                            }
                        }
                    }

                    //Criar um set para os ids
                    List<int> minhasLicitacoes = new List<int>();

                    string query3 = @"
                        SELECT Leilao.id AS LeilaoID, Licitacao.id AS LicitacaoID
                        FROM Leilao
                        INNER JOIN Licitacao ON Leilao.id = Licitacao.idLeilao
                        WHERE Leilao.Estado = 1";

                    using (SqlCommand command3 = new SqlCommand(query3, connection))
                    {
                        using (SqlDataReader reader3 = command3.ExecuteReader())
                        {
                            while (reader3.Read())
                            {
                                int idLicitacao = reader3.GetInt32(reader3.GetOrdinal("LicitacaoID"));
                                minhasLicitacoes.Add(idLicitacao);
                            }
                        }
                    }

                    string query = @"
                        SELECT Leilao.id AS LeilaoId, Leilao.titulo, Leilao.valorMinimo ,Leilao.valorAtual, Leilao.dataHoraFinal, Leilao.tipoLeilao, Leilao.imagem AS imagemLeilao
                               , Leilao.localizacao, Leilao.idInstituicao, Leilao.descricao, Leilao.estado, Leilao.idAdministrador AS LeilaoIdAdmin, Artista.*
                        FROM Leilao
                        INNER JOIN Artista ON Leilao.idArtista = Artista.id
                        WHERE Leilao.Estado = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artista artista = null;
                                {
                                    // Configure os atributos do Artista com base nas colunas do resultado da consulta
                                    int idArtista = reader.GetInt32(reader.GetOrdinal("id"));
                                    string nomeArtista = reader.GetString(reader.GetOrdinal("nome"));
                                    string imagemArtista = reader.GetString(reader.GetOrdinal("imagem"));
                                    int idAdmin = reader.GetInt32(reader.GetOrdinal("idAdministrador"));

                                    artista = new Artista(idArtista, nomeArtista, imagemArtista, idAdmin);
                                }
                                ;

                                Leilao leilao = null;
                                {
                                    // Configure os atributos do Leilao com base nas colunas do resultado da consulta
                                    int idLeilao = reader.GetInt32(reader.GetOrdinal("LeilaoID"));
                                    string titulo = reader.GetString(reader.GetOrdinal("titulo"));
                                    float licitacaoAtual = (float)reader.GetDecimal(reader.GetOrdinal("valorAtual"));
                                    DateTime dataTermina = reader.GetDateTime(reader.GetOrdinal("dataHoraFinal"));
                                    string tipoLeilao = reader.GetString(reader.GetOrdinal("tipoLeilao"));
                                    string imagem = reader.GetString(reader.GetOrdinal("imagemLeilao"));
                                    string localizacao = reader.GetString(reader.GetOrdinal("localizacao"));
                                    string descricao = reader.GetString(reader.GetOrdinal("descricao"));
                                    //int idGeneroMusical = reader.GetInt32(reader.GetOrdinal("idGeneroMusical"));
                                    float valorBase = (float)reader.GetDecimal(reader.GetOrdinal("valorMinimo"));
                                    int idAdmin2 = reader.GetInt32(reader.GetOrdinal("LeilaoIdAdmin"));
                                    int idInstituicao;
                                    if (!reader.IsDBNull(reader.GetOrdinal("idInstituicao")))
                                    {
                                        idInstituicao = reader.GetInt32(reader.GetOrdinal("idInstituicao"));
                                    }
                                    else
                                    {
                                        idInstituicao = -1;
                                    }

                                    bool ativo = reader.GetBoolean(reader.GetOrdinal("estado"));


                                    // Buscar o nome do gênero musical usando o dicionário
                                    GeneroMusical g = generoPorLeilao[idLeilao];
                                    Experiencia e = new Experiencia(descricao, imagem, localizacao,
                                        artista.GetIdArtista(), g);
                                    if (tipoLeilao.Equals("asCegas"))
                                    {
                                        leilao = new LeilaoAsCegas(idLeilao, ativo, licitacaoAtual, valorBase,
                                            dataTermina,
                                            titulo, DateTime.Now, idAdmin2, idInstituicao, minhasLicitacoes, e);
                                    }
                                    else
                                    {
                                        leilao = new LeilaoIngles(idLeilao, ativo, licitacaoAtual, valorBase,
                                            dataTermina,
                                            titulo, DateTime.Now, idAdmin2, idInstituicao, minhasLicitacoes, e);
                                    }
                                }
                                ;

                                leiloesAtivosMap.Add(leilao, artista);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trate a exceção conforme necessário, ou apenas a lance novamente.
                    throw;
                }
            }

            return leiloesAtivosMap;
        }

        public Dictionary<Leilao, Artista> filtrarLeiloesPorArtista(string nomeArtista)
        {
            Dictionary<Leilao, Artista> leiloesPorArtista = new Dictionary<Leilao, Artista>();

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    //GENEROS
                    string query2 = @"
                        SELECT Leilao.id AS LeilaoID, GeneroMusical.id AS GeneroID, GeneroMusical.nome AS GeneroNome, 
                               GeneroMusical.idAdministrador AS GeneroAdmin
                        FROM Leilao
                        INNER JOIN GeneroMusical ON Leilao.idGeneroMusical = GeneroMusical.id
                        WHERE Leilao.Estado = 1";

                    // Criar um dicionário para armazenar o nome do gênero para cada Leilao
                    Dictionary<int, GeneroMusical> generoPorLeilao = new Dictionary<int, GeneroMusical>();

                    // Buscar o nome do gênero musical
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                int idLeilao = reader2.GetInt32(reader2.GetOrdinal("LeilaoID"));
                                int idGenero = reader2.GetInt32(reader2.GetOrdinal("GeneroId"));
                                string nomeGenero = reader2.GetString(reader2.GetOrdinal("GeneroNome"));
                                int idAdmin = reader2.GetInt32(reader2.GetOrdinal("GeneroAdmin"));

                                GeneroMusical g = new GeneroMusical(idGenero, nomeGenero, idAdmin);

                                generoPorLeilao[idLeilao] = g;
                            }
                        }
                    }

                    //IDS Licitações
                    List<int> minhasLicitacoes = new List<int>();

                    string query3 = @"
                        SELECT Leilao.id AS LeilaoID, Licitacao.id AS LicitacaoID
                        FROM Leilao
                        INNER JOIN Licitacao ON Leilao.id = Licitacao.idLeilao
                        WHERE Leilao.Estado = 1";

                    using (SqlCommand command3 = new SqlCommand(query3, connection))
                    {
                        using (SqlDataReader reader3 = command3.ExecuteReader())
                        {
                            while (reader3.Read())
                            {
                                int idLicitacao = reader3.GetInt32(reader3.GetOrdinal("LicitacaoID"));
                                minhasLicitacoes.Add(idLicitacao);
                            }
                        }
                    }

                    // Primeiro, obter o ID do artista pelo nome
                    string queryObterIdArtista = "SELECT id FROM Artista WHERE nome = @NomeArtista";

                    using (SqlCommand commandObterIdArtista = new SqlCommand(queryObterIdArtista, connection))
                    {
                        commandObterIdArtista.Parameters.AddWithValue("@NomeArtista", nomeArtista);

                        int idArtista = Convert.ToInt32(commandObterIdArtista.ExecuteScalar());

                        // Em seguida, usar o ID do artista para obter os leilões ativos em que ele participa
                        string queryLeiloesPorArtista = @"
                     SELECT Leilao.id AS LeilaoId, Leilao.titulo, Leilao.valorMinimo ,Leilao.valorAtual, Leilao.dataHoraFinal, Leilao.tipoLeilao, Leilao.imagem AS imagemLeilao
                               , Leilao.localizacao, Leilao.idInstituicao, Leilao.descricao, Leilao.estado, Leilao.idAdministrador AS LeilaoIdAdmin, Artista.*
                      FROM Leilao
                      INNER JOIN Artista ON Leilao.idArtista = Artista.id
                      WHERE Leilao.Estado = 1 AND Leilao.idArtista = @IdArtista";

                        using (SqlCommand commandLeiloesPorArtista = new SqlCommand(queryLeiloesPorArtista, connection))
                        {
                            commandLeiloesPorArtista.Parameters.AddWithValue("@IdArtista", idArtista);

                            using (SqlDataReader reader = commandLeiloesPorArtista.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Artista artista = null;
                                    {
                                        // Configure os atributos do Artista com base nas colunas do resultado da consulta
                                        int idArtistaLeilao = reader.GetInt32(reader.GetOrdinal("id"));
                                        string nomeArtistaLeilao = reader.GetString(reader.GetOrdinal("nome"));
                                        string imagemArtistaLeilao = reader.GetString(reader.GetOrdinal("imagem"));
                                        int idAdminArtistaLeilao =
                                            reader.GetInt32(reader.GetOrdinal("idAdministrador"));

                                        artista = new Artista(idArtistaLeilao, nomeArtistaLeilao, imagemArtistaLeilao,
                                            idAdminArtistaLeilao);
                                    }
                                    ;

                                    Leilao leilao = null;
                                    {
                                        // Configure os atributos do Leilao com base nas colunas do resultado da consulta
                                        int idLeilao = reader.GetInt32(reader.GetOrdinal("LeilaoID"));
                                        string titulo = reader.GetString(reader.GetOrdinal("titulo"));
                                        float licitacaoAtual =
                                            (float)reader.GetDecimal(reader.GetOrdinal("valorAtual"));
                                        DateTime dataTermina = reader.GetDateTime(reader.GetOrdinal("dataHoraFinal"));
                                        string tipoLeilao = reader.GetString(reader.GetOrdinal("tipoLeilao"));
                                        string imagem = reader.GetString(reader.GetOrdinal("imagemLeilao"));
                                        string localizacao = reader.GetString(reader.GetOrdinal("localizacao"));
                                        string descricao = reader.GetString(reader.GetOrdinal("descricao"));
                                        //int idGeneroMusical = reader.GetInt32(reader.GetOrdinal("idGeneroMusical"));
                                        float valorBase = (float)reader.GetDecimal(reader.GetOrdinal("valorMinimo"));
                                        int idAdmin2 = reader.GetInt32(reader.GetOrdinal("LeilaoIdAdmin"));
                                        int idInstituicao;
                                        if (!reader.IsDBNull(reader.GetOrdinal("idInstituicao")))
                                        {
                                            idInstituicao = reader.GetInt32(reader.GetOrdinal("idInstituicao"));
                                        }
                                        else
                                        {
                                            idInstituicao = -1;
                                        }

                                        bool ativo = reader.GetBoolean(reader.GetOrdinal("estado"));


                                        // Buscar o nome do gênero musical usando o dicionário
                                        GeneroMusical g = generoPorLeilao[idLeilao];
                                        Experiencia e = new Experiencia(descricao, imagem, localizacao,
                                            artista.GetIdArtista(), g);

                                        if (tipoLeilao.Equals("asCegas"))
                                        {
                                            leilao = new LeilaoAsCegas(idLeilao, ativo, licitacaoAtual, valorBase,
                                                dataTermina,
                                                titulo, DateTime.Now, idAdmin2, idInstituicao, minhasLicitacoes, e);
                                        }
                                        else
                                        {
                                            leilao = new LeilaoIngles(idLeilao, ativo, licitacaoAtual, valorBase,
                                                dataTermina,
                                                titulo, DateTime.Now, idAdmin2, idInstituicao, minhasLicitacoes, e);
                                        }
                                    }
                                    ;

                                    leiloesPorArtista.Add(leilao, artista);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trate a exceção conforme necessário, ou apenas a lance novamente.
                    throw;
                }
            }

            return leiloesPorArtista;
        }

        public GeneroMusical getGenero(int idGenero)
        {
            GeneroMusical generoMusical = null;

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id, nome, idAdministrador FROM GeneroMusical WHERE id = @IdGenero";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdGenero", idGenero);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                {
                                    int id = reader.GetInt32(reader.GetOrdinal("id"));
                                    string nome = reader.GetString(reader.GetOrdinal("nome"));
                                    int idAdministrador = reader.GetInt32(reader.GetOrdinal("idAdministrador"));
                                    generoMusical = new GeneroMusical(id, nome, idAdministrador);
                                    // Preencha outros campos do objeto GeneroMusical conforme necessário
                                }
                                ;
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

            if (generoMusical == null)
            {
                throw new GeneroMusicalNaoExisteException("O género musical com o id " + idGenero + " não existe");
            }

            return generoMusical;
        }

        public static int sizeGeneros()
        {
            int totalRows = 0;

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) AS TotalRows FROM GeneroMusical";

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

        //DUVIDA:SABER SE O ID COMEÇA POR ZERO OU NÃO
        public Dictionary<int, GeneroMusical> preencherGeneros()
        {
            int tamanho = LeilaoDAO.sizeGeneros();
            Dictionary<int, GeneroMusical> resultado = new Dictionary<int, GeneroMusical>();

            for (int i = 0; i < tamanho; i++)
            {
                try
                {
                    GeneroMusical generoMusical = this.getGenero(i + 1);
                    resultado.Add(i + 1, generoMusical);
                }
                catch (GeneroMusicalNaoExisteException)
                {
                    // Lide com a exceção, se necessário, por exemplo, registre-a
                    Console.WriteLine($"GeneroMusical com o id {i + 1} não existe.");
                }
            }

            return resultado;
        }

        public GeneroMusical putGeneroMusical(int id, GeneroMusical novoGenero)
        {
            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                connection.Open();

                string sql = "MERGE INTO GeneroMusical AS target " +
                             "USING (VALUES (@IdGenero, @Nome, @IdAdmin)) " +
                             "AS source (id, nome, idAdministrador) " +
                             "ON target.id = @IdGenero " +
                             "WHEN MATCHED THEN " +
                             "    UPDATE SET nome = source.nome, idAdministrador = source.idAdministrador " +
                             "WHEN NOT MATCHED THEN " +
                             "    INSERT (id, nome, idAdministrador) VALUES (source.id, source.nome, source.idAdministrador);";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.GetNome());
                    cmd.Parameters.AddWithValue("@IdAdmin", novoGenero.GetIdAdmin());


                    cmd.ExecuteNonQuery();
                }
            }

            return novoGenero;
        }
        
        
        public HashSet<int> keySet()
        {
            HashSet<int> leilaoIds = new HashSet<int>();

            using (SqlConnection connection = new SqlConnection(DAOConfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id FROM Leilao";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int leilaoId = Convert.ToInt32(reader["id"]);
                                leilaoIds.Add(leilaoId);
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

            return leilaoIds;
        }
        /*
          public List<Leilao> obterLeiloesPorIdsGenero(List<int> idsGenero)
          {
              List<Leilao> leiloes = new List<Leilao>();

              try
              {
                  using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                  {
                      connection.Open();

                      foreach (int idGenero in idsGenero)
                      {
                          string query = @"
                          SELECT
                              Leilao.*,
                              Licitacao.id As IdLicitacao,
                              Artista.id As IDArtista,
                              Artista.nome As NomeArtista,
                              Artista.imagem As ImagemArtista,
                              Artista.idAdministrador As ADARTISTA,
                              GeneroMusical.id AS IdGenero,
                              GeneroMusical.nome AS NomeGenero,
                              GeneroMusical.idAdministrador AS AdminGenero
                          FROM
                              Leilao
                              INNER JOIN GeneroMusical ON Leilao.idGeneroMusical = GeneroMusical.id
                              INNER JOIN Licitacao ON Leilao.id = Licitacao.idLeilao
                              INNER JOIN Artista ON Leilao.idArtista = Artista.id

                          WHERE
                              Leilao.idGeneroMusical = @IdGenero AND Leilao.Estado = 1";


                          GeneroMusical genero = null;
                          using (SqlCommand cmd = new SqlCommand(query, connection))
                          {
                              cmd.Parameters.AddWithValue("@IdGenero", idGenero);
                              using (SqlDataReader reader = cmd.ExecuteReader())
                              {
                                  while (reader.Read())
                                  {
                                      string nomeGenero = reader.GetString(reader.GetOrdinal("NomeGenero"));
                                      int idAdmin = reader.GetInt32(reader.GetOrdinal("AdminGenero"));
                                      GeneroMusical g = new GeneroMusical(idGenero, nomeGenero, idAdmin);

                                      // Configure os atributos do Artista com base nas colunas do resultado da consulta
                                      int idArtistaLeilao = reader.GetInt32(reader.GetOrdinal("IDArtista"));
                                      string nomeArtistaLeilao = reader.GetString(reader.GetOrdinal("NomeArtista"));
                                      string imagemArtistaLeilao = reader.GetString(reader.GetOrdinal("ImagemArtista"));
                                      int idAdminArtistaLeilao =
                                          reader.GetInt32(reader.GetOrdinal("ADARTISTA"));

                                      Artista artista = new Artista(idArtistaLeilao, nomeArtistaLeilao,
                                          imagemArtistaLeilao,
                                          idAdminArtistaLeilao);

                                      Leilao leilao = null;
                                      int idLeilao = reader.GetInt32(reader.GetOrdinal("id"));
                                      string titulo = reader.GetString(reader.GetOrdinal("titulo"));
                                      float licitacaoAtual = (float)reader.GetDecimal(reader.GetOrdinal("valorAtual"));
                                      DateTime dataTermina = reader.GetDateTime(reader.GetOrdinal("dataHoraFinal"));
                                      string tipoLeilao = reader.GetString(reader.GetOrdinal("tipoLeilao"));
                                      string imagem = reader.GetString(reader.GetOrdinal("imagem"));
                                      string localizacao = reader.GetString(reader.GetOrdinal("localizacao"));
                                      string descricao = reader.GetString(reader.GetOrdinal("descricao"));
                                      float valorBase = (float)reader.GetDecimal(reader.GetOrdinal("valorMinimo"));
                                      int idAdmin2 = reader.GetInt32(reader.GetOrdinal("idAdministrador"));
                                      int idInstituicao;
                                      if (!reader.IsDBNull(reader.GetOrdinal("idInstituicao")))
                                      {
                                          idInstituicao = reader.GetInt32(reader.GetOrdinal("idInstituicao"));
                                      }
                                      else
                                      {
                                          idInstituicao = -1;
                                      }

                                      bool ativo = reader.GetBoolean(reader.GetOrdinal("estado"));
                                      Experiencia e = new Experiencia(descricao, imagem, localizacao,
                                          artista.getIdArtista(), g);

                                      if (tipoLeilao.Equals("asCegas"))
                                      {
                                          leilao = new LeilaoAsCegas(idLeilao, ativo, licitacaoAtual, valorBase,
                                              dataTermina,
                                              titulo, DateTime.Now, idAdmin2, idInstituicao, minhasLicitacoes, e);
                                      }
                                      else
                                      {
                                          leilao = new LeilaoIngles(idLeilao, ativo, licitacaoAtual, valorBase,
                                              dataTermina,
                                              titulo, DateTime.Now, idAdmin2, idInstituicao, minhasLicitacoes, e);
                                      }

                                      leiloes.Add(leilao);


                                  }
                              }

                          }
                      }
                  }

              }
              catch
              {

              }

              return leiloes;

          }

          public List<Leilao> obterLeiloesPorTipo(List<int> tipos)
          {
              List<Leilao> leiloes = new List<Leilao>();

              try
              {
                  using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                  {
                      connection.Open();

                      foreach (int tipoLeilao in tipos)
                      {
                          string query = "SELECT * FROM Leilao WHERE tipo = @Tipo";
                          using (SqlCommand command = new SqlCommand(query, connection))
                          {
                              command.Parameters.AddWithValue("@Tipo", tipoLeilao);

                              using (SqlDataReader reader = command.ExecuteReader())
                              {
                                  while (reader.Read())
                                  {
                                      int idGeneroMusical = Convert.ToInt32(reader["idGeneroMusical"]);

                                      string query2 = "SELECT * FROM GeneroMusical  WHERE id = @IdGenero";

                                      using (SqlCommand command2 = new SqlCommand(query2, connection))
                                      {
                                          command2.Parameters.AddWithValue("@IdGenero", idGeneroMusical);

                                          using (SqlDataReader readerGenero = command.ExecuteReader())
                                          {
                                              if (readerGenero.Read())
                                              {
                                                  Boolean ativo = Convert.ToBoolean(reader["ativo"]);
                                                  if (ativo)
                                                  {
                                                      string nomeGenero = Convert.ToString(readerGenero["nome"]);
                                                      int idAdmistradoGenero =
                                                          Convert.ToInt32(readerGenero["idAdministrador "]);
                                                      GeneroMusical generoMusical =
                                                          new GeneroMusical(idGeneroMusical, nomeGenero,
                                                              idAdmistradoGenero);
                                                      string discricao = Convert.ToString(reader["descricao"]);
                                                      string imagem = Convert.ToString(reader["imagem"]);
                                                      string localizacao = Convert.ToString(reader["localizacao"]);
                                                      int idArtista = Convert.ToInt32(reader["idArtista"]);

                                                      Experiencia experiencia = new Experiencia(discricao, imagem,
                                                          localizacao,
                                                          idArtista, generoMusical);
                                                      int idLeilao = Convert.ToInt32((reader["id"]));
                                                      float valorAtual = Convert.ToSingle(reader["valorAtual"]);
                                                      float valorBase = Convert.ToSingle(reader["valorMinimo"]);
                                                      DateTime dataHoraFinal =
                                                          Convert.ToDateTime((reader["dataHoraFinal"]));
                                                      string titulo = Convert.ToString(reader["titulo"]);
                                                      DateTime dataHoraContador =
                                                          Convert.ToDateTime((reader["dataHoraContador"]));
                                                      int idAdmin = Convert.ToInt32((reader["idAdministrador"]));
                                                      string query3 =
                                                          "SELECT * FROM Licitacao  WHERE idLeilao  = @IdLeilao";
                                                      List<int> minhasLicitacoes = new List<int>();
                                                      using (SqlCommand command3 = new SqlCommand(query, connection))
                                                      {
                                                          command.Parameters.AddWithValue("@IdLeilao", idLeilao);

                                                          using (SqlDataReader readerLicitacoes = command.ExecuteReader())
                                                          {
                                                              while (reader.Read())
                                                              {
                                                                  minhasLicitacoes.Add(
                                                                      Convert.ToInt32(readerLicitacoes["id"]));
                                                              }
                                                          }
                                                      }

                                                      if (tipoLeilao == 0)
                                                      {
                                                          Leilao leilao = new LeilaoAsCegas(idLeilao, ativo, valorAtual,
                                                              valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin,
                                                              -1, minhasLicitacoes, experiencia);
                                                          leiloes.Add(leilao);
                                                      }
                                                      else
                                                      {
                                                          Leilao leilao = new LeilaoIngles(idLeilao, ativo, valorAtual,
                                                              valorBase, dataHoraFinal, titulo, dataHoraContador, idAdmin,
                                                              -1, minhasLicitacoes, experiencia);
                                                          leiloes.Add(leilao);
                                                      }

                                                  }

                                              }

                                          }
                                      }

                                  }
                              }
                          }
                      }
                  }
              }
              catch (Exception ex)
              {
                  // Lidar com exceções, se necessário
                  Console.WriteLine("Erro ao obter leilões: " + ex.Message);
              }

              return leiloes;
          }

        
      */
    }
}