using Microsoft.Data.SqlClient;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Services
{
    public class BackupService : IBackupService
    {
        private readonly ILogger<BackupService> logger;

        public BackupService(ILogger<BackupService> logger)
        {
            this.logger = logger;
        }

        public async Task<ServiceResult<string>> CreateAndSendBackupAsync()
        {
            return null;
        }
    }
}
