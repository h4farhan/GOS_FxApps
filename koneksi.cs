using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Windows.Markup;

namespace GOS_FxApps
{
    public static class Koneksi
    {
        //untuk koneksi lokal laptop kantor
        //Data Source = 192.168.1.25; Initial Catalog = tes; User ID = tii; Password=tii2025;TrustServerCertificate=True
        //private static readonly string connectionString =
        //    "Data Source=192.168.1.25;" +
        //    "Initial Catalog=tes;" +
        //    "Persist Security Info=True;" +
        //    "User ID=tii;Password=tii2025;" +
        //    "TrustServerCertificate=True;" +
        //    "Connect Timeout=3;" +
        //    "Pooling=true;Max Pool Size=200;";

        //untuk server
        //Data Source = 192.168.1.64; Initial Catalog = gos_apps; User ID = sa; Password=$Genta2025$;TrustServerCertificate=True
        private static readonly string connectionString =
            "Data Source=192.168.1.64;" +
            "Initial Catalog=gos_apps;" +
            "Persist Security Info=True;" +
            "User ID=sa;Password=$Genta2025$;" +
            "TrustServerCertificate=True;" +
            "Connect Timeout=3;" +
            "Pooling=true;Max Pool Size=200;";

        //untuk lokal server
        //Data Source = WIN-LP6P3PLEU8O;Initial Catalog = gos_apps;Integrated Security=True;TrustServerCertificate=True;
        //private static readonly string connectionString =
        //    "Data Source=WIN-LP6P3PLEU8O;" +
        //    "Initial Catalog=gos_apps;" +
        //    "Integrated Security=True;" +
        //    "TrustServerCertificate=True;" +
        //    "Connect Timeout=3;" +
        //    "Pooling=true;Max Pool Size=200;";

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

        public static async Task<SqlConnection> GetConnectionAsync()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            await conn.OpenAsync();  
            return conn;
        }
    }
}
