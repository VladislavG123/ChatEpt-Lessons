using ChatEpt.DTOs;
using ChatEpt.Services.Abstract;
using ChatEpt.Services.Ai;
using ChatEpt.Services.Ai.Abstract;

namespace ChatEpt.Services;

public class AiMessageRouter : IAiMessageRouter
{
    private readonly IAiService[] _aiServices;

    public AiMessageRouter(StartupAiService startupAiService, IWhoIsAiService whoIsAiService, RandomAiService randomAiService)
    {
        _aiServices = new IAiService[]
        {
            startupAiService,
            whoIsAiService,
            randomAiService
        };
    }

    /// <inheritdoc/>
    public MessageDto GetAnswer(string request)
        => _aiServices
            .First(service => request.ContainsAll(StringComparison.InvariantCultureIgnoreCase, service.KeyWords))
            .GetAnswer(request);
}