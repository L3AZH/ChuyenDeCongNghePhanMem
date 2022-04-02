using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Models
{
    public class ObjectRequestFromJs
    {
        public List<SqlTable> tablePick { get; set; }
        public List<ObjectQueryPick> listObject { get; set; }
    }
}
