using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XamarinEnterpriseApp.Xamarin.Core.Converters.JsonConverters
{
    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                IthdColorConverter.Singleton,
                IthdMarkerConverter.Singleton,
                ReportStatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
