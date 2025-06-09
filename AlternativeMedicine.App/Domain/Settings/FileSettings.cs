namespace AlternativeMedicine.App.Domain.Settings;

public static class FileSettings
{
    public const string WebRootPath = "C:\\Users\\obayh\\OneDrive\\Desktop\\Projects\\AlternativeMedicine.App\\AlternativeMedicine.App\\wwwroot\\";
    public const string ImagesPath = "assets\\images";
    public const string AllowedExtensions = ".jpg,.png,.jpeg,.pdf";
    public const int MaxFileSizeInMB = 5;
    public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
}