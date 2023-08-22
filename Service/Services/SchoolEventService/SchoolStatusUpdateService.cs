using Microsoft.Extensions.Hosting;
using Service.Services.ScheduleManager;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Services.SchoolEventService
{
    public class SchoolStatusUpdateService : BackgroundService
    {
        private readonly StatusUpdateScheduler _statusUpdateScheduler;

        public SchoolStatusUpdateService(StatusUpdateScheduler statusUpdateScheduler)
        {
            _statusUpdateScheduler = statusUpdateScheduler;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _statusUpdateScheduler.StartAsync(stoppingToken);
        }


    }
}
