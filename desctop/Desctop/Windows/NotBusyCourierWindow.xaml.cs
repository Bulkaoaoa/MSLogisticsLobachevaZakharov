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
using System.Windows.Shapes;

namespace Desctop.Windows
{
    /// <summary>
    /// Interaction logic for NotBusyCourierWindow.xaml
    /// </summary>
    public partial class NotBusyCourierWindow : Window
    {
        Order orderSQL = new Order();
        public NotBusyCourierWindow(OrderApi order)
        {
            InitializeComponent();
            CbCouriers.ItemsSource = AppData.Context.Courier.ToList().Where(p => p.Order.ToList().Where(i => i.OrderStatus.Id == 4 && (i.DateOfDelivery == DateTime.Now.Date && i.TimeOfDelivery == DateTime.Now.TimeOfDay- new TimeSpan(1,0,0))).ToList().Count() == 0).ToList();
            orderSQL = AppData.Context.Order.ToList().FirstOrDefault(p => p.Id == order.Id);
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            orderSQL.StatusId = 4;
            orderSQL.Courier = (CbCouriers.SelectedItem as Courier);
            AppData.Context.SaveChanges();
            Close();
        }
    }
}
