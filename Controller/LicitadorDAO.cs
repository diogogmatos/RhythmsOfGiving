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

        public Licitador get(string email)
        {
            //falta definir a lógica
            throw new LicitadorNaoExisteException("O licitador com o email, " + email + " não existe!");
        }

        public Licitador get(int id)
        {
            //falta definir a lógica
            throw new LicitadorNaoExisteException("O licitador com o id " + id + " não existe!");
        }

        public void put(String email, Object l)
        {
            //falta definir a lógica

            throw new DadosInvalidosException("Foram inseridos dados que já estão associados a outra conta");
        }


        public bool VerificarUnicoNumeroCC(int novoNumeroCC)
        {
            bool numeroCCUnico = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(DAOconfig.GetConnectionString()))
                {
                    connection.Open();

                    // Consulta para verificar se o novoNumeroCC já existe na tabela Licitador
                    string query = "SELECT COUNT(*) FROM Licitador WHERE numeroCC = @NovoNumeroCC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                       // command.Parameters.AddWithValue("@NovoNumeroCC", novoNumeroCC);

                        int count = (int)command.ExecuteScalar();

                        // Se count for 0, significa que o número de cartão é único
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

    }
}