using System;
using SQLite;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
