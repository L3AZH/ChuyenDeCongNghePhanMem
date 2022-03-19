using CDCNPM.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Repositories
{
    public class SqlReposiory : ISqlRepository
    {
        public List<SqlTable> getListSqlTable(IConfiguration config)
        {
            List<SqlTable> listResult = new List<SqlTable>();
            try
            {
                string conString = config.GetConnectionString("MSSQLConnection");
                SqlConnection connection = new SqlConnection(conString);

                connection.Open();
                string queryString =
                    "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME NOT LIKE 'sysdiagrams';";
                SqlCommand query = new SqlCommand(queryString, connection);
                query.CommandType = CommandType.Text;
                SqlDataReader result = query.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        SqlTable tableResult = new SqlTable()
                        {
                            tableName = result.GetString(2)
                        };
                        listResult.Add(tableResult);
                    }
                    result.Close();
                }
                result.Close();

                foreach (SqlTable table in listResult)
                {
                    queryString = "SELECT * FROM information_schema.columns WHERE TABLE_NAME='"+table.tableName+"';";
                    SqlCommand queryCol = new SqlCommand(queryString, connection);
                    queryCol.CommandType = CommandType.Text;
                    SqlDataReader resultCol = queryCol.ExecuteReader();
                    table.columns = new List<SqlColumn>();
                    if (resultCol.HasRows)
                    {
                        while (resultCol.Read())
                        {
                            SqlColumn column = new SqlColumn()
                            {
                                name = resultCol.GetString(3),
                                type = resultCol.GetString(7)
                            };
                            table.columns.Add(column);
                        }
                        resultCol.Close();
                    }
                    resultCol.Close();
                }

                connection.Close();

            } catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.StackTrace);
                listResult = null;
            }
            return listResult;
        }
    }
}
