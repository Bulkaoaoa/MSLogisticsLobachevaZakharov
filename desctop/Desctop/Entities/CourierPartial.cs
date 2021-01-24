using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desctop.Entities
{
    public partial class Courier
    {
        public string FIO
        {
            get
            {
                return $"{User.LastName} {User.FirstName} {User.Patronymic}";
            }
        }
    }
}
