using Microsoft.EntityFrameworkCore;
using WebsiteMinion.Models;
using static WebsiteMinion.Common.Constants;

namespace WebsiteMinion.Contexts;

public class WebsiteDbContext : DbContext
{
    public WebsiteDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public WebsiteDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

    }

    public DbSet<WebsiteInfo> Websiteinfos { get; set; }

    public DbSet<WebsiteStatusHistory> Websitestatushistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = Environment.GetEnvironmentVariable(ConnectionStringKeys.MinionSiteDb)!;
            optionsBuilder.UseNpgsql(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }


}
