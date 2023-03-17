using System.Text;
using ChatEpt.DTOs;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class MessageService : IMessageService
{
    private readonly HttpClient _httpClient;
    private const string Url = "https://whatthecommit.com/index.txt";
    
    public MessageService(HttpClient httpClient)
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

        var answer = response.Content.ReadAsStringAsync().Result;
        return new MessageServiceDto(request, answer.Trim());
    }
}