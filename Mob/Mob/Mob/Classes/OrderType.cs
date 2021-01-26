using System;
using System.Collections.Generic;
using System.Text;

namespace Mob.Classes
{
    public class OrderType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string NamePrice
        {
            get
            {
                return $"{Name} ({Price:N2})";
            }
        }
    }
}
