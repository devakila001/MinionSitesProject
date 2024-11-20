using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WebsiteMinion.Contexts;
using WebsiteMinion.Contracts;
using WebsiteMinion.Configs;

namespace WebsiteMinion.Services;
public class TimedJobService : BackgroundService
{
    private readonly Serilog.ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IWebsiteChecker _websiteChecker;
    private readonly TimeSpan _jobInterval;

    public TimedJobService(Serilog.ILogger logger, IOptions<JobSettings> jobSettings, IServiceProvider serviceProvider, IWebsiteChecker websiteChecker)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _websiteChecker = websiteChecker;
        _jobInterval = TimeSpan.FromSeconds(jobSettings.Value.IntervalSeconds);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Information("Timed Job Service running.");
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var websiteContext = scope.ServiceProvider.GetRequiredService<WebsiteDbContext>();

                var listOfWebsiteToMonitor = await websiteContext.Websiteinfos.Where(wsi => wsi.MonitoringEnabled == true).ToListAsync();

                // Run your job logic with the scoped WebsiteContext here
                _logger.Information("Running the job at: {time}", DateTimeOffset.Now);
                _logger.Information($"Websites being monitored = {listOfWebsiteToMonitor.Count}");

                foreach (var webSiteInfo in listOfWebsiteToMonitor)
                {
                    var isUp = await _websiteChecker.CheckExistsSitesAsync(webSiteInfo.WebsiteUrl);
                    _logger.Information($"Website {webSiteInfo.WebsiteUrl} is up = {isUp}");
                }
            }

            _logger.Information($"Waiting for {_jobInterval} seconds.");
            await Task.Delay(_jobInterval, stoppingToken);
        }
        _logger.Information("Timed Job Service is stopping.");
    }
}
