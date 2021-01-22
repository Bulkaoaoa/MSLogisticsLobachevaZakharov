using System;
using System.Collections.Generic;
using System.Text;

namespace Mob.Classes
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public object Patronymic { get; set; }
        public object Photo { get; set; }
        public object Telephone { get; set; }
        public object IsFree { get; set; }
        public object ScheduleCourier { get; set; }

    }


}
