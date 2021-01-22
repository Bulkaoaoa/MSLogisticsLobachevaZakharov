using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponseUser
    {
        public ResponseUser(User user, bool IsCourier)
        {
            if (IsCourier == false)
            {
                Id = user.Id;
                Login = user.Login;
                Password = user.Password;
                RoleId = user.RoleId;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Patronymic = user.Patronymic;
                Photo = user.Photo;
                Telephone = user.Telephone;
            }
            else
            {
                Id = user.Id;
                Login = user.Login;
                Password = user.Password;
                RoleId = user.RoleId;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Patronymic = user.Patronymic;
                Photo = user.Photo;
                Telephone = user.Telephone;
                //Это может ломаться, надо будет тестить
                IsFree = user.Courier.IsFree;
                ScheduleCourier = new ResponseCourier(user.Courier, true);
            }

        }

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
        public ResponseCourier ScheduleCourier { get; set; }
    }
}