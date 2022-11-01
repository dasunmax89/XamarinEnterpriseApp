using System;
using System.Collections.Generic;
using XamarinEnterpriseApp.Xamarin.Core.Constants;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportFiles
    {
        [JsonProperty("Files")]
        public List<ReportFile> Files { get; set; }

        [JsonIgnore]
        public List<ReportFile> DeletedFiles { get; set; }

        [JsonProperty("Result")]
        public APIResult Result { get; set; }

        public ReportFiles()
        {
            Files = new List<ReportFile>();
            DeletedFiles = new List<ReportFile>();
        }

        public List<GalleryListItemModel> ToGalleryList()
        {
            List<GalleryListItemModel> imageList = new List<GalleryListItemModel>();

            var files = Files.FindAll(x => x.FihdType.Equals(StringConstants.FileExtensionJPG, StringComparison.InvariantCultureIgnoreCase) ||
                        x.FihdType.Equals(StringConstants.FileExtensionJPEG, StringComparison.InvariantCultureIgnoreCase) ||
                        x.FihdType.Equals(StringConstants.FileExtensionGIF, StringComparison.InvariantCultureIgnoreCase) ||
                        x.FihdType.Equals(StringConstants.FileExtensionHeic, StringComparison.InvariantCultureIgnoreCase) ||
                        x.FihdType.Equals(StringConstants.FileExtensionHeif, StringComparison.InvariantCultureIgnoreCase) ||
                        x.FihdType.Equals(StringConstants.FileExtensionPNG, StringComparison.InvariantCultureIgnoreCase));

            foreach (ReportFile item in files)
            {
                GalleryListItemModel imageModel = item.ToGalleryListItem();

                imageList.Add(imageModel);
            }

            return imageList;
        }

        public List<GalleryListItemModel> ToAttachmentList()
        {
            List<GalleryListItemModel> imageList = new List<GalleryListItemModel>();

            var files = Files.FindAll(x => !x.FihdType.Equals(StringConstants.FileExtensionJPG, StringComparison.InvariantCultureIgnoreCase) &&
                             !x.FihdType.Equals(StringConstants.FileExtensionJPEG, StringComparison.InvariantCultureIgnoreCase) &&
                             !x.FihdType.Equals(StringConstants.FileExtensionGIF, StringComparison.InvariantCultureIgnoreCase) &&
                             !x.FihdType.Equals(StringConstants.FileExtensionHeic, StringComparison.InvariantCultureIgnoreCase) &&
                             !x.FihdType.Equals(StringConstants.FileExtensionHeif, StringComparison.InvariantCultureIgnoreCase) &&
                             !x.FihdType.Equals(StringConstants.FileExtensionPNG, StringComparison.InvariantCultureIgnoreCase));

            foreach (ReportFile item in files)
            {
                GalleryListItemModel imageModel = item.ToAttachmentListItem();

                imageList.Add(imageModel);
            }

            return imageList;
        }
    }
}
