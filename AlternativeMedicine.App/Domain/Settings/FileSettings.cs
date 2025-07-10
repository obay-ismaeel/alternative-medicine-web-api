namespace AlternativeMedicine.App.Domain.Settings;

public static class FileSettings
{
    public const string ImagesPath = "assets/images"; // use forward slashes for Linux compatibility
    public const string DefaultImageFile = "no-image.jpg";
    public static string DefaultImagePath => $"{ImagesPath}/{DefaultImageFile}";

    public const string AllowedExtensions = ".jpg,.png,.jpeg,.pdf";
    public const int MaxFileSizeInMB = 5;
    public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
}