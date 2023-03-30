namespace ChatEpt.DTOs;

/// <summary>
/// Data transfer object for IMessageService
/// </summary>
public record MessageDto(string Request, string Answer, bool NeedToSave = true);