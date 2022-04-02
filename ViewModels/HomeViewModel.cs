using CDCNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.ViewModels
{
    public class HomeViewModel
    {
        public List<SqlTable> listTable { get; set; }

        public List<SqlTable> listTablePick { get; set; }

        public List<ObjectQueryPick> listObject { get; set; }
    }
}
