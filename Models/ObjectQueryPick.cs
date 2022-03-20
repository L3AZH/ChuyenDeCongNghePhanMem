using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Models
{
    public class ObjectQueryPick
    {
        public SqlTable tablePick { get; set; }

        public SqlColumn columnPick { get; set; }

        public string columnNameRename { get; set; }

        public string total { get; set; }

        public string condition { get; set; }

        public bool isShow { get; set; }

        public string sortType { get; set; }

    }
}
