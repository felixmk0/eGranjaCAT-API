using nastrafarmapi.Services;

namespace nastrafarmapi.Interfaces
{
    public interface IBackupService
    {
        Task<ServiceResult<string>> CreateAndSendBackupAsync();
    }
}