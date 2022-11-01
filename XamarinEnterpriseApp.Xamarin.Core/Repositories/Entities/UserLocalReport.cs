using System;
using XamarinEnterpriseApp.Xamarin.Core.Models;

namespace XamarinEnterpriseApp.Xamarin.Core.Repositories.Entities
{
    public class UserLocalReport : BaseEntity
    {
        public long IthdId { get; set; }

        public long CustId { get; set; }

        public long IthdIsttId { get; set; }

        public long IthdUshdId { get; set; }

        public long IthdArhdId { get; set; }

        public long IthdCahdId { get; set; }

        public string CahdLabel { get; set; }

        public long IthdDehdId { get; set; }

        public long IthdAlhdId { get; set; }

        public string IthdText { get; set; }

        public string IthdLocationTown { get; set; }

        public string IthdLocationDesc { get; set; }

        public string IthdLocationText { get; set; }

        public string Location { get; set; }

        public double Lon { get; set; }

        public double Lat { get; set; }

        public string State { get; set; }

        public string Statetype { get; set; }

        public string FeatureId { get; set; }

        public string DeviceId { get; set; }

        public string DeviceToken { get; set; }

        public string AppVersion { get; set; }

        public string DevicePlatform { get; set; }

        public string IpAddress { get; set; }

        public string Application { get; set; }

        public string Language { get; set; }

        public DateTime DateCreated { get; set; }

        public string IconSource { get; set; }

        public long ReporterCount { get; set; }

        public UserLocalReport()
        {

        }

        public ReportItem GetReportItem()
        {
            ReportItem reportItem = new ReportItem()
            {
                IthdId = IthdId,
                Status = State,
                Statetype = Statetype,
                Category = CahdLabel,
                DateEntry = DateCreated,
                Location = Location,
                Coordinates = new Coordinates(Lat, Lon),
                ReporterCount = ReporterCount,
            };

            return reportItem;
        }
    }
}
