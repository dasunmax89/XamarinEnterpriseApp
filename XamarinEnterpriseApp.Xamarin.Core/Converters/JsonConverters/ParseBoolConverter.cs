using System;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters
{
    public class ParseBoolConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is Boolean)
            {
                return reader.Value;
            }
            else
            {
                var stringValue = (string)reader.Value;

                switch (stringValue.ToLower())
                {
                    case "yes":
                        return true;
                    case "success":
                        return true;
                    case "true":
                        return true;
                    case "y":
                        return true;
                    case "no":
                        return false;
                    case "fail":
                        return false;
                    case "false":
                        return false;
                    case "n":
                        return false;
                    default:
                        return false;
                }
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            bool boolValue = (bool)value;

            string val = boolValue ? "true" : "false";
            writer.WriteValue(val);
        }
    }
}
