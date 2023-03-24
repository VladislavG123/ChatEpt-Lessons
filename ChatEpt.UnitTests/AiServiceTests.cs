using System.Text.RegularExpressions;
using ChatEpt.Services;

namespace ChatEpt.UnitTests;

public class AiServiceTests
{
    [Test]
    [TestCase("Can you propose a startup idea for me?")]
    [TestCase("Do you have any recommendations for a startup idea?")]
    [TestCase("I'm looking for suggestions for a new startup idea, can you help me out?")]
    [TestCase("Could you offer some ideas for a startup I could create?")]
    [TestCase("What startup ideas would you suggest I pursue?")]
    [TestCase("I'm in need of some startup inspiration, can you give me any ideas?")]
    [TestCase("Can you provide me with a startup idea that I could work on?")]
    [TestCase("I'm searching for a potential startup idea, do you have any suggestions?")]
    [TestCase("Could you give me some startup ideas that you think might work well?")]
    [TestCase("I'm seeking your advice on a potential startup idea that I could pursue. What do you suggest?")]
    public void AiService_StartupIdea(string request)
    {
        // Arrange
        var aiService = new AiService(new HttpClient());

        // Act
        var result = aiService.GetAnswer(request);
        
        // Assert
        Assert.True(Regex.IsMatch(result.Answer, "You can open like a .+ for .+"));
    }
}