using System;
using System.Threading.Tasks;
using XamarinEnterpriseApp.Xamarin.Core.Models.Base;
using SQLite;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class PersonalInfo : BaseEntity
    {
        public string Initials { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string LivingAddress { get; set; }
        public string HouseNo { get; set; }
        public string HouseNoLetter { get; set; }
        public string HouseNoAdding { get; set; }
        public string Email { get; set; }
        public string StreetName { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public bool ReceiveFeedbackByEmail { get; set; }
        public string FaxNo { get; set; }
        public string BSN { get; set; }

        public PersonalInfo()
        {

        }
    }
}
