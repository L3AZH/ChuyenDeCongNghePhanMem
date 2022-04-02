using CDCNPM.Models;
using CDCNPM.Reports;
using CDCNPM.Repositories;
using CDCNPM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

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
            return View(homeViewModel);
        }

        [HttpPost]
        [Route("ResultQuery")]
        public JsonResult ResultQuery([FromBody] ObjectRequestFromJs request)
        {
            string queryResult = generateQueryFromObjectQueryPick(request.listObject, request.tablePick).Replace("\n", "  ");
            Response.Cookies.Append(
                "query",
                queryResult,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddSeconds(60 * 5)
                });
            return Json(queryResult);
        }

        [Route("report")]
        public ViewResult Report()
        {
            string cookieQuery = Request.Cookies["query"];
            Response.Cookies.Delete("query");
            DataSet dataSet = sqlRepository.getDataSetFromRawQuery(config, cookieQuery);
            SampleReport report = new SampleReport()
            {
                query = cookieQuery
            };
            report.DataSource = dataSet;
            SampleReport.InitBands(report);
            SampleReport.InitDetailsBaseXRTable(report, dataSet, "Test");
            return View(report);
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
                if (item.isShow)
                {
                    if (string.IsNullOrEmpty(item.total) ||
                    string.IsNullOrWhiteSpace(item.total) || "None".Equals(item.total))
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
                    }
                    else
                    {
                        /**
                         * check if user had a custome name or rename
                         **/
                        if (string.IsNullOrEmpty(item.columnNameRename) ||
                            string.IsNullOrWhiteSpace(item.columnNameRename))
                        {
                            queryString += String.Format("{0}({1}.{2}), ",
                                item.total,
                                item.columnPick.tableName,
                                item.columnPick.name);
                        }
                        else
                        {
                            queryString += String.Format("{0}({1}.{2}) as {3}, ",
                                item.total,
                                item.columnPick.tableName,
                                item.columnPick.name,
                                item.columnNameRename);
                        }
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
            queryString = generateWhereSectionQuery(queryString, listObject, listTable);
            /**
             * GROUP BY SECTION
             **/
            bool needGroupBy = false;
            foreach (ObjectQueryPick item in listObject)
            {
                if (item.isGroupBy)
                {
                    needGroupBy = true;
                    break;
                }
            }
            if (needGroupBy)
            {
                queryString += "GROUP BY ";
                foreach (ObjectQueryPick item in listObject)
                {
                    if (item.isGroupBy)
                    {
                        queryString += String.Format("{0}.{1}, ", item.tablePick.tableName, item.columnPick.name);
                    }
                }
                queryString = queryString.Trim();
                queryString = queryString.Remove(queryString.Length - 1);
                queryString += "\n ";
            }
            /**
             * GROUP BY ORDER TYPE 
             **/
            bool needOrderType = false;
            foreach (ObjectQueryPick item in listObject)
            {
                if (!(string.IsNullOrEmpty(item.sortType) ||
                    string.IsNullOrWhiteSpace(item.sortType) || "None".Equals(item.sortType)))
                {
                    needOrderType = true;
                    break;
                }
            }
            if (needOrderType)
            {
                queryString += "ORDER BY ";
                foreach (ObjectQueryPick item in listObject)
                {
                    switch (item.sortType)
                    {
                        case "ASC":
                            queryString += String.Format("{0}.{1}, ", item.tablePick.tableName, item.columnPick.name);
                            break;
                        case "DESC":
                            queryString += String.Format("{0}.{1} DESC, ", item.tablePick.tableName, item.columnPick.name);
                            break;
                        default:
                            break;
                    }
                }
                queryString = queryString.Trim();
                queryString = queryString.Remove(queryString.Length - 1);
                queryString += "\n ";
            }
            return queryString;
        }

        private static string generateWhereSectionQuery(
            string queryString, 
            List<ObjectQueryPick> listObject, 
            List<SqlTable> listTable)
        {
            bool isHaveFKConstaint = false;
            bool isHaveCondition = false;
            foreach(SqlTable table in listTable)
            {
                if(table.listFK.Count > 0)
                {
                    foreach(string item in table.listFK)
                    {
                        string[] listData = item.Split("-");
                        if(listTable.Find(ele => ele.tableName.Equals(listData[1])) != null)
                        {
                            isHaveFKConstaint = true;
                            break;
                        }
                    }
                    if (isHaveFKConstaint) break;
                }
            }
            foreach(ObjectQueryPick item in listObject)
            {
                if(!(string.IsNullOrEmpty(item.condition) ||
                    string.IsNullOrWhiteSpace(item.condition)))
                {
                    isHaveCondition = true;
                    break;
                }
                if(item.orConditionList != null)
                {
                    if (item.orConditionList.Count > 0)
                    {
                        isHaveCondition = true;
                        break;
                    }
                }
            }
            Utils.log(string.Format("This shit must run isHaveFKConstaint: {0},isHaveCondition {1}", isHaveFKConstaint, isHaveCondition));
            if (isHaveFKConstaint || isHaveCondition)
            {
                queryString += "WHERE ";
                /**
                 * WHERE : JOIN SECTION
                 **/
                foreach (SqlTable table in listTable)
                {
                    if (table.listFK.Count > 0)
                    {
                        foreach (string item in table.listFK)
                        {
                            string[] listData = item.Split("-");
                            SqlTable checkConnect = listTable.Find(ele => ele.tableName.Equals(listData[1]));
                            if(checkConnect != null)
                            {
                                queryString += String.Format("({0}.{1} = {2}.{3}) AND ",
                                table.tableName, listData[0],
                                listData[1], listData[2]);
                            }
                        }
                    }
                }
                /**
                * WHERE: CONDITION SECTION
                **/
                foreach (ObjectQueryPick item in listObject)
                {
                    if (!(string.IsNullOrEmpty(item.condition) ||
                        string.IsNullOrWhiteSpace(item.condition)))
                    {
                        if (item.orConditionList != null && item.orConditionList.Count > 0)
                        {
                            queryString += String.Format("({0}.{1} = \'{2}\' OR ",
                            item.tablePick.tableName,
                            item.columnPick.name,
                            item.condition);
                            foreach (string orCondition in item.orConditionList)
                            {
                                if (!string.IsNullOrWhiteSpace(orCondition))
                                {
                                    queryString += String.Format("{0}.{1} = \'{2}\' OR ",
                                    item.tablePick.tableName,
                                    item.columnPick.name,
                                    orCondition);
                                }
                            }
                            queryString = queryString.Trim();
                            queryString = queryString.Remove(queryString.Length - 3);
                            queryString += ") AND ";
                        }
                        else
                        {
                            queryString += String.Format("{0}.{1} = \'{2}\' AND ",
                            item.tablePick.tableName,
                            item.columnPick.name,
                            item.condition);
                        }
                    }
                    else
                    {
                        if (item.orConditionList != null && item.orConditionList.Count > 0)
                        {
                            queryString += "(";
                            foreach (string orCondition in item.orConditionList)
                            {
                                if (!string.IsNullOrWhiteSpace(orCondition))
                                {
                                    queryString += String.Format("{0}.{1} = \'{2}\' OR ",
                                    item.tablePick.tableName,
                                    item.columnPick.name,
                                    orCondition);
                                }
                            }
                            queryString = queryString.Trim();
                            queryString = queryString.Remove(queryString.Length - 3);
                            queryString += ") AND ";
                        }
                    }
                }
                queryString = queryString.Trim();
                queryString = queryString.Remove(queryString.Length - 4);
                queryString += "\n ";
            }
            return queryString;
        }
    }
}
