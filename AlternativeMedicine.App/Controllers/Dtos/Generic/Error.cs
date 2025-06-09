namespace AlternativeMedicine.App.Controllers.Dtos.Generic;

public record Error
{
    public int Code { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }

    public Error(int code, string type, string message)
    {
        Code = code;
        Type = type;
        Message = message;
    }
}