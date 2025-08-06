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
        //private static readonly string connectionString = "Data Source=mssql-200510-0.cloudclusters.net,19998;Initial Catalog=gos_apps;Persist Security Info=True;" +
        //    "User ID=DTIAdmin;Password=%Tii2025%;Encrypt=True;TrustServerCertificate=True;";

        //private static readonly string connectionString = "Data Source=192.168.1.25,1433;Initial Catalog=gos_apps;Persist Security Info=True;" +
        //    "User ID=sa;Password=%tii2025%;TrustServerCertificate=True;";

        private static readonly string connectionString = "Data Source=TII_NB2_1122;Initial Catalog=gos_apps;Persist Security Info=True;" +
            "User ID=sa;Password=%tii2025%;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }

        public static bool CekKoneksi()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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
