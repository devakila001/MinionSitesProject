namespace SiteChecker.UI.ViewModels
{
    public class WebsiteInfoDTO
    {
        public required string WebsiteUrl { get; set; }
        public int MonitoringIntervalSeconds { get; set; }
        public bool IsUp { get; set; }
        public DateTime? LastCheckedAt { get; set; }
    }
}