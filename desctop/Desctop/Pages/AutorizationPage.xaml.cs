using Desctop.Classes;
using Desctop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desctop.Pages
{
    /// <summary>
    /// Interaction logic for AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        public AutorizationPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userList = AppData.Context.User.ToList();
            User user = userList.FirstOrDefault(p => p.Login == TbLogin.Text && p.Password == PbPassword.Password);
            if (user != null)
            {
                switch (user.RoleId)
                {
                    case 1:
                        break;
                    case 2:
                        AppData.MainFrame.Navigate(new ManagerPage());
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
