using nastrafarmapi.Entities.Excel;

namespace nastrafarmapi.Interfaces
{
    public interface IExcelService
    {
        Task<MemoryStream> GenerateExcelAsync<T>(IEnumerable<T> data, List<ExcelColumnMap<T>> maps, string sheetName);
    }
}