using OfficeOpenXml;

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

            return package.GetAsByteArray();
        }
    }
}
