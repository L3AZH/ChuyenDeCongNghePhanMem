using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Models
{
    public class SqlTable
    {
        public string tableName { get; set; }

        public List<SqlColumn> columns;
    }
}
