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
        public string Patronymic { get; set; }
        public byte[] Photo { get; set; }
        public string Telephone { get; set; }
        public bool? IsFree { get; set; }
        public Courier ScheduleCourier { get; set; }
        public string  FI {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }

    }


}
