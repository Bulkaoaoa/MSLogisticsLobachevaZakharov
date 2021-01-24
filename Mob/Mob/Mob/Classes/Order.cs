using System;
using System.Collections.Generic;
using System.Text;

namespace Mob.Classes
{
    public class Order
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Code { get; set; }
        public string StatusName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int StartLocation { get; set; }
        public string StartLocationName { get; set; }
        public int EndLocation { get; set; }
        public string EndLocationName { get; set; }
        public List<Rule> RulesOfStartLocation { get; set; }
        public List<Rule> RulesOfEndLocation { get; set; }
        public Nullable<System.DateTime> DateOfDelivery { get; set; }
        public Nullable<System.TimeSpan> TimeOfDelivery { get; set; }
        public string DateTimeOfDeliveryForCourie { get; set; }
        public string DateTimeOfDeliveryForClient { get; set; }
        public Nullable<int> CourierId { get; set; }
        public string CourierName { get; set; }
        public Nullable<decimal> CourierRate { get; set; }
        public Nullable<decimal> ClientRate { get; set; }
        public int ManagerId { get; set; }
        public int OrderTypeId { get; set; }
        public decimal OrderPrice { get; set; }
        public int StatusId { get; set; }
    }
}
