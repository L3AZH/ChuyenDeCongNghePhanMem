using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM
{
    public class Utils
    {
        public static void log(String message)
        {
            System.Diagnostics.Debug.WriteLine("\n");
            System.Diagnostics.Debug.WriteLine("=====================================================");
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine("=====================================================");
            System.Diagnostics.Debug.WriteLine("\n");
        }

        public static void logQuery(String queryString, SqlDataReader result)
        {
            log(String.Format("Query: {0} - result: {1}", queryString, result.HasRows.ToString()));
        }
    }
}
