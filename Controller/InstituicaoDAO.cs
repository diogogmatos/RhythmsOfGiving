
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

        internal List<int> containsKeys()
        {
            List<int> r = new List<int>();

            return r;
        }

        internal Instituicao get(int idLicitacao)
        {
            // A exception está mal, só para funcionar o get
            throw new NotImplementedException();
        }

        public void put(int id, Instituicao a)
        {
            //falta definir a lógica
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