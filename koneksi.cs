using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace GOS_FxApps
{
    public static class Koneksi
    {

        //private static readonly string connectionString = "Data Source=192.168.1.64;" +
        //    "Initial Catalog=gos_apps;" +
        //    "Persist Security Info=True;" +
        //    "User ID=sa;Password=%tii2025%;" +
        //    "TrustServerCertificate=True;";

        private static readonly string connectionString = "Data Source=WIN-LP6P3PLEU8O;" +
            "Initial Catalog=gos_apps;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static string GetConnectionString()
        {
            return connectionString;
        }

        public static bool CekKoneksi()
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
