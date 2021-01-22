using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppMsLogisctics.Entites;

namespace WebAppMsLogisctics.Models
{
    public class ResponseClient
    {
        public ResponseClient(Client client)
        {
            FirstName = new ResponseUser(client.User, false).FirstName;
            //Можно будет добавить фотографию (С другой стороны можно просто брать юзера по Id )
        }
        public string FirstName { get; set; }
        public int Id { get; set; }
    }
}