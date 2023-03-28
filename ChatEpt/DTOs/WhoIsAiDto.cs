using System.Text.Json.Serialization;

namespace ChatEpt.DTOs;

public class WhoIsAiDto
{
    [JsonPropertyName("results")]
    public List<Person> People { get; set; }
}

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

