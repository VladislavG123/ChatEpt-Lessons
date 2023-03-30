using System.Text;
using System.Text.RegularExpressions;
using ChatEpt.DTOs;
using ChatEpt.Services.Ai.Abstract;

namespace ChatEpt.Services.Ai;

public interface IWhoIsAiService : IAiService
{
}

public class WhoIsAiService : IWhoIsAiService
{
    private readonly HttpClient _httpClient;

    private const string Url = "https://randomuser.me/api";

    private const string ResponseTemplate = "{Fullname} is {Gender}, who lives in {City} at {Country}. " +
                                            "{HeOrShe} is {Age} year old, {HerOrHis} birthsday is {DoB}. " +
                                            "You can reach {HimOrHer} through {Email} or {Phone}.";

    public WhoIsAiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string[] KeyWords { get; } = { "Who", "is", "?" };

    public MessageDto GetAnswer(string request)
    {
        var httpResponseMessage = _httpClient.GetAsync(Url).Result;
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new BadHttpRequestException($"Cannot get answer, status code: {httpResponseMessage.StatusCode}");
        }

        try
        {
            var responseFromAi = httpResponseMessage.Content.ReadFromJsonAsync<WhoIsAiDto>().Result;
            var person = responseFromAi!.People.FirstOrDefault();

            bool IsMale(string gender) => gender.ToLower().Equals("male");

            return new MessageDto(request, new StringBuilder(ResponseTemplate)
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
                .ToString());
        }
        catch (NullReferenceException)
        {
            return new MessageDto(request, "This person is not found", false);
        }
    }
}