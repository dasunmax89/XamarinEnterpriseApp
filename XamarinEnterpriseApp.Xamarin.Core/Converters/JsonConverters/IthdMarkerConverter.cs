using System;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters
{
    public class IthdMarkerConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(IthdMarker) || t == typeof(IthdMarker?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "red")
            {
                return IthdMarker.Red;
            }
            throw new Exception("Cannot unmarshal type IthdMarker");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (IthdMarker)untypedValue;
            if (value == IthdMarker.Red)
            {
                serializer.Serialize(writer, "red");
                return;
            }
            throw new Exception("Cannot marshal type IthdMarker");
        }

        public static readonly IthdMarkerConverter Singleton = new IthdMarkerConverter();
    }
}
