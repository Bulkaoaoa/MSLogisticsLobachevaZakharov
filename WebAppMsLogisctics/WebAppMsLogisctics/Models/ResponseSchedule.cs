using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponseSchedule
    {
        public ResponseSchedule(Schedule schedule)
        {
            Monday = schedule.Monday;
            Tuesday = schedule.Tuesday;
            Wednesday = schedule.Wednesday;
            Thursday = schedule.Thursday;
            Friday = schedule.Friday;
            Saturday = schedule.Saturday;
            Sunday = schedule.Sunday;
        }
        public int Id { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}