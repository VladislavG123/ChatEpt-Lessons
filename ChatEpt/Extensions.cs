namespace ChatEpt;

public static class Extensions
{
    public static string Quoted(this string stringValue) => $"\"{stringValue}\"";

    public static bool ContainsAll(this string stringValue, StringComparison comparison, params string[] parameters) 
        => parameters.All(parameter => stringValue.Contains(parameter, comparison));
}