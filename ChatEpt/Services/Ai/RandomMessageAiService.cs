using ChatEpt.DTOs;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services.Ai;

/// <inheritdoc />
public class RandomMessageAiService : IAiService
{
    private readonly HttpClient _httpClient;

    private const string Url = "https://whatthecommit.com/index.txt";

    public RandomMessageAiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public string[] KeyWords { get; } = Array.Empty<string>();
    
    /// <inheritdoc />
    public MessageDto GetAnswer(string request)
    {
        var httpResponseMessage = _httpClient.GetAsync(Url).Result;
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new BadHttpRequestException($"Cannot get answer, status code: {httpResponseMessage.StatusCode}");
        }

        return new MessageDto(request, httpResponseMessage.Content.ReadAsStringAsync().Result.Trim());
    }
}