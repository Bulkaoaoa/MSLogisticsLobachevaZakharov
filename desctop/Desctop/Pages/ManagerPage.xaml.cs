using Desctop.Classes;
using Desctop.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Desctop.Pages
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        string response = "";
        List<OrderApi> listWithoutCourierOrder = new List<OrderApi>();
        List<OrderApi> listCourierOrder = new List<OrderApi>();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        int _countNewOrder = 0;
        int _countCourierOrder = 0;
        int _countComplectationOrder = 0;
        public ManagerPage()
        {
            InitializeComponent();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TI3.Header = $"Заказы у курьера ({listCourierOrder.Count})";
            if (_countComplectationOrder != listWithoutCourierOrder.Where(p => p.StatusId == 2).Count())
            {
                IcComplectationOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 2).ToList();
            }
            if (_countCourierOrder != listCourierOrder.Count)
            {
                IcCourierOrder.ItemsSource = listCourierOrder;
            }
            if (_countNewOrder < listWithoutCourierOrder.Where(p => p.StatusId == 1).Count())
            {
                BtnUpdateNewOrder.IsEnabled = true;
                BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p=>p.StatusId==1).Count() - _countNewOrder})";
            }
            else if (_countNewOrder > listWithoutCourierOrder.Where(p => p.StatusId == 1).Count())
            {
                IcNewOrder.ItemsSource = listWithoutCourierOrder.Where(p=>p.StatusId==1);
                _countNewOrder = listWithoutCourierOrder.Where(p=>p.StatusId==1).Count();
            }
            else
            {
                BtnUpdateNewOrder.IsEnabled = false;
                BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p => p.StatusId == 1).Count() - _countNewOrder})";
            }
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(Update));
                thread.Start();
                thread.Join();
            }
            catch
            {

            }
        }
        private void Update()
        {
            try
            {
                var client = new WebClient();
                client.Encoding = Encoding.UTF8;
                //await Task.Run(() =>
                //{
                response = client.DownloadString(new Uri("http://mslogisticslz.somee.com/api/OrdersWithoutCourier"));
                listWithoutCourierOrder = JsonConvert.DeserializeObject<List<OrderApi>>(response);
                response = client.DownloadString(new Uri("http://mslogisticslz.somee.com/api/OrderWithCourier"));
                listCourierOrder = JsonConvert.DeserializeObject<List<OrderApi>>(response);
            }
            catch (Exception)
            {
            }
            //});
        }

        private void BtnUpdateNewOrder_Click(object sender, RoutedEventArgs e)
        {
            IcCourierOrder.ItemsSource = listCourierOrder;
            IcNewOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 1);
            IcComplectationOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 2);
            _countNewOrder = listWithoutCourierOrder.Where(p => p.StatusId == 1).Count();
            _countComplectationOrder = listWithoutCourierOrder.Where(p => p.StatusId == 2).Count();
            _countCourierOrder = listCourierOrder.Count;
            BtnUpdateNewOrder.IsEnabled = false;
            BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p => p.StatusId == 1).Count() - _countNewOrder})";
        }
    }
}
