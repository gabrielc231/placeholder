/*using Microsoft.AspNetCore.Mvc.Testing;

[TestClass]
public class FileControllerTests
{
    private HttpClient _client;

    [TestInitialize]
    public void Setup()
    {
        var application = new WebApplicationFactory<Program>();
        _client = application.CreateClient();
    }

    [TestMethod]
    public async Task GetFiles_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/File/Index");

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode);
    }
}*/
