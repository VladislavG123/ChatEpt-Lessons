using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class BadWordChecker : IBadWordChecker
{
    private readonly ProfanityFilter.ProfanityFilter _filter;

    public BadWordChecker(ProfanityFilter.ProfanityFilter filter)
    {
        _filter = filter;
    }
    
    public bool HasBadWordInText(string text) => _filter.DetectAllProfanities(text).Count > 0;
}