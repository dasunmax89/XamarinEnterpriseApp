using System;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters
{
    public class ReportStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ReportStatus) || t == typeof(ReportStatus?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Ingepland":
                    return ReportStatus.Ingepland;
                case "Nieuw":
                    return ReportStatus.Nieuw;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ReportStatus)untypedValue;
            switch (value)
            {
                case ReportStatus.Ingepland:
                    serializer.Serialize(writer, "Ingepland");
                    return;
                case ReportStatus.Nieuw:
                    serializer.Serialize(writer, "Nieuw");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly ReportStatusConverter Singleton = new ReportStatusConverter();
    }
}
