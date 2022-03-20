using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Models
{
    public class SqlTable
    {
        public string tableName { get; set; }
        public List<string> primaryKey { get; set; }
        /**
         * primarykey name in this table - table reference - key name in that table reference
         * example 
         * NhanVien(MSNV, TENNV, MACHINHANH)-ChiNhanh(MACN, TENCN)
         * At object ChiNhanh item of listFK belike: MACN(primary key) - NhanVien(table ref) - MACHINHANH(foreign key)
         * MACN-NhanVien-MACHINHANH
         **/
        public List<string> listFK { get; set; }
        public List<SqlColumn> columns { get; set; }
    }
}
