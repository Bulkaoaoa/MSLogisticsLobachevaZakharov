using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponseCourier
    {
        public ResponseCourier(Courier courier, bool IsForSchedule)
        {
            if (IsForSchedule == false)
            {
                //TODO: Ну потом что нибудь сделаем, да?
            }
            else
            {
                Id = courier.Id;
                IsFree = courier.IsFree;
                Schedule = new ResponseSchedule(courier.Schedule);
            }
        }
        public int Id { get; set; }
        public bool IsFree { get; set; }
        public Nullable<int> ScheduleId { get; set; }
        public ResponseSchedule Schedule { get; set; }
    }
}