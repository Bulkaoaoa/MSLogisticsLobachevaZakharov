using System;
using System.Collections.Generic;
using System.Text;

namespace Mob.Classes
{
    public class Courier
    {
        public int Id { get; set; }
        public bool IsFree { get; set; }
        public Nullable<int> ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public string CourierName { get; set; }
    }
}
