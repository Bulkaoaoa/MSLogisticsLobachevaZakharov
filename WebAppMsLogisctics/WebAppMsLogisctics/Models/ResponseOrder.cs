using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponseOrder
    {
        public ResponseOrder(Order order, bool IsLookForUser)
        {
            if (IsLookForUser == true)
            {
                Id = order.Id;
                Comment = order.Comment;
                Code = order.Code;
                CourierName = new ResponseCourier(order.Courier, false).CourierName;
                // TODO:  Надо сделать средний подсчет рейтинга, знаю как делать, но чет хз
                //Вот эти два молодых могут ломаться 
                StatusName = order.OrderStatus.Name;
                OrderPrice = order.OrderType.Price;
            }
            else
            {
                Id = order.Id;
                Comment = order.Comment;
                Code = order.Code;
                ClientName = new ResponseClient(order.Client).FirstName;
                StartLocationName = new ResponceLocation(order.Location, true).LocationName;
                EndLocationName = new ResponceLocation(order.Location1, true).LocationName;
                OrderPrice = order.OrderType.Price;
                RulesOfStartLocation = new ResponceLocation(order.Location, true).ListOfRules;
                RulesOfEndLocation = new ResponceLocation(order.Location1, true).ListOfRules;
            }
        }
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
        public List<ResponseRule> RulesOfStartLocation { get; set; }
        public List<ResponseRule> RulesOfEndLocation { get; set; }

        public Nullable<int> CourierId { get; set; }
        public string CourierName { get; set; }
        public Nullable<decimal> CourierRate { get; set; }
        public Nullable<decimal> ClientRate { get; set; }
        public int ManagerId { get; set; }
        public int OrderTypeId { get; set; }
        public decimal OrderPrice { get; set; }
    }
}