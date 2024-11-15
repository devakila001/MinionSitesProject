namespace WebsiteMinion.Contracts
{
    public interface IRegisterWebsites
    {
        Task RegisterWebsiteAsync(string websiteUrl);
    }
}
