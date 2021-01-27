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
        List<OrderApi> listWithoutCourierOrder = new List<OrderApi>();
        List<OrderApi> listCourierOrder = new List<OrderApi>();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        int _countNewOrder = 0;
        int _countCourierOrder = 0;
        int _countComplectationOrder = 0;
        List<OrderType> listOrderTypes = new List<OrderType>();
        bool isFirstUpdate = true;
        public ManagerPage()
        {
            InitializeComponent();
            try
            {
                listOrderTypes = AppData.Context.OrderType.ToList();
                listOrderTypes.Insert(0, new OrderType
                {
                    Id = 0,
                    Name = "Все типы",
                    Price = 0
                });
                CbType.ItemsSource = listOrderTypes;
                CbType.SelectedIndex = 0;
                CbType.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                CbType.IsEnabled = false;
            }
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (CbType.SelectedIndex > 0)
                {
                    TI1.Header = $"Ожидание выполнения ({listWithoutCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name && p.StatusId == 1).Count()})";
                    TI2.Header = $"Отправлены на комплектацию ({listWithoutCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name && p.StatusId == 2).Count()})";
                    TI3.Header = $"Заказы у курьера ({listCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count()})";
                    if (_countComplectationOrder != listWithoutCourierOrder.Where(p => p.StatusId == 2 & p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count())
                    {
                        IcComplectationOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 2 & p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).ToList();
                    }
                    if (_countCourierOrder != listCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count())
                    {
                        IcCourierOrder.ItemsSource = listCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).ToList();
                    }
                    if (_countNewOrder < listWithoutCourierOrder.Where(p => p.StatusId == 1 & p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count())
                    {
                        BtnUpdateNewOrder.IsEnabled = true;
                        BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p => p.StatusId == 1 & p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count() - _countNewOrder})";
                    }
                    else if (_countNewOrder > listWithoutCourierOrder.Where(p => p.StatusId == 1 & p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count())
                    {
                        IcNewOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 1 & p.OrderTypeName == (CbType.SelectedItem as OrderType).Name);
                        _countNewOrder = listWithoutCourierOrder.Where(p => p.StatusId == 1 & p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count();
                    }
                    else
                    {
                        BtnUpdateNewOrder.IsEnabled = false;
                        BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p => p.StatusId == 1).Count() - _countNewOrder})";
                    }
                    if (isFirstUpdate)
                    {
                        CbType.IsEnabled = true;
                        TbxDataLoaded.Visibility = Visibility.Collapsed;
                        BtnUpdateNewOrder_Click(null, null);
                    }
                }
                else
                {
                    TI1.Header = $"Ожидание выполнения ({listWithoutCourierOrder.Where(p => p.StatusId == 1).Count()})";
                    TI2.Header = $"Отправлены на комплектацию ({listWithoutCourierOrder.Where(p => p.StatusId == 2).Count()})";
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
                        BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p => p.StatusId == 1).Count() - _countNewOrder})";
                    }
                    else if (_countNewOrder > listWithoutCourierOrder.Where(p => p.StatusId == 1).Count())
                    {
                        IcNewOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 1);
                        _countNewOrder = listWithoutCourierOrder.Where(p => p.StatusId == 1).Count();
                    }
                    else
                    {
                        BtnUpdateNewOrder.IsEnabled = false;
                        BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p => p.StatusId == 1).Count() - _countNewOrder})";
                    }
                    if (isFirstUpdate)
                    {
                        CbType.IsEnabled = true;
                        TbxDataLoaded.Visibility = Visibility.Collapsed;
                        BtnUpdateNewOrder_Click(null, null);
                    }
                }
            }
            catch
            {

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
                try
                {
                    var response = client.DownloadString(new Uri("http://mslogisticslz.somee.com/api/OrdersWithoutCourier"));
                    listWithoutCourierOrder = JsonConvert.DeserializeObject<List<OrderApi>>(response);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                try
                {
                    var response = client.DownloadString(new Uri("http://mslogisticslz.somee.com/api/OrderWithCourier"));
                    listCourierOrder = JsonConvert.DeserializeObject<List<OrderApi>>(response);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch
            {
            }
        }

        private void BtnUpdateNewOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BtnUpdateNewOrder.IsEnabled = false;
                if (CbType.SelectedIndex > 0)
                {
                    TI1.Header = $"Ожидание выполнения ({listWithoutCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name && p.StatusId == 1).Count()})";
                    TI2.Header = $"Отправлены на комплектацию ({listWithoutCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name && p.StatusId == 2).Count()})";
                    TI3.Header = $"Заказы у курьера ({listCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count()})";
                    _countNewOrder = listWithoutCourierOrder.Where(p => p.StatusId == 1 && p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count();
                    _countComplectationOrder = listWithoutCourierOrder.Where(p => p.StatusId == 2 && p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count();
                    _countCourierOrder = listCourierOrder.Count;
                    IcCourierOrder.ItemsSource = listCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).ToList();
                    IcNewOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 1 && p.OrderTypeName == (CbType.SelectedItem as OrderType).Name);
                    IcComplectationOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 2 && p.OrderTypeName == (CbType.SelectedItem as OrderType).Name);
                    BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p => p.StatusId == 1 && p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count() - _countNewOrder})";
                }
                else
                {
                    TI1.Header = $"Ожидание выполнения ({listWithoutCourierOrder.Where(p => p.StatusId == 1).Count()})";
                    TI2.Header = $"Отправлены на комплектацию ({listWithoutCourierOrder.Where(p => p.StatusId == 2).Count()})";
                    TI3.Header = $"Заказы у курьера ({listCourierOrder.Count})";
                    _countNewOrder = listWithoutCourierOrder.Where(p => p.StatusId == 1).Count();
                    _countComplectationOrder = listWithoutCourierOrder.Where(p => p.StatusId == 2).Count();
                    _countCourierOrder = listCourierOrder.Where(p => p.OrderTypeName == (CbType.SelectedItem as OrderType).Name).Count();
                    IcCourierOrder.ItemsSource = listCourierOrder;
                    IcNewOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 1);
                    IcComplectationOrder.ItemsSource = listWithoutCourierOrder.Where(p => p.StatusId == 2);
                    BtnUpdateNewOrder.Content = $"Новые заказы ({listWithoutCourierOrder.Where(p => p.StatusId == 1).Count() - _countNewOrder})";
                }
            }
            catch
            {

            }
        }

        private void CbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnUpdateNewOrder_Click(null, null);
        }
    }
}
