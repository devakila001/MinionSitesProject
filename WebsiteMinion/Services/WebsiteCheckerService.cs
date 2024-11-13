//using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebsiteMinion.Contexts;
using WebsiteMinion.Models;
using Serilog;

namespace WebsiteMinion.Services
{
    public class WebsiteCheckerService      
    {
        private readonly ILogger _logger;
        private readonly WebsiteDbContext _context;
        public WebsiteCheckerService(ILogger logger,WebsiteDbContext websiteDbContext)
        {   
            _logger = logger;
            _context = websiteDbContext;
        }

        public async Task<bool> CheckExistsSitesAsync(string url)
        {
            _logger.Information($"Entering...Parameters:{nameof(url)}={url}");

            ArgumentException.ThrowIfNullOrWhiteSpace(url, nameof(url));


            var webSiteInfo = _context.websiteInfos.Where(whi => whi.WebsiteUrl == url).FirstOrDefault();
            var webSiteStausHistory = new WebsiteStatusHistory()
            {
              RequestSentAt = DateTime.UtcNow,
              WebsiteInfoId = webSiteInfo.Id, 
            };

            bool isSiteUp = false;
            try
            {
                _logger.Information($"Attempting to reach {url}");
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10); // Set a timeout
                    HttpResponseMessage response = await client.GetAsync(url);

                    _logger.Information($"Everything looks great at {url}");
                    webSiteStausHistory.HttpStatusCode = (int)response.StatusCode;


                    //if ((webStatusHistory.HttpStatusCode >= 400 && webStatusHistory.HttpStatusCode < 600))

                    if (webSiteStausHistory.HttpStatusCode == (int)HttpStatusCode.BadRequest)
                    {
                        webSiteStausHistory.StatusMessage = "Degraded";
                    }
                    else
                    {
                        webSiteStausHistory.StatusMessage = "Success";
                    }
                    webSiteStausHistory.IsUp = true;
                    // Check if status code indicates success
                    isSiteUp = response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.Error($"There was an {ex}");
                // Handle cases where the site is not reachable (DNS issues, etc.)
                webSiteStausHistory.IsUp = false;
                webSiteStausHistory.StatusMessage = ex.Message;
                webSiteStausHistory.HttpStatusCode = 0;
            }
            catch (TaskCanceledException ex)
            {
                _logger.Error($"There was an {ex}");
                // Handle timeouts
                _logger.Information("Request timed out.");
                webSiteStausHistory.IsUp = false;
                webSiteStausHistory.StatusMessage = ex.Message;
                webSiteStausHistory.HttpStatusCode = 0;
            }

            _context.websiteStatusHistories.Add(webSiteStausHistory);
            await _context.SaveChangesAsync();

            return isSiteUp;
        }
        
    }
}
