using System;
using XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class ReportFile
    {
        [JsonProperty("Fihd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long FihdId { get; set; }

        [JsonProperty("Fihd_ithd_id")]
        [JsonConverter(typeof(ParseNumberConverter))]
        public long FihdIthdId { get; set; }

        [JsonProperty("Fihd_label")]
        public string FihdLabel { get; set; }

        [JsonProperty("Fihd_type")]
        public string FihdType { get; set; }

        [JsonProperty("Fihd_bytes")]
        public string FihdBytes { get; set; }

        [JsonProperty("Fihd_filename")]
        public string FihdFilename { get; set; }

        [JsonProperty("Fihd_zkn_identification")]
        public string FihdZknIdentification { get; set; }

        [JsonProperty("Fihd_extention_thumb")]
        public string ThumbBytes { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonIgnore]
        public int Identifier { get; set; }

        public ReportFile()
        {

        }

        public GalleryListItemModel ToGalleryListItem()
        {
            var imageFile = new GalleryListItemModel()
            {
                ImageBase64 = FihdBytes,
                FileName = FihdLabel,
                FileType = FihdType,
                Identifier = FihdId > 0 ? FihdId : Identifier,
                FihdId = FihdId,
                FihdIthdId = FihdIthdId,
                Aspect = Aspect.AspectFill
            };

            return imageFile;
        }

        public GalleryListItemModel ToAttachmentListItem()
        {
            string thumbBytes = !string.IsNullOrEmpty(ThumbBytes) ? ThumbBytes : "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAJwSURBVHhe7dhtb5NQFAfwRqOfo9+0n8N9ASNmMtd1faCU0gdgS+2gXXS+8oWLRjft5vqk0B65zSV05UKa0N5c9PyT/yvOwskPuE2W2yafvn7If76+AhE6HOgHhULhCV2NT0QCGNga2P0GXwTxAFS+CGICcEQQF4ATgtgAHBDEB9gzQjYASPeEkB0A0j0giAQwdHRwfISkDmz9gK6+m4gEsG39PKXrpw8CIAACIADrJiIXARAAARCArp8+CIAACIAArJuIXARAAARAALp++iAAAiAAArBuInIRAAEQAAHo+umDAAiAAAjAuonIRYBdAmhlOd88lSFLvbyUntP106csv8zX5FeQpSIAAiAAAtD102cbgPrb12A1FegoJeb1zSpHEpiNKhhqhXmdVaNeBlOrrf6WdX29XAH6Zgt+z+fkt3eVHzff/J+iI+YsKYGaTsZ0GuDh/g46tXi4dvUEft2N6DTAbDqBM73OnA3KDaBVKcLC8+hqYb5/uWbOq8eHMJ/N6FQYghD3ZO9HP+lUGALeKL5hzpNyA7hy+nSlx1kul8wF+0aLTkRjqtXIfFc5pVejsa1OZD4oN4CPQ5uuE412IkfmydJxsTQlMk/OiLgMzo3IfFBuAF3/YCJPezOj2xvmvFaSwXX/0Kkw5LtW/IN0c558FtNxeF4E8Tw38Zzhegi+t3uwWCzoagCT8UPioXZhth8hkO856VCz/JN//dzwXDfx9SflCkDaqhz7r6QJ7wx99ZPImlkveROcsy5cWG1Qi4fMmfWSGQLnnHehWYp/8kG5A4hWBPjfAXq9F8/o+umTRQBJknb3HyEE+GcBcrm/Mf3ZjEUcMc0AAAAASUVORK5CYII=";

            var imageFile = new GalleryListItemModel()
            {
                ImageBase64 = thumbBytes,
                FileName = $"{FihdLabel}{FihdType}",
                FileType = FihdType,
                Identifier = FihdId > 0 ? FihdId : Identifier,
                FihdId = FihdId,
                FihdIthdId = FihdIthdId,
                Tag = this,
            };

            return imageFile;
        }
    }
}