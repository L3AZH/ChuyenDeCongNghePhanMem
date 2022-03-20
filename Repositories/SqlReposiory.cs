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
                listResult = getListTable(config);

                for(int index = 0; index < listResult.Count; index++)
                {
                    getListColumnsForTable(config, listResult[index]);
                }
                initPrimaryKeyForTable(config, listResult);
                initRelationShipFKForListTable(config, listResult);
            } catch (Exception err)
            {
                listResult = null;
                Utils.log(err.StackTrace);
            }
            return listResult;
        }

        private List<SqlTable> getListTable(IConfiguration config)
        {
            SqlConnection myConn = null;
            List<SqlTable> listResult = new List<SqlTable>();
            try
            {
                string conString = config.GetConnectionString("MSSQLConnection");
                myConn = new SqlConnection(conString);

                myConn.Open();
                string queryString =
                    "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME NOT LIKE 'sysdiagrams';";
                SqlCommand command = new SqlCommand(queryString, myConn);
                SqlDataReader result = command.ExecuteReader();
                Utils.logQuery(queryString, result);
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        SqlTable table = new SqlTable()
                        {
                            // column 2 is TABLE_NAME begin index is 0
                            tableName = result.GetString(2)
                        };
                        listResult.Add(table);
                    }
                    result.Close();
                } else {
                    Utils.log("getListTable: No Result");
                    result.Close();
                }
               
            } catch(Exception err)
            {
                listResult = null;
                Utils.log(err.StackTrace);
            }
            finally
            {
               if(myConn != null)
                {
                    myConn.Close();
                }
            }
            return listResult;
        }

        private SqlTable getListColumnsForTable(IConfiguration config, SqlTable table)
        {
            if( table == null)
            {
                Utils.log("SqlTable input object is null");
                return null;
            }
            SqlConnection myConn = null;
            SqlTable resultTable = table;
            resultTable.columns = new List<SqlColumn>();
            try
            {
                string conString = config.GetConnectionString("MSSQLConnection");
                myConn = new SqlConnection(conString);

                myConn.Open();
                string queryString = String.Format(
                    "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{0}';", table.tableName);
                SqlCommand command = new SqlCommand(queryString, myConn);
                SqlDataReader result = command.ExecuteReader();
                Utils.logQuery(queryString, result);
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        SqlColumn column = new SqlColumn()
                        {
                            /**
                             * Columm 3 is COLUMN_NAME, 7 is DATA_TYPE , index begin is 0
                             **/
                            name = result.GetString(3),
                            type = result.GetString(7),
                            tableName = table.tableName
                        };
                        resultTable.columns.Add(column);
                    }
                    result.Close();
                }
                result.Close();
            } catch(Exception err)
            {
                if(resultTable != null)
                {
                    resultTable.columns = null;
                }
                Utils.log(err.StackTrace);
            }
            finally
            {
                if(myConn != null)
                {
                    myConn.Close();
                }
            }
            return resultTable;
        }

        private List<SqlTable> initPrimaryKeyForTable(IConfiguration config, List<SqlTable> listTable)
        {
            if (listTable == null)
            {
                Utils.log("initPrimaryKeyForTable input object List<SqlTable> is null");
                return null;
            }
            SqlConnection myConn = null;
            try
            {
                string conString = config.GetConnectionString("MSSQLConnection");
                myConn = new SqlConnection(conString);

                myConn.Open();
                foreach (SqlTable table in listTable)
                {
                    string queryString = String.Format(
                        "SELECT KU.table_name as TABLENAME, column_name as PRIMARYKEYCOLUMN " +
                        "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC " +
                        "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU " +
                        "ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' " +
                        "AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME " +
                        "AND KU.table_name='{0}' " +
                        "ORDER BY KU.TABLE_NAME, KU.ORDINAL_POSITION; ", table.tableName);
                    SqlCommand command = new SqlCommand(queryString, myConn);
                    SqlDataReader result = command.ExecuteReader();
                    Utils.logQuery(queryString, result);
                    table.primaryKey = new List<string>();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            /**
                             * 1: PRIMARYKEYCOLUMN,
                             **/
                            table.primaryKey.Add(result.GetString(1));
                        }
                        result.Close();
                    }
                    result.Close();
                }
            }
            catch (Exception err)
            {
                Utils.log(err.StackTrace);
            }
            finally
            {
                if (myConn != null) myConn.Close();
            }
            return listTable;
        }

        private List<SqlTable> initRelationShipFKForListTable(IConfiguration config, List<SqlTable> listTable)
        {
            if(listTable == null)
            {
                Utils.log("initRelationShipFkForListTable input object List<SqlTable> is null");
                return null;
            }
            SqlConnection myConn = null;
            try
            {
                string conString = config.GetConnectionString("MSSQLConnection");
                myConn = new SqlConnection(conString);

                myConn.Open();
                foreach(SqlTable table in listTable)
                {
                    string queryString  = String.Format("EXEC sp_fkeys {0}", table.tableName);
                    SqlCommand command = new SqlCommand(queryString, myConn);
                    SqlDataReader result = command.ExecuteReader();
                    Utils.logQuery(queryString, result);

                    table.listFK = new List<string>();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            /**
                             * 3: PKCOLUMN_NAME,
                             * 6: FKTABLE_NAME,
                             * 7: FKCOLUMN_NAME
                             **/
                            string fkItem =
                                String.Format("{0}-{1}-{2}", result.GetString(3), result.GetString(6), result.GetString(7));
                            table.listFK.Add(fkItem);
                        }
                        result.Close();
                    }
                    result.Close();
                }
            }
            catch(Exception err)
            {
                Utils.log(err.StackTrace);
            }
            finally
            {
                if (myConn != null) myConn.Close();
            }
            return listTable;
        }
    }
}
