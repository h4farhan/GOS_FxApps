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
        // untuk koneksi lokas
        //Data Source = TII_NB2_1122; Initial Catalog = gos_apps; User ID = tii; Password=mm;TrustServerCertificate=True
        private static readonly string connectionString = "Data Source=TII_NB2_1122;" +
            "Initial Catalog=gos_apps;" +
            "Persist Security Info=True;" +
            "User ID=tii;Password=mm;" +
            "TrustServerCertificate=True;";

        //Data Source = 192.168.1.64; Initial Catalog = gos_apps; User ID = sa; Password=$Genta2025$;TrustServerCertificate=True
        //private static readonly string connectionString = "Data Source=192.168.1.64;" +
        //    "Initial Catalog=gos_apps;" +
        //    "Persist Security Info=True;" +
        //    "User ID=sa;Password=$Genta2025$;" +
        //    "TrustServerCertificate=True;";

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
