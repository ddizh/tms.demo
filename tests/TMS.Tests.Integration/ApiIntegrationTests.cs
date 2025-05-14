using TMS.ServiceDefaults;

namespace TMS.Tests.Integration;

public class ApiIntegrationTests : IClassFixture<AppHostFixture>
{
    private readonly AppHostFixture _appHostFixture;

    public ApiIntegrationTests(AppHostFixture appHostFixture)
    {
        _appHostFixture = appHostFixture;

        if (_appHostFixture.Application == null)
        {
            throw new Exception("Application has not been initialized");
        }
    }

    [Fact]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        var httpClient = _appHostFixture.Application!.CreateHttpClient(KnownResources.ApiResourceName);
        await _appHostFixture.Application!.ResourceNotifications.WaitForResourceHealthyAsync(KnownResources.ApiResourceName)
            .WaitAsync(AppHostFixture.DefaultTimeout);
        var response = await httpClient.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}