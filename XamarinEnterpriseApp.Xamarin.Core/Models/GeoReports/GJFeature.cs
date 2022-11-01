using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public partial class GJFeature : MarkerListItem
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("geometry")]
        public GJGeometry Geometry { get; set; }

        [JsonProperty("properties")]
        public GJFeatureProperties Properties { get; set; }

        [JsonIgnore]
        public Coordinates Coordinates { get; set; }

        [JsonIgnore]
        public CahdMapLayerDef CahdMapLayerDef { get; set; }

        [JsonIgnore]
        public long GisiId { get; set; }

        [JsonIgnore]
        public long GisiIthdId { get; set; }

        [JsonIgnore]
        public string FeatureId { get; set; }

        public override string MarkerText
        {
            get
            {
                return GetCaption();
            }
        }

        public override void SetMarkerIcon(bool isSelected)
        {
            if (isSelected)
            {
                MarkerIconSource = "gjpoint_icon_selected.png";
            }
            else
            {
                MarkerIconSource = "gjpoint_icon.png";
            }
        }

        public string GetId()
        {
            string id = string.Empty;

            if (Properties != null)
            {
                id = Properties.GetId(CahdMapLayerDef);
            }

            return id;
        }

        public string GetCaption()
        {
            string caption = string.Empty;

            if (Properties != null)
            {
                caption = Properties.GetCaption(CahdMapLayerDef);
            }

            return caption;
        }

        public List<ListItemModel> GetInfoList()
        {
            List<ListItemModel> descriptionBuider = new List<ListItemModel>();

            CahdMapLayerDef cahdMaplayerDef = CahdMapLayerDef;

            string dataString = JsonConvert.SerializeObject(Properties);

            var dataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataString);

            foreach (var item in dataDictionary)
            {
                if (cahdMaplayerDef.Fields.Contains(item.Key))
                {
                    descriptionBuider.Add(new ListItemModel()
                    {
                        Caption = $"{item.Key}",
                        Header = $"{item.Value}"
                    });
                }
            }

            return descriptionBuider;
        }
    }
}
