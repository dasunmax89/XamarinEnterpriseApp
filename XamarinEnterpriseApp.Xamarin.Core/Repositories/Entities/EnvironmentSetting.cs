using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models.Base;
using SQLite;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class EnvironmentSetting : BaseEntity, ISelectable
    {
        public string EndPointName { get; set; }
        public string EndPointURL { get; set; }
        public string Type { get; set; }
        public bool IsSelected { get; set; }
        public bool RememberCredentials { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public EnvironmentSetting()
        {

        }
    }
}
