
using System;
using System.Data.SqlClient;
using RhythmsOfGiving.Controller;

namespace RhythmsOfGiving.Controller
{
    public class InstituicaoDAO
    {
        private static InstituicaoDAO? singleton = null;

        private InstituicaoDAO()
        {
        }

        public static InstituicaoDAO getInstance()
        {
            if (singleton == null)
            {
                singleton = new InstituicaoDAO();
            }

            return singleton;
        }

        public static int size()
        {
            return 0; // depois usar a query necessária
        }

        internal List<int> keySet()
        {
            List<int> r = new List<int>();

            return r;
        }
        
         public Instituicao get(int idInstituicao)
    {

        using SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString());
        connection.Open();

        string query = "SELECT * FROM Instituicao WHERE id = @id";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", idInstituicao);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {

            int id = reader.GetInt32(reader.GetOrdinal("id"));
            string nome = reader.GetString(reader.GetOrdinal("nome"));
            string descricao = reader.GetString(reader.GetOrdinal("descricao"));
            string logoPath = reader.GetString(reader.GetOrdinal("logotipo"));
            string link = reader.GetString(reader.GetOrdinal("hiperligacao"));
            string iban = reader.GetString(reader.GetOrdinal("iban"));
            int idAdmin = reader.GetInt32(reader.GetOrdinal("idAdministrador"));
            return new Instituicao(id,nome,descricao,logoPath,link,iban,idAdmin);
        }

        return null; // Retorna null se a instituição não for encontrada
    }

    public void put(int id, Instituicao instituicao)
    {

        using SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString());
        connection.Open();

        string query = "UPDATE Instituicao SET nome = @nome, descricao = @descricao, " +
                       "logotipo = @logotipo, hiperligacao = @hiperligacao, iban = @iban, " +
                       "idAdministrador = @idAdministrador WHERE id = @id";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@nome", instituicao.getNome());
        command.Parameters.AddWithValue("@descricao", instituicao.getDescricao());
        command.Parameters.AddWithValue("@logotipo", instituicao.getLogoPath());
        command.Parameters.AddWithValue("@hiperligacao", instituicao.getLink());
        command.Parameters.AddWithValue("@iban", instituicao.getIban());
        command.Parameters.AddWithValue("@idAdministrador", instituicao.getIdAdmin());

        command.ExecuteNonQuery();
    }

        public bool existeInstituicao(string nome)
        {
            using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Instituicao WHERE nome = @Nome";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", nome);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            return true; // já existe artista na base de dados
                        }
                        else
                        {
                            return false; // Não existe
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trate a exceção conforme necessário, ou apenas a lance novamente.
                    throw;
                }
            }
        }

    }
}