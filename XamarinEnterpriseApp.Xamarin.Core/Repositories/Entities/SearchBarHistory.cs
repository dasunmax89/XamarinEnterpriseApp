using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models.Base;
using SQLite;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class SearchBarHistory : BaseEntity, ISelectable
    {
        public string SearchText { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SearchDate { get; set; }
        public string SearchBarType { get; set; }
        public bool IsSelected { get; set; }

        public SearchBarHistory()
        {

        }
    }
}
