
using System.Data;
using System.Data.SqlClient;
using RhythmsOfGiving.Controller;

public class LeilaoDAO{
    private static LeilaoDAO? singleton = null;
        private LeilaoDAO() { }

        public static LeilaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new LeilaoDAO();
            }
            return singleton;
        }
        
        public static int size(){
            return 0; // depois usar a query necessária
        }

        
        //Acho que vai ter de alterar por causa dos atributos do leilao, mas a lógica é esta
        public Dictionary<Leilao, Artista> leiloesAtivos(){

            Dictionary<Leilao, Artista> leiloesAtivosMap = new Dictionary<Leilao, Artista>();

            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query2 = @"
                        SELECT Leilao.id AS LeilaoID, GeneroMusical.nome
                        FROM Leilao
                        INNER JOIN GeneroMusical ON Leilao.idGeneroMusical = GeneroMusical.id";

                    // Criar um dicionário para armazenar o nome do gênero para cada Leilao
                    Dictionary<int, string> generoPorLeilao = new Dictionary<int, string>();

                    // Buscar o nome do gênero musical
                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                int idLeilao = reader2.GetInt32(reader2.GetOrdinal("LeilaoID"));
                                string nomeGenero = reader2.GetString(reader2.GetOrdinal("nome"));

                                generoPorLeilao[idLeilao] = nomeGenero;
                            }
                        }
                    }

                    string query = @"
                        SELECT Leilao.id AS LeilaoId, Leilao.titulo, Leilao.valorAtual, Leilao.dataHoraFinal, Leilao.tipoLeilao, Leilao.imagem AS imagemLeilao
                               , Leilao.localizacao, Leilao.descricao, Leilao.Estado, Leilao.idAdministrador AS LeilaoIdAdmin, Artista.*
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
                                };

                                Leilao leilao = null;
                                {
                                    // Configure os atributos do Leilao com base nas colunas do resultado da consulta
                                    int idLeilao = reader.GetInt32(reader.GetOrdinal("LeilaoID"));
                                    string titulo = reader.GetString(reader.GetOrdinal("titulo"));
                                    float licitacaoAtual = reader.GetFloat(reader.GetOrdinal("valorAtual"));
                                    DateTime dataTermina = reader.GetDateTime(reader.GetOrdinal("dataHoraFinal"));
                                    string tipoLeilao = reader.GetString(reader.GetOrdinal("tipoLeilao"));
                                    string imagem = reader.GetString(reader.GetOrdinal("imagemLeilao"));
                                    string localizacao = reader.GetString(reader.GetOrdinal("localizacao"));
                                    string descricao = reader.GetString(reader.GetOrdinal("descricao"));
                                    int idGeneroMusical = reader.GetInt32(reader.GetOrdinal("idGeneroMusical"));

                                    bool asCegas = tipoLeilao.Equals("àsCegas");

                                    // Buscar o nome do gênero musical usando o dicionário
                                    string nomeGenero = generoPorLeilao.ContainsKey(idLeilao) ? generoPorLeilao[idLeilao] : string.Empty;

                                    leilao = new Leilao(idLeilao, artista.getNome(), titulo, localizacao, nomeGenero,
                                        tipoLeilao, dataTermina,
                                        descricao, descricao, licitacaoAtual, imagem, artista.getImagem(), asCegas);
                                };

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

}