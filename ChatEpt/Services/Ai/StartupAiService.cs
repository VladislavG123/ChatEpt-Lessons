using ChatEpt.DTOs;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services.Ai;

/// <inheritdoc />
public class StartupAiService : IAiService
{
    private readonly HttpClient _httpClient;
    
    private const string Url = "https://itsthisforthat.com/api.php?json";

    public StartupAiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    string[] IAiService.KeyWords { get; } = { "?", "startup", "idea" };

    /// <inheritdoc />
    public MessageDto GetAnswer(string request)
    {
        var httpResponseMessage = _httpClient.GetAsync(Url).Result;
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new BadHttpRequestException($"Cannot get answer, status code: {httpResponseMessage.StatusCode}");
        }
            
        var responseFromAi = httpResponseMessage.Content.ReadFromJsonAsync<StartupAiDto>().Result;

        return responseFromAi is null 
            ? new MessageDto(request, $"I don't know", false) 
            : new MessageDto(request, $"You can open like a {responseFromAi.This} for {responseFromAi.That}");
    }

    private class StartupAiDto
    {
        public string This { get; set; }
        public string That { get; set; }
    }
}