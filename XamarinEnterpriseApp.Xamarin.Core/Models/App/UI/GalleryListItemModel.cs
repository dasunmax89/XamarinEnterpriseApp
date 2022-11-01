using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GalleryListItemModel : ListItemModel
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public long FihdId { get; set; }

        public long FihdIthdId { get; set; }

        public string ImageUrl { get; set; }

        public Aspect Aspect { get; set; }

        ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(() => ImageSource);
            }
        }

        private string imageBase64;
        public string ImageBase64
        {
            get { return imageBase64; }
            set
            {
                imageBase64 = value;
                RaisePropertyChanged(() => ImageBase64);

                try
                {
                    ImageSource = ImageSource.FromStream(
                   () => new MemoryStream(Convert.FromBase64String(imageBase64)));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private string imageLocal;
        public string ImageLocal
        {
            get { return imageLocal; }
            set
            {
                imageLocal = value;
                RaisePropertyChanged(() => ImageLocal);

                try
                {
                    ImageSource = ImageSource.FromFile(imageLocal);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        string _color;
        public string BackgroundColor
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                RaisePropertyChanged(() => BackgroundColor);
            }
        }

        string _titleColor;
        public string TitleColor
        {
            get
            {
                return _titleColor;
            }
            set
            {
                _titleColor = value;
                RaisePropertyChanged(() => TitleColor);
            }
        }

        string _textColor;
        public string TextColor
        {
            get
            {
                return _textColor;
            }
            set
            {
                _textColor = value;
                RaisePropertyChanged(() => TextColor);
            }
        }

        string _imageAcc;
        public string ImageAcc
        {
            get
            {
                return _imageAcc;
            }
            set
            {
                _imageAcc = value;
                RaisePropertyChanged(() => ImageAcc);
            }
        }
    }
}
