using Microsoft.EntityFrameworkCore;
using Moq;
using Serilog;
using WebsiteMinion.Contexts;
using WebsiteMinion.Contracts;


namespace SiteChecker.Tests;

public class MinionSiteCheckerTests
{

    Mock<IWebsiteChecker> mockIWebsiteChecker;
    Mock<ILogger> mockLogger;
    WebsiteDbContext inMemoryWebsiteContext;

    public MinionSiteCheckerTests() {
        var options = new DbContextOptionsBuilder<WebsiteDbContext>()
            .UseInMemoryDatabase(databaseName: "MinionSiteDb" + Guid.NewGuid())
            .Options;
        mockLogger = new Mock<ILogger>();
        inMemoryWebsiteContext = new WebsiteDbContext(options);
        mockIWebsiteChecker = new Mock<IWebsiteChecker>();
    }



    [Fact]
    public async Task CheckWebsiteStatus()
    { 
        //Arrange
        IWebsiteChecker websiteChecker = mockIWebsiteChecker.Object;


        //Act
        bool isSiteExist = await websiteChecker.CheckExistsSitesAsync("exisitingsite");
        //Assert

        Assert.False(isSiteExist);
    }

    [Fact]
    public async Task CheckWebsiteStatus_NonExist()
    {
        //Arrange
        IWebsiteChecker websiteChecker = mockIWebsiteChecker.Object;


        //Act
        bool isSiteExist = await websiteChecker.CheckExistsSitesAsync("nonexisitingsite");
        //Assert

        Assert.False(isSiteExist);
    }
}

