using ChatEpt.DTOs;
using ChatEpt.Services.Abstract;
using ChatEpt.Services.Ai;

namespace ChatEpt.Services;

public class AiMessageRouter : IAiMessageRouter
{
    private readonly IAiService[] _aiServices;

    public AiMessageRouter(
        StartupAiService startupAiService,
        WhoIsAiService whoIsAiService,
        RandomMessageAiService randomMessageAiService)
    {
        _aiServices = new IAiService[]
        {
            startupAiService,
            whoIsAiService,
            randomMessageAiService
        };
    }

    /// <inheritdoc/>
    public MessageDto GetAnswer(string request)
        => _aiServices
            .First(x => request.ContainsAll(StringComparison.InvariantCultureIgnoreCase, x.KeyWords))
            .GetAnswer(request);
}