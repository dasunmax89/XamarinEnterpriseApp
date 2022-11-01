using System;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters
{
    public class IthdColorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(IthdColor) || t == typeof(IthdColor?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "255,0,0")
            {
                return IthdColor.The25500;
            }
            throw new Exception("Cannot unmarshal type IthdColor");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (IthdColor)untypedValue;
            if (value == IthdColor.The25500)
            {
                serializer.Serialize(writer, "255,0,0");
                return;
            }
            throw new Exception("Cannot marshal type IthdColor");
        }

        public static readonly IthdColorConverter Singleton = new IthdColorConverter();
    }
}
