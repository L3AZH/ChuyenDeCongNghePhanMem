using CDCNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCNPM.Controllers
{
    public class UnitTestHomeController
    {

        public static void testGenerateQuery()
        {
            SqlTable chiNhanhTable = new SqlTable()
            {
                tableName = "ChiNhanh",
                columns = new List<SqlColumn>()
                    {
                        new SqlColumn()
                        {
                            tableName = "ChiNhanh",
                            name = "MACN"
                        },
                        new SqlColumn()
                        {
                            tableName = "ChiNhanh",
                            name = "ChiNhanh"
                        },
                        new SqlColumn()
                        {
                            tableName = "ChiNhanh",
                            name = "DIACHI"
                        },
                        new SqlColumn()
                        {
                            tableName = "ChiNhanh",
                            name = "SoDT"
                        }
                    },
                primaryKey = new List<string>() { "MACN" },
                listFK = new List<string>() { "MACN-NhanVien-MACN", "MACN-Kho-MACN" }
            };
            SqlTable khoTale = new SqlTable()
            {
                tableName = "Kho",
                columns = new List<SqlColumn>()
                    {
                        new SqlColumn()
                        {
                            tableName = "Kho",
                            name = "MAKHO"
                        },
                        new SqlColumn()
                        {
                            tableName = "Kho",
                            name = "TENKHO"
                        },
                        new SqlColumn()
                        {
                            tableName = "Kho",
                            name = "DIACHI"
                        },
                        new SqlColumn()
                        {
                            tableName = "Kho",
                            name = "MACN"
                        }
                    },
                primaryKey = new List<string>() { "MAKHO" },
                listFK = new List<string>() { }
            };
            SqlTable nhanVienTable = new SqlTable()
            {
                tableName = "NhanVien",
                columns = new List<SqlColumn>()
                    {
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "MANV"
                        },
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "HO"
                        },
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "TEN"
                        },
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "MACN"
                        },
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "DIACHI"
                        },
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "NGAYSINH"
                        },
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "LUONG"
                        },
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "MACN"
                        },
                        new SqlColumn()
                        {
                            tableName = "NhanVien",
                            name = "Trangthaixoa"
                        }
                    },
                primaryKey = new List<string>() { "MANV" },
                listFK = new List<string>() { "MANV-DatHang-MANV" }
            };
            SqlTable datHangTable = new SqlTable()
            {
                tableName = "DatHang",
                columns = new List<SqlColumn>()
                    {
                        new SqlColumn()
                        {
                            tableName = "DatHang",
                            name = "MasoDDH"
                        },
                        new SqlColumn()
                        {
                            tableName = "DatHang",
                            name = "NGAY"
                        },
                        new SqlColumn()
                        {
                            tableName = "DatHang",
                            name = "NhaCC"
                        },
                        new SqlColumn()
                        {
                            tableName = "DatHang",
                            name = "MANV"
                        }
                    },
                primaryKey = new List<string>() { "MasoDDH" },
                listFK = new List<string>() { }
            };
            List<ObjectQueryPick> listObject = new List<ObjectQueryPick>()
            {
                new ObjectQueryPick()
                {
                    tablePick = chiNhanhTable,
                    columnPick = new SqlColumn()
                    {
                        tableName = chiNhanhTable.tableName,
                        name = "ChiNhanh"
                    }
                },
                new ObjectQueryPick()
                {
                    tablePick = nhanVienTable,
                    columnPick = new SqlColumn()
                    {
                        tableName = nhanVienTable.tableName,
                        name = "TEN"
                    },
                    columnNameRename = "THis shit must run"
                },
                new ObjectQueryPick()
                {
                    tablePick = khoTale,
                    columnPick = new SqlColumn()
                    {
                        tableName = khoTale.tableName,
                        name = "DIACHI"
                    }
                },
            };

            List<SqlTable> listTable = new List<SqlTable>()
            {
                chiNhanhTable,
                khoTale,
                nhanVienTable,
                datHangTable
            };

            Utils.log(HomeController.generateQueryFromObjectQueryPick(listObject, listTable));
        }

    }
}
