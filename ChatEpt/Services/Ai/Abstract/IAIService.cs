using ChatEpt.DTOs;

namespace ChatEpt.Services.Abstract;

/// <summary>
/// Defines the interface for an artificial intelligence service, which can answer questions based on a given request
/// </summary>
public interface IAiService
{
    /// <summary>
    /// Gets the keywords associated with this AI service implementation, which can be used to filter requests and determine if this implementation can handle a given request.
    /// </summary>
    string[] KeyWords { get; }
    
    /// <summary>
    /// Gets an answer from the AI service for the specified request, returning a DTO object containing both the request and the AI's response.
    /// </summary>
    /// <exception cref="BadHttpRequestException">Throws a BadHttpRequestException if the request was unsuccessful, indicating that the AI service was unable to generate a valid response.</exception>
    /// <returns>MessageServiceDto - a DTO object containing both the request and the AI's response.</returns>
    MessageDto GetAnswer(string request);
    
}