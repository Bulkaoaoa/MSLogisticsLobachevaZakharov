using Desctop.Classes;
using Desctop.Entities;
using Desctop.Windows;
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

namespace Desctop.Controls
{
    /// <summary>
    /// Interaction logic for NewOrderControl.xaml
    /// </summary>
    public partial class NewOrderControl : UserControl
    {
        public NewOrderControl()
        {
            InitializeComponent();
        }

        private void BtnSelectCourier_Click(object sender, RoutedEventArgs e)
        {
            NotBusyCourierWindow courierWindow = new NotBusyCourierWindow((sender as Button).DataContext as OrderApi);
            courierWindow.Owner = App.Current.MainWindow;
            courierWindow.ShowDialog();
        }

        private void BtnToComplictation_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отправить заказ на комплектацию на скад", "Отправить на комплектацию?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var order = AppData.Context.Order.ToList().FirstOrDefault(p => p.Id == ((sender as Button).DataContext as OrderApi).Id);
                order.StatusId = 2;
                AppData.Context.SaveChangesAsync();
            }
            MainStack.Visibility = Visibility.Collapsed;
        }
    }
}
