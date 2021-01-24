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
                if (order.Courier != null)
                    CourierName = new ResponseCourier(order.Courier, false).CourierName;
                // TODO:  Надо сделать средний подсчет рейтинга, знаю как делать, но чет хз
                StatusName = order.OrderStatus.Name;
                OrderPrice = order.OrderType.Price;

                DateOfDelivery = order.DateOfDelivery;
                TimeOfDelivery = order.TimeOfDelivery;
            }
            else
            {
                Id = order.Id;
                Comment = order.Comment;
                Code = order.Code;
                ClientName = new ResponseClient(order.Client).FirstName;
                StartLocationName = new ResponceLocation(order.Location).Address;
                EndLocationName = new ResponceLocation(order.Location1).Address;
                OrderPrice = order.OrderType.Price;
                RulesOfStartLocation = order.Rule1.ToList().ConvertAll(p => new ResponseRule(p)).ToList();
                RulesOfEndLocation = order.Rule.ToList().ConvertAll(p => new ResponseRule(p)).ToList();

                DateOfDelivery = order.DateOfDelivery;
                TimeOfDelivery = order.TimeOfDelivery;

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
        public Nullable<System.DateTime> DateOfDelivery { get; set; }
        public Nullable<System.TimeSpan> TimeOfDelivery { get; set; }

        public string DateTimeOfDeliveryForCourier
        {
            get
            {
                if (DateOfDelivery == null && TimeOfDelivery == null)
                    return "Доставить сегодня до конца дня";
                else if (DateOfDelivery == null && TimeOfDelivery != null)
                    return $"Доставить сегодня к {TimeOfDelivery.Value.Hours}:{TimeOfDelivery.Value.Minutes}";
                else if (DateOfDelivery != null && TimeOfDelivery == null)
                    return $"Доставить {DateOfDelivery.Value.Date.ToShortDateString()} в любое время";
                else
                    return $"Доставить {DateOfDelivery.Value.ToShortDateString()} к {TimeOfDelivery.Value.Hours}:{TimeOfDelivery.Value.Minutes}";
            }
        }

        public string DateTimeOfDeliveryForClient
        {
            get
            {
                if (StatusName != "Отменен" || StatusName != "Заказ выполнен")
                {
                    if (DateOfDelivery == null && TimeOfDelivery == null)
                        return $"{CourierName} доставит до конца дня";
                    else if (DateOfDelivery == null && TimeOfDelivery != null)
                        return $"{CourierName} доставит сегодня к {TimeOfDelivery.Value.Hours}:{TimeOfDelivery.Value.Minutes}";
                    else if (DateOfDelivery != null && TimeOfDelivery == null)
                        return $"{CourierName} доставит {DateOfDelivery.Value.Date.ToShortDateString()} в течении дня";
                    else
                        return $"{CourierName} доставит {DateOfDelivery.Value.ToShortDateString()} к {TimeOfDelivery.Value.Hours}:{TimeOfDelivery.Value.Minutes}";
                }
                else if (StatusName == "Отменен")
                    return "Ваш заказ не будет доставлен";
                else
                    return "Ваш заказ был доставлен";
            }
        }
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