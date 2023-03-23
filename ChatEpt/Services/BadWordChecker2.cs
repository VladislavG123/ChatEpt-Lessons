using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class BadWordChecker2 : IBadWordChecker
{
    public bool HasBadWordInText(string text)
    {
        return true;
    }
}