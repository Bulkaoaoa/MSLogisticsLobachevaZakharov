using Desctop.Classes;
using Desctop.Pages;
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

namespace Desctop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppData.MainFrame = MainFrame;
            try
            {
                Entities.User user = AppData.Context.User.ToList().FirstOrDefault(p => p.Login == Properties.Settings.Default.Login
                               && p.Password == Properties.Settings.Default.Password);
                if (user != null)
                {
                    AppData.Manager = AppData.Context.Manager.ToList().FirstOrDefault(p => p.User.Id == user.Id);
                    AppData.MainFrame.Navigate(new ManagerPage());
                }
                else
                {
                    AppData.MainFrame.Navigate(new AutorizationPage());
                }
            }
            catch (Exception ex)
            {
                AppData.MainFrame.Navigate(new AutorizationPage());
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            var page = AppData.MainFrame.Content as Page;
            if (page.Title == "Авторизация")
            {
                BtnBack.IsEnabled = false;
                BtnBack.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnBack.IsEnabled = true;
                BtnBack.Visibility = Visibility.Visible;
            }
            Title = $"{page.Title}";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Выйти из аккаунта?", "Выйти?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.Login = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
            AppData.MainFrame.Navigate(new AutorizationPage());
        }
    }
}
