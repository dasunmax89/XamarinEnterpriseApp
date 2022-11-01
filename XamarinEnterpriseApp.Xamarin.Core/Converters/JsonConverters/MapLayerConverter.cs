using System;
using XamarinEnterpriseApp.Xamarin.Core.Helpers;
using XamarinEnterpriseApp.Xamarin.Core.Models;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters
{
    public class MapLayerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(string);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            CahdMapLayerDef returnVal = null;

            if (reader.TokenType != JsonToken.Null)
            {
                var value = serializer.Deserialize<string>(reader);

                try
                {
                    returnVal = JsonConvert.DeserializeObject<CahdMapLayerDef>(value);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("MapLayerConverter-ReadJson", ex);
                }
            }

            return returnVal;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            CahdMapLayerDef obj = (CahdMapLayerDef)value;
            serializer.Serialize(writer, obj);
            return;
        }
    }

}
