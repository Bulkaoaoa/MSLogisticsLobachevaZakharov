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
            TbLogin.Focus();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (String.IsNullOrEmpty(TbLogin.Text))
                error += "Введите логин\n";
            if (String.IsNullOrEmpty(PbPassword.Password))
                error += "• Введите пароль\n";
            if (!String.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var userList = AppData.Context.User.ToList();
            User user = userList.FirstOrDefault(p => p.Login == TbLogin.Text && p.Password == PbPassword.Password);
            if (user != null)
            {
                switch (user.RoleId)
                {
                    case 1:
                        MessageBox.Show("Для клиента используйте мобильное приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case 2:
                        AppData.MainFrame.Navigate(new ManagerPage());
                        break;
                    case 3:
                        MessageBox.Show("Для курьера используйте мобильное приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Не верный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
