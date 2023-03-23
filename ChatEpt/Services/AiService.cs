using ChatEpt.DTOs;
using ChatEpt.Models;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient;
    private const string Url = "https://whatthecommit.com/index.txt";

    public AiService(HttpClient httpClient)
    {
        // Got from IoC aka DI
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public MessageServiceDto GetAnswer(string request)
    {
        var response = _httpClient.GetAsync(Url).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new BadHttpRequestException($"Cannot get answer, status code: {response.StatusCode}");
        }

        return new MessageServiceDto(request, response.Content.ReadAsStringAsync().Result.Trim());
    }
}