using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desctop.Classes
{
    public partial  class OrderApi
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Code { get; set; }
        public object StatusName { get; set; }
        public object TypeName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int StartLocation { get; set; }
        public string StartLocationName { get; set; }
        public int EndLocation { get; set; }
        public string EndLocationName { get; set; }
        public Rulesofstartlocation[] RulesOfStartLocation { get; set; }
        public Rulesofendlocation[] RulesOfEndLocation { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public string TimeOfDelivery { get; set; }
        public string DateTimeOfDeliveryForCourier { get; set; }
        public object CourierId { get; set; }
        public object CourierName { get; set; }
        public object CourierRate { get; set; }
        public object ClientRate { get; set; }
        public int ManagerId { get; set; }
        public int OrderTypeId { get; set; }
        public float OrderPrice { get; set; }
        public int StatusId { get; set; }
    }
    public class Rulesofstartlocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Rulesofendlocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
