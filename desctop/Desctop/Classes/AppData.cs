using Desctop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desctop.Classes
{
    public static class AppData
    {
        public static Frame MainFrame;
        public static LogEntities Context = new LogEntities();
        public static Manager Manager;
    }
}
