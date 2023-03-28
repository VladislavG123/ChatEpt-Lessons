namespace ChatEpt.DTOs;

/// <summary>
/// Data transfer object for IMessageService
/// </summary>
public record MessageServiceDto(string Request, string Answer, bool NeedToSave = true);