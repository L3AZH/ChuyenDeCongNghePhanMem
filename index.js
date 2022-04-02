const mockdata = [
  {
    pKs: ["MANV"],
    fKs: ["NhanVien-MACN-ChiNhanh-MACN"],
    table_name: "NhanVien",
    columns: [
      {
        column_name: "MANV",
        talbe_name: "NhanVien",
      },
      {
        column_name: "HO",
        talbe_name: "NhanVien",
      },
      {
        column_name: "TEN",
        talbe_name: "NhanVien",
      },
      {
        column_name: "DIACHI",
        talbe_name: "NhanVien",
      },
      {
        column_name: "NGAYSINH",
        talbe_name: "NhanVien",
      },
      {
        column_name: "LUONG",
        talbe_name: "NhanVien",
      },
      {
        column_name: "MACN",
        talbe_name: "NhanVien",
      },
      {
        column_name: "TrangThaiXoa",
        talbe_name: "NhanVien",
      },
    ],
  },
  {
    pKs: ["MAVT"],
    fKs: [],
    table_name: "Vattu",
    columns: [
      {
        column_name: "MAVT",
        talbe_name: "Vattu",
      },
      {
        column_name: "TENVT",
        talbe_name: "Vattu",
      },
      {
        column_name: "DVT",
        talbe_name: "Vattu",
      },
      {
        column_name: "SOLUONGTON",
        talbe_name: "Vattu",
      },
    ],
  },
  {
    pKs: ["MAKHO"],
    fKs: ["Kho-MACN-ChiNhanh-MACN"],
    table_name: "Kho",
    columns: [
      {
        column_name: "MAKHO",
        talbe_name: "Kho",
      },
      {
        column_name: "TENKHO",
        talbe_name: "Kho",
      },
      {
        column_name: "DIACHI",
        talbe_name: "Kho",
      },
      {
        column_name: "MACN",
        talbe_name: "Kho",
      },
    ],
  },
  {
    pKs: ["MasoDDH"],
    fKs: ["DatHang-MANV-NhanVien-MANV"],
    table_name: "DatHang",
    columns: [
      {
        column_name: "MasoDDH",
        talbe_name: "DatHang",
      },
      {
        column_name: "NGAY",
        talbe_name: "DatHang",
      },
      {
        column_name: "NhaCC",
        talbe_name: "DatHang",
      },
      {
        column_name: "MANV",
        talbe_name: "DatHang",
      },
    ],
  },
  {
    pKs: ["MAPX"],
    fKs: ["PhieuXuat-MANV-NhanVien-MANV"],
    table_name: "PhieuXuat",
    columns: [
      {
        column_name: "MAPX",
        talbe_name: "PhieuXuat",
      },
      {
        column_name: "NGAY",
        talbe_name: "PhieuXuat",
      },
      {
        column_name: "HOTENKH",
        talbe_name: "PhieuXuat",
      },
      {
        column_name: "MANV",
        talbe_name: "PhieuXuat",
      },
    ],
  },
  {
    pKs: ["MasoDDH", "MAVT"],
    fKs: ["CTDDH-MAVT-Vattu-MAVT", "CTDDH-MasoDDH-DatHang-MasoDDH"],
    table_name: "CTDDH",
    columns: [
      {
        column_name: "MasoDDH",
        talbe_name: "CTDDH",
      },
      {
        column_name: "MAVT",
        talbe_name: "CTDDH",
      },
      {
        column_name: "SOLUONG",
        talbe_name: "CTDDH",
      },
      {
        column_name: "DONGIA",
        talbe_name: "CTDDH",
      },
    ],
  },
  {
    pKs: ["MAPN"],
    fKs: ["PhieuNhap-MANV-NhanVien-MANV", "PhieuNhap-MasoDDH-DatHang-MasoDDH"],
    table_name: "PhieuNhap",
    columns: [
      {
        column_name: "MAPN",
        talbe_name: "PhieuNhap",
      },
      {
        column_name: "NGAY",
        talbe_name: "PhieuNhap",
      },
      {
        column_name: "MasoDDH",
        talbe_name: "PhieuNhap",
      },
      {
        column_name: "MANV",
        talbe_name: "PhieuNhap",
      },
    ],
  },
  {
    pKs: ["MAPX", "MAVT"],
    fKs: ["CTPX-MAVT-Vattu-MAVT", "CTPX-MAPX-PhieuXuat-MAPX"],
    table_name: "CTPX",
    columns: [
      {
        column_name: "MAPX",
        talbe_name: "CTPX",
      },
      {
        column_name: "MAVT",
        talbe_name: "CTPX",
      },
      {
        column_name: "SOLUONG",
        talbe_name: "CTPX",
      },
      {
        column_name: "DONGIA",
        talbe_name: "CTPX",
      },
    ],
  },
  {
    pKs: ["MAPN", "MAVT"],
    fKs: ["CTPN-MAVT-Vattu-MAVT", "CTPN-MAPN-PhieuNhap-MAPN"],
    table_name: "CTPN",
    columns: [
      {
        column_name: "MAPN",
        talbe_name: "CTPN",
      },
      {
        column_name: "MAVT",
        talbe_name: "CTPN",
      },
      {
        column_name: "SOLUONG",
        talbe_name: "CTPN",
      },
      {
        column_name: "DONGIA",
        talbe_name: "CTPN",
      },
    ],
  },
];

