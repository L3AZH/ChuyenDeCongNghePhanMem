using System;
using System.Data;
using System.Drawing;
using DevExpress.XtraReports.UI;

namespace CDCNPM.Reports
{
    public partial class SampleReport
    {
        public string query { get; set; }
        public SampleReport()
        {
            InitializeComponent();
        }

        public static void InitBands(XtraReport rep)
        {
            DetailBand detail = new DetailBand();
            PageHeaderBand pageHeader = new PageHeaderBand();
            ReportHeaderBand reportHeader = new ReportHeaderBand();
            ReportFooterBand reportFooter = new ReportFooterBand();

            reportHeader.HeightF = 40;
            detail.HeightF = 20;
            reportFooter.HeightF = 380;
            pageHeader.HeightF = 20;
            rep.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] { reportHeader, detail, pageHeader, reportFooter });
        }
        public static void InitDetailsBaseXRTable(XtraReport rep, DataSet ds, String tit)
        {
            ds = ((DataSet)rep.DataSource);
            int colCount = ds.Tables[0].Columns.Count;
            int colWidth = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right)) / colCount;
            rep.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
            XRLabel title = new XRLabel();
            title.Text = tit;
            title.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            title.ForeColor = Color.Red;
            title.Font = new Font("Tahoma", 20, FontStyle.Bold, GraphicsUnit.Pixel);
            title.Width = Convert.ToInt32(rep.PageWidth - 50);


            // Create a table to represent headers
            XRTable tableHeader = new XRTable();
            tableHeader.Height = 40;
            tableHeader.BackColor = Color.PeachPuff;
            tableHeader.ForeColor = Color.Black;
            tableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            tableHeader.Font = new Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            tableHeader.Width = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right));
            tableHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100.0F);
            XRTableRow headerRow = new XRTableRow();
            headerRow.Width = tableHeader.Width;
            tableHeader.Rows.Add(headerRow);
            tableHeader.BeginInit();

            /*Create a table to display data*/
            XRTable tableDetail = new XRTable();
            tableDetail.Height = 20;
            tableDetail.Width = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right));
            tableDetail.Font = new Font("Tahoma", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            XRTableRow detailRow = new XRTableRow();
            detailRow.Width = tableDetail.Width;
            tableDetail.Rows.Add(detailRow);
            tableDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100.0F);
            tableDetail.BeginInit();
            tableDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            tableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            /*Create table cells, fill the header cells with text, bind the cells to data*/
            for (int i = 0; i < colCount; i++)
            {
                XRTableCell headerCell = new XRTableCell();
                headerCell.Text = ds.Tables[0].Columns[i].Caption;
                XRTableCell detailCell = new XRTableCell();
                detailCell.DataBindings.Add("Text", null, ds.Tables[0].Columns[i].Caption);

                detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                /*
                if (i == 0)
                {
                    headerCell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                }
                else
                {
                    headerCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                }*/

                headerCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.All;

                headerCell.Width = colWidth / colCount * (i + 1);
                detailCell.Width = colWidth / colCount * (i + 1);
                detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;

                //detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;

                /*Place the cells into the corresponding tables*/
                headerRow.Cells.Add(headerCell);
                detailRow.Cells.Add(detailCell);
            }

            tableHeader.EndInit();
            tableDetail.EndInit();
            /*Place the table onto a report's Detail band*/
            rep.Bands[BandKind.ReportHeader].Controls.Add(title);
            rep.Bands[BandKind.PageHeader].Controls.Add(tableHeader);
            rep.Bands[BandKind.Detail].Controls.Add(tableDetail);
        }
    }
}
