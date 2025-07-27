using ClosedXML.Excel;
using nastrafarmapi.Entities.Excel;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<MemoryStream> GenerateExcelAsync<T>(IEnumerable<T> data, List<ExcelColumnMap<T>> maps, string sheetName)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            for (int col = 0; col < maps.Count; col++) worksheet.Cell(1, col + 1).Value = maps[col].Header;

            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < maps.Count; col++)
                {
                    var cell = worksheet.Cell(row, col + 1);
                    cell.SetValue(cell.Value);
                }

                row++;
            }

            worksheet.Columns().AdjustToContents();

            worksheet.Style.Font.SetFontSize(12);
            worksheet.Style.Font.SetFontName("Arial");
            worksheet.Style.Fill.BackgroundColor = XLColor.White;

            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Row(1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            worksheet.Row(1).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            worksheet.Row(1).Style.Border.OutsideBorderColor = XLColor.Black;
            worksheet.Row(1).Style.Border.InsideBorderColor = XLColor.Black;

            for (int i = 0; i < row; i++)
            {
                var rowCells = worksheet.Row(i + 1);
                foreach (var cell in rowCells.Cells())
                {
                    cell.Style.Border.OutsideBorderColor = XLColor.Black;
                    cell.Style.Border.InsideBorderColor = XLColor.Black;
                }
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
    }
}