const table_str_condition = (tableName, colName) => ` 
          <div   
            class="grid grid-cols-12 gap-3 mt-8 query-wrapper"
            data-col="${colName}"
            data-table="${tableName}"
          >
            <div class="content-table-name font-bold text-[12px] col-span-1">
              ${tableName}
            </div>
            <div
              class="content-table-fieldname font-bold text-[12px] col-span-1"
            >
              ${colName}
            </div>
            <div class="content-table-rename font-bold text-[12px] col-span-3">
              <input
                class="shadow appearance-none border border-neutral-300 rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
                id="rename-${colName}-${tableName}"
                type="text"
                placeholder="Rename field"
              />
            </div>
            <div class="font-bold text-[12px] col-span-1">
              <div class="flex justify-center">
                <div class="mb-3">
                  <select
                    id="total-select-${colName}-${tableName}"
                    class="form-select appearance-none block w-full px-3 py-1.5 text-base font-normal text-gray-700 bg-white bg-clip-padding bg-no-repeat border border-solid border-gray-300 rounded transition ease-in-out m-0 focus:text-gray-700 focus:bg-white focus:border-blue-600 focus:outline-none"
                    aria-label="Default select example"
                  >
                    <option value="None">Select</option>
                    <option value="MAX">Max</option>
                    <option value="MIN">Min</option>
                    <option value="COUNT">Count</option>
                    <option value="SUM">Sum</option>
                    <option value="GROUP BY">Group by</option>
                  </select>
                </div>
              </div>
            </div>
            <div class="font-bold text-[12px] col-span-1">
              <div class="mb-3">
                <select
                  id="isSort-${colName}-${tableName}"
                  class="form-select appearance-none block w-full px-3 py-1.5 text-base font-normal text-gray-700 bg-white bg-clip-padding bg-no-repeat border border-solid border-gray-300 rounded transition ease-in-out m-0 focus:text-gray-700 focus:bg-white focus:border-blue-600 focus:outline-none"
                  aria-label="Default select example"
                >
                  <option value="None">None</option>
                  <option value="ASC">Asc</option>
                  <option value="DESC">Desc</option>
                </select>
              </div>
            </div>
            <div class="font-bold text-[12px] col-span-1">
              <div class="flex">
                <label class="flex items-center">
                  <input
                    id="isShow-${colName}-${tableName}"
                    type="checkbox"
                    class="form-checkbox h-5 w-5 text-gray-600"
                    value="IsShow"
                  />
                  <span class="ml-[5px]">isShow</span>
                </label>
              </div>
            </div>
            <div class="font-bold text-[12px] col-span-2">
              <input
                id="condition-${colName}-${tableName}"
                class="shadow appearance-none border border-neutral-300 rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
                id="Rename"
                type="text"
                placeholder="Condition"
              />
            </div>
            <div class="font-bold text-[12px] col-span-2">
              <input
                id="or-condition-${colName}-${tableName}"
                class="shadow appearance-none border border-neutral-300 rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
                id="Rename"
                type="text"
                placeholder="Or conditiom"
              />
            </div>
          </div>`;

