using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GOS_FxApps
{
    public static class Koneksi
    {
        private static readonly string connectionString = "Data Source=192.168.1.25,1433;Initial Catalog=gos_apps;Persist Security Info=True;" +
            "User ID=sa;Password=%tii2025%;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
