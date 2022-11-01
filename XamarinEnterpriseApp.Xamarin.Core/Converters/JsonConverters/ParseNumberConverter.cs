using System;
using System.Globalization;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters
{
    public class ParseNumberConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(long) || objectType == typeof(long?);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long returnVal = 0;

            if (reader.TokenType != JsonToken.Null)
            {
                var value = serializer.Deserialize<string>(reader);

                Int64.TryParse(value, out returnVal);
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

            var longValue = (long)value;
            serializer.Serialize(writer, longValue.ToString());
            return;
        }
    }

    public class ParseDoubleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(double) || objectType == typeof(double?);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            double returnVal = 0;

            if (reader.TokenType != JsonToken.Null)
            {
                var value = serializer.Deserialize<string>(reader);

                double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out returnVal);
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

            var longValue = (double)value;
            var stringVal = longValue.ToString(CultureInfo.InvariantCulture);
            serializer.Serialize(writer, stringVal);
            return;
        }
    }

}
