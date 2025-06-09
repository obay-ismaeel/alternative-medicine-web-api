using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlternativeMedicine.App.Controllers.Dtos.Generic;

public record Result<T>
{
    public T Data { get; set; }
    public Error Error { get; set; }
    public bool IsSuccessful => Error == null;
    public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
}