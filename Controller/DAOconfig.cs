using System.Data.SqlClient;

namespace RhythmsOfGiving.Controller
{
    public class DAOconfig
    {
        public const string USER = "FaltaDefinir";
        public const string PASSWORD = "FaltaDefinir";
        public const string MACHINE = "FaltaDefinir";
        public const string DATABASE = "FaltaDefinir";

        public static string GetConnectionString()
        {
            //     SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //     builder.DataSource = MACHINE;
            //     builder.UserID = USER;
            //     builder.Password = PASSWORD;
            //     builder.InitialCatalog = DATABASE;
            //     builder.TrustServerCertificate = true;
            //     return builder.ConnectionString;

            return null;
        }
    }
}