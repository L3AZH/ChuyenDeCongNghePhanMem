using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Models
{
    public class SqlColumn
    {
        public SqlColumn() { }
        public SqlColumn(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
        public string name  { get; set; }
        public string type { get; set; }
    }
}
