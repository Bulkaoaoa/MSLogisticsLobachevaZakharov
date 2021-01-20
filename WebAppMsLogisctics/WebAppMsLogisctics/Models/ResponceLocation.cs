using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponceLocation
    {
        public ResponceLocation(Location location, bool IsNeedLocationRules)
        {
            if (IsNeedLocationRules == false)
                LocationName = location.Address;
            else
            {
                LocationName = location.Address;
                ListOfRules = location.Rule.ToList().ConvertAll(p => new ResponseRule(p)).ToList();
            }
        }

        public List<ResponseRule> ListOfRules { get; set; }
        public string LocationName { get; set; }
    }
}