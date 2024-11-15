

using Serilog;
using WebsiteMinion.Contexts;
using WebsiteMinion.Models;

namespace WebsiteMinion;

public class RegistrationHelper
{
    private readonly ILogger _logger;
    private readonly WebsiteDbContext _context;

    public RegistrationHelper(ILogger logger, WebsiteDbContext websiteDbContext)
    {
        _logger = logger;
        _context = websiteDbContext;
    }

    public async Task RegisterWebsiteAsync(string websiteUrl)
    {
        _logger.Information($"Entering ...Parameters:{nameof(websiteUrl)}={websiteUrl}");

        ArgumentException.ThrowIfNullOrWhiteSpace(websiteUrl, nameof(websiteUrl));

        var websiteInfo = new WebsiteInfo()
        {
            WebsiteUrl = websiteUrl,
            CreatedAt = DateTime.UtcNow,
            RegisteredAt = DateTime.UtcNow,
            MonitoringEnabled = true,
        };

        _context.Websiteinfos.Add(websiteInfo);
        await _context.SaveChangesAsync();

        _logger.Information($"Site registered successfully ... Site id:{nameof(websiteInfo)}={websiteInfo.Id}");


    }
}
