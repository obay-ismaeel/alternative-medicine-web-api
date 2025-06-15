using AlternativeMedicine.App.Domain.Settings;

namespace AlternativeMedicine.App.Services;

public class FileStorageService : IFileStorageService { 
    private readonly IWebHostEnvironment _env;

    public FileStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> StoreAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "." + file.FileName.Split('.')[1];

        var filePath = Path.Combine(FileSettings.ImagesPath, uniqueFileName);

        var fullPath = Path.Combine(_env.WebRootPath, filePath);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    public void Delete(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return;

        if (FileSettings.DefaultImagePath == filePath)
            return;

        var fullPath = Path.Combine(_env.WebRootPath, filePath);

        File.Delete(fullPath);
    }

    public byte[]? GetImageBytes(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return null;
        }

        var fullPath = Path.Combine(_env.WebRootPath, filePath);

        if (!File.Exists(fullPath))
        {
            return null;
        }

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
