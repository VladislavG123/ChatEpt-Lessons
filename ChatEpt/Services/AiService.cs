using System.Collections;
using System.Text.Json.Serialization;
using ChatEpt.DTOs;
using ChatEpt.Models;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient;
    private const string DefaultAiUrl = "https://whatthecommit.com/index.txt";
    private const string StartupAiUrl = "https://itsthisforthat.com/api.php?json";

    public AiService(HttpClient httpClient)
    {
        // Got from IoC aka DI
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public MessageServiceDto GetAnswer(string request)
    {
        string response;
        
        if (request.ContainsAll(StringComparison.InvariantCultureIgnoreCase, "?", "startup", "idea"))
        {
           var httpResponseMessage = _httpClient.GetAsync(StartupAiUrl).Result;
           if (!httpResponseMessage.IsSuccessStatusCode)
           {
               throw new BadHttpRequestException($"Cannot get answer, status code: {httpResponseMessage.StatusCode}");
           }
            
           var responseFromAi = httpResponseMessage.Content.ReadFromJsonAsync<Hashtable>().Result;
           
           // touple, record, struct record, struct, class
           
           // json -> You can open like a "this" for "that"
           // {"this":"Soylent","that":"Your Mom"}
           response = $"You can open like a {responseFromAi["this"]} for {responseFromAi["that"]}";
        }
        else
        {
            var httpResponseMessage = _httpClient.GetAsync(DefaultAiUrl).Result;
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new BadHttpRequestException($"Cannot get answer, status code: {httpResponseMessage.StatusCode}");
            }

            response = httpResponseMessage.Content.ReadAsStringAsync().Result.Trim();
        }

        return new MessageServiceDto(request, response);
    }
}