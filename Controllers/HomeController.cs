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
            return View(homeViewModel);
        }

    }
}
