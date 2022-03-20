using CDCNPM.Models;
using CDCNPM.Repositories;
using CDCNPM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {

        private ISqlRepository sqlRepository;
        private readonly IConfiguration config;

        public HomeController(ISqlRepository theSqlReposiory, IConfiguration theConfig)
        {
            this.sqlRepository = theSqlReposiory;
            this.config = theConfig;
        }

        [Route("~/")]
        [Route("index")]
        public ViewResult Index()
        {
            List<SqlTable> result = sqlRepository.getListSqlTable(config);
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                listTable = result
            };
            UnitTestHomeController.testGenerateQuery();
            return View(homeViewModel);
        }

        public static string generateQueryFromObjectQueryPick(List<ObjectQueryPick> listObject, List<SqlTable> listTable)
        {
            String queryString = "";
            /**
             * Generate select section
             **/
            queryString += "SELECT ";
            foreach(ObjectQueryPick item in listObject)
            {
                if(string.IsNullOrEmpty(item.total) ||
                    string.IsNullOrWhiteSpace(item.total))
                {
                    /**
                    * check if user had a custome name or rename and it had total field
                    **/
                    if (string.IsNullOrEmpty(item.columnNameRename) ||
                        string.IsNullOrWhiteSpace(item.columnNameRename))
                    {
                        queryString += String.Format("{0}.{1}, ",
                            item.columnPick.tableName,
                            item.columnPick.name);
                    }
                    else
                    {
                        queryString += String.Format("{0}.{1} as {2}, ",
                            item.columnPick.tableName,
                            item.columnPick.name,
                            item.columnNameRename);
                    }
                } else
                {
                    /**
                     * check if user had a custome name or rename
                     **/
                    if (string.IsNullOrEmpty(item.columnNameRename) ||
                        string.IsNullOrWhiteSpace(item.columnNameRename))
                    {
                        queryString += String.Format("{0}({1}.{2}) as {3}, ",
                            item.total,
                            item.columnPick.tableName,
                            item.columnPick.name,
                            item.columnNameRename);
                    }
                    else
                    {
                        queryString += String.Format("{0}({1}.{2}), ",
                            item.total,
                            item.columnPick.tableName,
                            item.columnPick.name);
                    }
                }
            }
            queryString = queryString.Trim();
            queryString = queryString.Remove(queryString.Length - 1);
            queryString += "\n ";
            /**
             * Generate FROM section
             **/
            queryString += "FROM ";
            foreach(SqlTable table in listTable)
            {
                queryString += String.Format("{0}, ", table.tableName);
            }
            queryString = queryString.Trim();
            queryString = queryString.Remove(queryString.Length - 1);
            queryString += "\n ";
            /**
             * Generate WHERE section
             **/
            queryString += "WHERE ";
            /**
             * WHERE : JOIN SECTION
             **/
            foreach(SqlTable table in listTable)
            {
                if(table.listFK.Count > 0)
                {
                    foreach(string item in table.listFK)
                    {
                        string[] listData = item.Split("-");
                        queryString += String.Format("({0}.{1} = {2}.{3}) AND ",
                            table.tableName, listData[0],
                            listData[1], listData[2]);
                    }
                }
            }
            /**
             * WHERE: CONDITION SECTION
             **/

            return queryString;
        }

    }
}
