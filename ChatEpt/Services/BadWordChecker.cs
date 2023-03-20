using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class BadWordChecker : IBadWordChecker
{
    public bool HasBadWordInText(string text)
    {
        var filter = new ProfanityFilter.ProfanityFilter();

        var filteredMessage = filter.DetectAllProfanities(text);
    
        if (filteredMessage.Count>0)
        {
            return true;
        }

        return false;
    }
    
}