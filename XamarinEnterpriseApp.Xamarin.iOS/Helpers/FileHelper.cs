using System;
using System.IO;
using CoreGraphics;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.iOS.Helpers;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace XamarinEnterpriseApp.Xamarin.iOS.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }

        public static UIImage RotateImage(UIImage rotatedImage, UIImageOrientation orient)
        {
            float width = rotatedImage.CGImage.Width;
            float height = rotatedImage.CGImage.Height;
            CGImage imgRef = rotatedImage.CGImage;
            CGAffineTransform transform = CGAffineTransform.MakeIdentity();
            CGRect bounds = new CGRect(0, 0, width, height);
            float scaleRatio = (float)(bounds.Size.Width / width);
            CGSize imageSize = new CGSize(imgRef.Width, imgRef.Height);

            switch (orient)
            {

                case UIImageOrientation.Right:
                    bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
                    transform = CGAffineTransform.MakeTranslation(imageSize.Height, 0.0f);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)(Math.PI / 2.0));
                    break;

                case UIImageOrientation.Up: //EXIF = 1
                    transform = CGAffineTransform.MakeIdentity();
                    break;

                case UIImageOrientation.UpMirrored: //EXIF = 2
                    transform = CGAffineTransform.MakeTranslation(imageSize.Width, 0.0f);
                    transform = CGAffineTransform.Scale(transform, -1.0f, 1.0f);
                    break;

                case UIImageOrientation.Down: //EXIF = 3
                    transform = CGAffineTransform.MakeTranslation(imageSize.Width, imageSize.Height);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)Math.PI);
                    break;

                case UIImageOrientation.DownMirrored: //EXIF = 4
                    transform = CGAffineTransform.MakeTranslation(0.0f, imageSize.Height);
                    transform = CGAffineTransform.Scale(transform, 1.0f, -1.0f);
                    break;

                case UIImageOrientation.LeftMirrored: //EXIF = 5
                    bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
                    transform = CGAffineTransform.MakeTranslation(imageSize.Height, imageSize.Width);
                    transform = CGAffineTransform.Scale(transform, -1.0f, 1.0f);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)(3.0 * Math.PI / 2.0));
                    break;

                case UIImageOrientation.Left: //EXIF = 6
                    bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
                    transform = CGAffineTransform.MakeTranslation(0.0f, imageSize.Width);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)(3.0 * Math.PI / 2.0));
                    break;

                case UIImageOrientation.RightMirrored: //EXIF = 7
                    bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
                    transform = CGAffineTransform.MakeScale(-1.0f, 1.0f);
                    transform = CGAffineTransform.Rotate(transform, (System.nfloat)(Math.PI / 2.0));
                    break;

            }

            UIGraphics.BeginImageContext(bounds.Size);
            CGContext context = UIGraphics.GetCurrentContext();
            if (orient == UIImageOrientation.Right || orient == UIImageOrientation.Left)
            {
                context.ScaleCTM(-scaleRatio, scaleRatio);
                context.TranslateCTM(-height, 0);
            }
            else
            {
                context.ScaleCTM(scaleRatio, -scaleRatio);
                context.TranslateCTM(0, -height);
            }

            context.ConcatCTM(transform);
            context.DrawImage(new CGRect(0, 0, width, height), imgRef);
            UIImage imageCopy = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return imageCopy;
        }
    }
}
