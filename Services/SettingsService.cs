using nastrafarmapi.Data;

namespace nastrafarmapi.Services
{
    public class SettingsService
    {
        private readonly Logger<SettingsService> logger;
        private readonly ApplicationDbContext context;

        public SettingsService(Logger<SettingsService> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }
    }
}
