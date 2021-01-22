using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponceLocation
    {
        public ResponceLocation(Location location)
        {
                LocationName = location.Address;
        }

        public string LocationName { get; set; }
    }
}