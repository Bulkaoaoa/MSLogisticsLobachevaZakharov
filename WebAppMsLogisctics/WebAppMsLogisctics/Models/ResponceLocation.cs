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
            Location = location.Address;
            //TODO Потом надо будет прикрутить правила, возможно будет удобнее 
        }

        public string Location { get; set; }
    }
}