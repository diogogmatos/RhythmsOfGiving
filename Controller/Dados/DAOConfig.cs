using System.Data.SqlClient;

namespace RhythmsOfGiving.Controller
{
    public class DAOconfig
    {
        public const string USER = "RhythmsOfGiving";
        public const string PASSWORD = "RhythmsOfGiving";
        public const string MACHINE = "LAPTOP-N09LPQI4"; // Coloque o nome do servidor SQL Server aqui
        public const string DATABASE = "RhythmsOfGiving";

        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = MACHINE; // Nome do servidor SQL Server
            builder.UserID = USER;
            builder.Password = PASSWORD;
            builder.InitialCatalog = DATABASE;
            builder.IntegratedSecurity = true; 
            Console.WriteLine(builder.ConnectionString);
            return builder.ConnectionString;
        }
    }
}