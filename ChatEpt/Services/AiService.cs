using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using ChatEpt.DTOs;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient;
    private const string DefaultAiUrl = "https://whatthecommit.com/index.txt";
    private const string StartupAiUrl = "https://itsthisforthat.com/api.php?json";
    private const string WhoIsAiUrl = "https://randomuser.me/api";

    private const string WhoIsInformation = "{Fullname} is {Gender}, who lives in {City} at {Country}. " +
                                            "{HeOrShe} is {Age} year old, {HerOrHis} birthsday is {DoB}. " +
                                            "You can reach {HimOrHer} through {Email} or {Phone}." ;

    public AiService(HttpClient httpClient)
    {
        // Got from IoC aka DI
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public MessageServiceDto GetAnswer(string request)
    {
        string response;
        bool needToSave = true;
        
        if (request.ContainsAll(StringComparison.InvariantCultureIgnoreCase, "?", "startup", "idea"))
        {
           var httpResponseMessage = _httpClient.GetAsync(StartupAiUrl).Result;
           if (!httpResponseMessage.IsSuccessStatusCode)
           {
               throw new BadHttpRequestException($"Cannot get answer, status code: {httpResponseMessage.StatusCode}");
           }
            
           var responseFromAi = httpResponseMessage.Content.ReadFromJsonAsync<Hashtable>().Result;

           response = $"You can open like a {responseFromAi["this"]} for {responseFromAi["that"]}";
        }
        else if (request.ContainsAll(StringComparison.InvariantCultureIgnoreCase, "Who", "is", "?"))
        {
            var httpResponseMessage = _httpClient.GetAsync(WhoIsAiUrl).Result;
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new BadHttpRequestException($"Cannot get answer, status code: {httpResponseMessage.StatusCode}");
            }
            
            try
            {
                var responseFromAi = httpResponseMessage.Content.ReadFromJsonAsync<WhoIsAiDto>().Result;
                var person = responseFromAi!.People.FirstOrDefault();

                bool IsMale(string gender) => gender.ToLower().Equals("male");
            
                response = new StringBuilder(WhoIsInformation)
                    .Replace("{Fullname}", new Regex(@"Who is ([\w ]+)?").Match(request).Groups[1].Value.Trim())
                    .Replace("{Gender}", person.Gender)
                    .Replace("{City}", person.Location.City)
                    .Replace("{Country}", person.Location.Country)
                    .Replace("{HeOrShe}", IsMale(person.Gender) ? "He" : "She")
                    .Replace("{Age}", person.Dob.Age.ToString())
                    .Replace("{HerOrHis}", IsMale(person.Gender) ? "his" : "her")
                    .Replace("{DoB}", person.Dob.Date.ToString("dd.MM.yyyy"))
                    .Replace("{HimOrHer}", IsMale(person.Gender) ? "him" : "her")
                    .Replace("{Email}", person.Email)
                    .Replace("{Phone}", "+" + person.Phone.Replace(" ", ""))
                    .ToString();
            }
            catch (NullReferenceException)
            {
                needToSave = false;
                response = "This person is not found";
            }
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

        return new MessageServiceDto(request, response, needToSave);
    }
}