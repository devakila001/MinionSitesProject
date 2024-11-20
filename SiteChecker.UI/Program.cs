using Serilog;
using WebsiteMinion.Configs;
using WebsiteMinion.Contexts;
using WebsiteMinion.Contracts;
using WebsiteMinion.Services;

namespace SiteChecker.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add support to logging with SERILOG
            builder.Host.UseSerilog((context, configuration) =>
             configuration.ReadFrom.Configuration(context.Configuration));

            var jobSettings = builder.Configuration.GetSection("JobSettings").Get<JobSettings>();

            builder.Services.Configure<JobSettings>(builder.Configuration.GetSection("JobSettings"));

            if (jobSettings!.IntervalSeconds > 0)
            {
                builder.Services.AddSingleton<WebsiteDbContext>();
            }
            else
            {
                builder.Services.AddDbContext<WebsiteDbContext>();
            }

            if (jobSettings!.IntervalSeconds > 0)
            {
               builder.Services.AddSingleton<IWebsiteChecker, WebsiteCheckerService>();
                // Register the background job service
                builder.Services.AddHostedService<TimedJobService>();
            }
            else
            {
                builder.Services.AddScoped<IWebsiteChecker, WebsiteCheckerService>();
            }

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Add support to logging request with SERILOG
            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
