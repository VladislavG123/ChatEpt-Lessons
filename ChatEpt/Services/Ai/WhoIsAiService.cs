using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using ChatEpt.DTOs;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services.Ai;

/// <inheritdoc />
public class WhoIsAiService : IAiService
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

    /// <inheritdoc />
    string[] IAiService.KeyWords { get; } = { "Who", "is", "?" };

    /// <inheritdoc />
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

            return new MessageDto(request,
                new StringBuilder(ResponseTemplate)
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
    
    private class WhoIsAiDto
    {
        [JsonPropertyName("results")] 
        public List<Person> People { get; set; }

        public class Person
        {
            public string Gender { get; set; }
            public Location Location { get; set; }
            public string Email { get; set; }
            public Dob Dob { get; set; }
            public string Phone { get; set; }
            public Picture Picture { get; set; }
        }
    
        public class Location
        {
            public string City { get; set; }
            public string Country { get; set; }
        }

        public class Dob
        {
            public DateTime Date { get; set; }
            public int Age { get; set; }
        }

        public class Picture
        {
            public string Large { get; set; }
            public string Medium { get; set; }
            public string Thumbnail { get; set; }
        }
    }
}