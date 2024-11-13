using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public DbSet<WebsiteInfo> websiteInfos { get; set; }

    public DbSet<WebsiteStatusHistory> websiteStatusHistories { get; set; }

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
