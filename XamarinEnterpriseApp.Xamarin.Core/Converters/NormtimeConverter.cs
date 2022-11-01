using System;
using XamarinEnterpriseApp.Xamarin.Core.Enums;
using Newtonsoft.Json;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters
{
    public class NormtimeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Normtime) || t == typeof(Normtime?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "1 uur":
                    return Normtime.The1Uur;
                case "1 werkdag":
                    return Normtime.The1Werkdag;
                case "10 werkdagen":
                    return Normtime.The10Werkdagen;
                case "14 werkdagen":
                    return Normtime.The14Werkdagen;
                case "2 maanden":
                    return Normtime.The2Maanden;
                case "27 werkdagen":
                    return Normtime.The27Werkdagen;
                case "4 weken":
                    return Normtime.The4Weken;
                case "5 werkdagen":
                    return Normtime.The5Werkdagen;
                case "8 weken":
                    return Normtime.The8Weken;
            }
            throw new Exception("Cannot unmarshal type Normtime");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Normtime)untypedValue;
            switch (value)
            {
                case Normtime.The1Uur:
                    serializer.Serialize(writer, "1 uur");
                    return;
                case Normtime.The1Werkdag:
                    serializer.Serialize(writer, "1 werkdag");
                    return;
                case Normtime.The10Werkdagen:
                    serializer.Serialize(writer, "10 werkdagen");
                    return;
                case Normtime.The14Werkdagen:
                    serializer.Serialize(writer, "14 werkdagen");
                    return;
                case Normtime.The2Maanden:
                    serializer.Serialize(writer, "2 maanden");
                    return;
                case Normtime.The27Werkdagen:
                    serializer.Serialize(writer, "27 werkdagen");
                    return;
                case Normtime.The4Weken:
                    serializer.Serialize(writer, "4 weken");
                    return;
                case Normtime.The5Werkdagen:
                    serializer.Serialize(writer, "5 werkdagen");
                    return;
                case Normtime.The8Weken:
                    serializer.Serialize(writer, "8 weken");
                    return;
            }
            throw new Exception("Cannot marshal type Normtime");
        }

        public static readonly NormtimeConverter Singleton = new NormtimeConverter();
    }
}
