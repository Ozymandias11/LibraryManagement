using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace LibraryManagement.Extensions
{
    public static class ExcelExporter
    {
        public static byte[] ExportToExcel<T>(IEnumerable<T> data, string worksheetName)
        {
            using var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add(worksheetName);


            var properties = typeof(T).GetProperties();


            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = properties[i].Name;
            }


            int row = 2;
            foreach (var item in data)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[row, i + 1].Value = properties[i].GetValue(item);
                }
                row++;
            }


            worksheet.Cells.AutoFitColumns();


            var chart = worksheet.Drawings.AddChart("chart", eChartType.ColumnClustered);

           
            chart.Title.Text = $"{worksheetName} Overview";
            chart.SetPosition(1, 0, properties.Length + 1, 0);
            chart.SetSize(600, 400);

           
            var xAxisRange = worksheet.Cells[2, 1, row - 1, 1]; 
            var yAxisRange = worksheet.Cells[2, properties.Length, row - 1, properties.Length]; 

            var series = chart.Series.Add(yAxisRange, xAxisRange);
            series.Header = "Totals";


            return package.GetAsByteArray();
        }
    }
}