let selectedTables = [];
let selectedCols = [];
const initOption = {
  total: null,
  isSort: null,
  isShow: null,
  condition: "",
  orCondition: "",
  reName: "",
};
const checkboxTables = document.querySelectorAll(".table-sql-check");
const checkboxColsOfTableHtml = document.querySelectorAll(".col-table-check");

$(document).ready(function () {
  //////

  let checkboxTablesHtml = "";
  let checkboxColsOfTableHtml = "";
  const createQueryBtn = $("#create-query");
  const hide = ` <div
                class="hide bg-neutral-100/[0.5] w-full h-full absolute rounded top-0 left-0"
              ></div>`;
  mockdata.forEach((ele) => {
    checkboxTablesHtml = checkboxTablesHtml.concat(`
     <div class="flex mt-4">
          <label class="flex items-center">
            <input
              type="checkbox"
              class="form-checkbox h-5 w-5 text-gray-600 table-sql-check"
              value=${ele.table_name}
            />
            <span class="ml-[5px]">${ele.table_name}</span>
          </label>
        </div>
  `);
  });

  $(".sidebar_table_sql").append(checkboxTablesHtml);
  //////

  /////
  mockdata.forEach((ele) => {
    let colsChecHtml = "";
    ele.columns.forEach((ele) => {
      colsChecHtml = colsChecHtml.concat(`
              <div class="flex flex-wrap mr-[10px]">
                <div class="flex mt-4">
                  <label class="flex items-center">
                    <input
                      type="checkbox"
                      class="form-checkbox h-5 w-5 text-gray-600 col-table-check"
                      value="${ele.column_name}"
                      data-table="${ele.talbe_name}"
                    />
                    <span class="ml-[5px] font-medium">${ele.column_name}</span>
                  </label>
                </div>
              </div>
      `);
    });

    checkboxColsOfTableHtml = checkboxColsOfTableHtml.concat(`
              <div
                class="relative column-item border-solid border-2 border-neutral-300 px-[20px] py-[15px] rounded mt-3"
                data-table = "${ele.table_name}" 
              >
              ${hide}
              <h2 class="font-bold">${ele.table_name}</h2>
              
               <div class="flex flex-wrap">
                 ${colsChecHtml}
               </div>
              
            </div>
      `);
  });
  /////

  ////////

  $(".col-wrapper").append(checkboxColsOfTableHtml);

  ////////
  $(".table-sql-check").on("click", (e) => {
    const { value, checked } = e.target;

    if (checked) {
      selectedTables.push(
        mockdata.find((ele) => ele.table_name === value).table_name
      );
      $(`.column-item[data-table='${value}'] .hide`).remove();
    } else {
      selectedTables = selectedTables.filter((ele) => ele !== value);

      $(`.column-item[data-table='${value}']`).append(hide);
      $(`.column-item[data-table='${value}'] .col-table-check`).prop(
        "checked",
        false
      );
      $(`#table-section .query-wrapper[data-table="${value}"]`).remove();
    }
  });

  $(".col-table-check").on("click", (e) => {
    const { value, checked, dataset } = e.target;
    const table = dataset.table;

    const foundCol = mockdata
      .find((ele) => ele.table_name === table)
      .columns.find((ele) => ele.column_name === value);
    if (checked) {
      selectedCols.push({ ...foundCol, options: initOption });
      $("#table-section").append(
        table_str_condition(foundCol.talbe_name, foundCol.column_name)
      );
    } else {
      selectedCols = selectedCols.filter(
        (ele) => ele?.column_name !== value && ele?.talbe_name === table
      );

      $(
        `#table-section .query-wrapper[data-col="${foundCol.column_name}"]`
      ).remove();
    }
  });

  createQueryBtn.on("click", () => {
    selectedCols = selectedCols.map((ele) => ({
      ...ele,
      options: {
        ...ele.options,
        reName: $(`#rename-${ele.column_name}-${ele.talbe_name}`).val(),
        isShow: $(`#isShow-${ele.column_name}-${ele.talbe_name}`).is("checked"),
        isSort: $(`#isSort-${ele.column_name}-${ele.talbe_name}`).val(),
        total: $(`#total-select-${ele.column_name}-${ele.talbe_name}`).val(),
        condition: $(`#condition-${ele.column_name}-${ele.talbe_name}`).val(),
        orCondition: $(
          `#or-condition-${ele.column_name}-${ele.talbe_name}`
        ).val(),
      },
    }));

    console.log(selectedCols);
  });
});
