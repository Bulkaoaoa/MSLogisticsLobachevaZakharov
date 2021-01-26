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
            Address = location.Address;
            Id = location.Id;
        }

        public int Id { get; set; }
        public string Address { get; set; }
    }
}