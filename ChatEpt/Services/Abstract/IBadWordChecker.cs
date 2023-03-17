namespace ChatEpt.Services.Abstract;

public interface IBadWordChecker
{
    /// <summary>
    /// Checks if <param name="text" /> has bad words
    /// </summary>
    bool HasBadWordInText(string text);
}