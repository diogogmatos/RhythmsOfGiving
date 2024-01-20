using System.Data.SqlClient;

namespace RhythmsOfGiving.Controller
{
    public class DaOconfig
    {
        public const string User = "FaltaDefinir";
        public const string Password = "FaltaDefinir";
        public const string Machine = "FaltaDefinir";
        public const string Database = "FaltaDefinir";

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