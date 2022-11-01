using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace XamarinEnterpriseApp.Xamarin.Core.Models
{
    public partial class GJFeatureProperties : Dictionary<string, object>
    {
        public string Street
        {
            get
            {
                string street = string.Empty;

                street = this.FirstOrDefault(x => x.Key.ToLower() == "street").Value as string;

                return street;
            }
        }

        public string GetId(CahdMapLayerDef cahdMapLayerDef)
        {
            string id = string.Empty;

            if (ContainsKey(cahdMapLayerDef.Id))
            {
                var idStr = this.FirstOrDefault(x => x.Key == cahdMapLayerDef.Id).Value as string;

                if (idStr != null)
                {
                    id = idStr;
                }
            }

            return id;
        }

        public string GetCaption(CahdMapLayerDef cahdMapLayerDef)
        {
            string id = string.Empty;

            if (ContainsKey(cahdMapLayerDef.DropdownSelect))
            {
                var idStr = this.FirstOrDefault(x => x.Key == cahdMapLayerDef.DropdownSelect).Value as string;

                if (idStr != null)
                {
                    id = idStr;
                }
            }

            return id;
        }
    }
}
