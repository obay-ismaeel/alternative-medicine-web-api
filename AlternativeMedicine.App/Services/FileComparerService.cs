namespace AlternativeMedicine.App.Services;

public interface IFileComparerService
{
    bool AreImagesIdentical(byte[] image1Bytes, byte[] image2Bytes);
}

public class FileComparerService : IFileComparerService
{
    public bool AreImagesIdentical(byte[] image1Bytes, byte[] image2Bytes)
    {
        if (image1Bytes.Length != image2Bytes.Length) return false;

        for (int i = 0; i < image1Bytes.Length; i++)
        {
            if (image1Bytes[i] != image2Bytes[i]) return false;
        }

        return true;
    }

    //public bool AreImagesPixelIdentical(Bitmap img1, Bitmap img2)
    //{
    //    // First check image dimensions
    //    if (img1.Width != img2.Width || img1.Height != img2.Height)
    //        return false;

    //    // Lock bits for performance
    //    var rect = new Rectangle(0, 0, img1.Width, img1.Height);
    //    var bmpData1 = img1.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, img1.PixelFormat);
    //    var bmpData2 = img2.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, img2.PixelFormat);

    //    try
    //    {
    //        unsafe
    //        {
    //            byte* ptr1 = (byte*)bmpData1.Scan0;
    //            byte* ptr2 = (byte*)bmpData2.Scan0;
    //            int width = img1.Width;
    //            int height = img1.Height;

    //            for (int y = 0; y < height; y++)
    //            {
    //                for (int x = 0; x < width; x++)
    //                {
    //                    if (*ptr1 != *ptr2)
    //                        return false;

    //                    ptr1++;
    //                    ptr2++;
    //                }
    //            }
    //        }
    //    }
    //    finally
    //    {
    //        img1.UnlockBits(bmpData1);
    //        img2.UnlockBits(bmpData2);
    //    }

    //    return true;
    //}
}
