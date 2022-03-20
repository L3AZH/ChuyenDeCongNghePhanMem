using CDCNPM.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Repositories
{
    public interface ISqlRepository
    {
        public List<SqlTable> getListSqlTable(IConfiguration config);

        public DataSet getDataSetFromRawQuery(IConfiguration config, string queryString);

    }
}
