using ChatEpt.DTOs;
using ChatEpt.Models;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class MessageService : IMessageService
{
    private readonly HttpClient _httpClient;
    private readonly IBadWordChecker _badWordChecker;
    private readonly ApplicationContext _applicationContext;
    private const string Url = "https://whatthecommit.com/index.txt";

    public MessageService(HttpClient httpClient, IBadWordChecker badWordChecker, ApplicationContext applicationContext)
    {
        // Got from IoC aka DI
        _httpClient = httpClient;
        _badWordChecker = badWordChecker;
        _applicationContext = applicationContext;
    }

    /// <inheritdoc/>
    public MessageServiceDto GetAnswer(string request)
    {
        if (_badWordChecker.HasBadWordInText(request))
        {
            return SaveAndReturnMessageServiceDto(request, "Please, don't use bad words!");
        }
        
        var response = _httpClient.GetAsync(Url).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new BadHttpRequestException($"Cannot get answer, status code: {response.StatusCode}");
        }

        return SaveAndReturnMessageServiceDto(request, response.Content.ReadAsStringAsync().Result.Trim());
    }

    /// <summary>
    /// Save, return dto
    /// </summary>
    /// <returns></returns>
    private MessageServiceDto SaveAndReturnMessageServiceDto(string request, string response)
    {
        _applicationContext.Messages.Add(new MessageEntity
        {
            Request = request,
            Response = response
        });
        _applicationContext.SaveChanges();

        return new MessageServiceDto(request, response);
    }
}