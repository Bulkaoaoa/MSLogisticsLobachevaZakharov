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
    /// Interaction logic for ComplectationOrderControl.xaml
    /// </summary>
    public partial class ComplectationOrderControl : UserControl
    {
        public ComplectationOrderControl()
        {
            InitializeComponent();
        }

        private void BtnSelectCourier_Click(object sender, RoutedEventArgs e)
        {
            NotBusyCourierWindow courierWindow = new NotBusyCourierWindow((sender as Button).DataContext as OrderApi);
            courierWindow.Owner = App.Current.MainWindow;
            courierWindow.ShowDialog();
        }
    }
}
