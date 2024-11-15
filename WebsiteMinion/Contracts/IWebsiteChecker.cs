namespace WebsiteMinion.Contracts;

public interface IWebsiteChecker
{
    Task<bool> CheckExistsSitesAsync(string url);
}
