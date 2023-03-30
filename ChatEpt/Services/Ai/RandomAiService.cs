using ChatEpt.DTOs;
using ChatEpt.Services.Ai.Abstract;

namespace ChatEpt.Services.Ai;

public class RandomAiService : IAiService
{
    private readonly HttpClient _httpClient;

    private const string Url = "https://whatthecommit.com/index.txt";
    public RandomAiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string[] KeyWords { get; } = Array.Empty<string>();

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