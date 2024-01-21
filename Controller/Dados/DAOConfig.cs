using System.Data.SqlClient;

namespace RhythmsOfGiving.Controller.Dados
{
    public class DAOConfig
    {
        public const string USER = "sa";
        public const string PASSWORD = "F7gY6c4r";
        public const string MACHINE = "localhost";
        public const string DATABASE = "RhythmsOfGiving2";

        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = MACHINE;
            builder.UserID = USER;
            builder.Password = PASSWORD;
            builder.InitialCatalog = DATABASE;
            // builder.IntegratedSecurity = true;
            Console.WriteLine(builder.ConnectionString);
            return builder.ConnectionString;
        }
    }
}
