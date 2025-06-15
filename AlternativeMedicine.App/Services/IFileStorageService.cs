using AlternativeMedicine.App.Domain.Settings;

namespace AlternativeMedicine.App.Services;

public interface IFileStorageService
{
    public Task<string> StoreAsync(IFormFile file);
    public void Delete(string? filePath);
    byte[]? GetImageBytes(string filePath);
}