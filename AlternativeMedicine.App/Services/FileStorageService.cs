using AlternativeMedicine.App.Domain.Settings;

namespace AlternativeMedicine.App.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;

    public FileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> StoreAsync(IFormFile file)
    {
        if (file is null || file.Length is 0)
            return null;

        var extension = Path.GetExtension(file.FileName);
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";

        var folderPath = Path.Combine(_env.WebRootPath, FileSettings.ImagesPath);

        // Ensure directory exists
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fullPath = Path.Combine(folderPath, uniqueFileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return web-safe path
        return $"{FileSettings.ImagesPath}/{uniqueFileName}".Replace("\\", "/");
    }

    public void Delete(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) ||
            filePath.Replace("\\", "/") == FileSettings.DefaultImagePath)
            return;

        var fullPath = Path.Combine(_env.WebRootPath, filePath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    public byte[]? GetImageBytes(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return null;

        var fullPath = Path.Combine(_env.WebRootPath, filePath);

        if (!File.Exists(fullPath))
            return null;

        try
        {
            return File.ReadAllBytes(fullPath);
        }
        catch
        {
            return null;
        }
    }
}
