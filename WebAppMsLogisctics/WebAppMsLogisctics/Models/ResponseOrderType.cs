using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponseOrderType
    {
        public ResponseOrderType(OrderType orderType)
        {
            Id = orderType.Id;
            Name = orderType.Name;
            Price = orderType.Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}