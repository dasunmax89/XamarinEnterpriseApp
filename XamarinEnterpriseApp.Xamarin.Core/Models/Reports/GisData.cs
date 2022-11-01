using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class GisData
    {
        [JsonProperty("GisItems")]
        public List<GisItem> GisItems { get; set; }

        public GisData()
        {
            GisItems = new List<GisItem>();
        }

        public static GisData FromFeature(GJFeature gJFeature)
        {
            GisData gisData = new GisData();

            string caption = gJFeature.GetCaption() ?? $"{gJFeature.GisiId}";

            gisData.GisItems.Add(new GisItem()
            {
                GisiId = gJFeature.GisiId,
                GisiIthdId = gJFeature.GisiIthdId,
                GisiVendor = gJFeature.CahdMapLayerDef.Vendor,
                GisiObject = gJFeature.FeatureId,
                Caption = caption,
                GisiCoordX = 0,
                GisiCoordY = 0,
                Coordinates = gJFeature.Coordinates,
                State = gJFeature.GisiId > 0 ? "MODIFIED" : "NEW"
            });

            return gisData;
        }
    }
}