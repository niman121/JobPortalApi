using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using JobPortal.Service.Dtos;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;

namespace JobPortal.Service.Extension
{
    public class MultimediaExt
    {
        public static byte[] GetBytesForExportToExcel_MultipleSheets(List<MultipleSheetsDto> sheets)
        {
            ExcelPackage p = new ExcelPackage();

            int workSheetNo = 1;

            foreach (var s in sheets)
            {
                var dt = s.dt;
                p.Workbook.Worksheets.Add(s.WorkSheetName);
                ExcelWorksheet ws = p.Workbook.Worksheets[workSheetNo];
                ws.Name = s.WorkSheetName; //Setting Sheet's name
                ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
                ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

                ws.Cells[1, 1].Value = s.ReportHeading; // Heading Name
                ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true; //Merge columns start and end range
                ws.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true; //Font should be bold
                ws.Cells[1, 1, 1, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet 
                                                                                                                 //ws.Cells[1, 1, 1, dt.Columns.Count].AutoFitColumns();
                int rowIndex = 2;

                CreateHeader(ws, ref rowIndex, dt);
                CreateData(ws, ref rowIndex, dt);
                ws.Cells.AutoFitColumns(10, 30);
                workSheetNo++;
            }

            byte[] filedata = p.GetAsByteArray();
            //File.WriteAllBytes(@"D:\temp\reports\VisitReport.xls", filedata);

            return filedata;
        }

        private static void CreateHeader(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 1;
            foreach (DataColumn dc in dt.Columns) //Creating Headings
            {
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting Top/left,right/bottom borders.
                var border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                cell.Style.Font.Bold = true;

                //Setting Value in cell
                cell.Value = dc.ColumnName;
                colIndex++;
            }
        }


        private static void CreateData(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            foreach (DataRow dr in dt.Rows) // Adding Data into rows
            {
                colIndex = 1;
                rowIndex++;

                foreach (DataColumn dc in dt.Columns)
                {
                    var cell = ws.Cells[rowIndex, colIndex];

                    if (dc.ColumnName == "NotifyManager" || dc.ColumnName == "Admin" || dc.ColumnName == "EntireDayTracking" || dc.ColumnName == "NotifyAdmin")
                    {
                        if (Convert.ToString(dr[dc.ColumnName]) == "Disabled")
                        {
                            cell.Value = (dr[dc.ColumnName]);
                            cell.Style.Font.Color.SetColor(Color.Black);
                        }
                        else
                        {
                            cell.Value = (dr[dc.ColumnName]);
                            cell.Style.Font.Color.SetColor(Color.Green);
                        }
                    }
                    else
                    {
                        //Setting Value in cell
                        cell.Value = (dr[dc.ColumnName]);
                    }

                    //Setting borders of cell
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = border.Bottom.Style = ExcelBorderStyle.Thin;

                    colIndex++;
                }
            }
        }

    }
}
