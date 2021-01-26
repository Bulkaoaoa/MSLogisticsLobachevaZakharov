using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Android.Widget;
using Mob.Classes;
using Mob.Pages;
using Mob.Pages.Client;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mob.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientMainPage : ContentPage
    {
        public ClientMainPage()
        {
            InitializeComponent();
            this.BindingContext = AppData.CurrUser;

            try
            {
                Device.StartTimer(new TimeSpan(0, 0, 30), () =>
                     {
                         Device.BeginInvokeOnMainThread(() =>
                         {
                             UpdateOrders();
                         });
                         return true;
                     });
            }
            catch 
            {
                Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long).Show();
            }

            UpdateOrders();
        }

        private void ContentPage_Disappearing(object sender, EventArgs e)
        {
            //Application.Current.Properties["UserId"] = 0;
            //Application.Current.SavePropertiesAsync();
        }


        public async void UpdateOrders()
        {
            LvMyOrders.IsRefreshing = true;

            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var task = await client.GetStringAsync($"http://mslogisticslz.somee.com/api/OrdersByUser?clientId={AppData.CurrUser.Id}");
                if (task.Length != 0)
                {
                    var listOfMyOrders = JsonConvert.DeserializeObject<List<Order>>(task);
                    if (listOfMyOrders.Count == 0)
                    {
                        Toast.MakeText(Android.App.Application.Context, "У вас ещё нет ни одного заказа, нажимайте скорее вот эту желтую кнопку справа снизу ;)", ToastLength.Long).Show();
                    }
                    else
                    {
                        var groupsOfOrders = listOfMyOrders.ToList().Where(p => p.DateOfDelivery.HasValue == true && p.DateOfDelivery.Value.Date >= DateTime.Now.Date || p.DateOfDelivery.HasValue == false).GroupBy(p => p.StatusName).Select(p => new Grouping<string, Order>(p.Key, p));
                        LvMyOrders.ItemsSource = new ObservableCollection<Grouping<string, Order>>(groupsOfOrders);
                    }
                }
                else
                    Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long).Show();
            }
            catch 
            {
                Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long).Show();

            }

            LvMyOrders.IsRefreshing = false;
        }
        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Name { get; private set; }
            public Grouping(K name, IEnumerable<T> items)
            {
                Name = name;
                foreach (T item in items)
                {
                    Items.Add(item);
                }
            }
        }

        private void LvMyOrders_Refreshing(object sender, EventArgs e)
        {
            UpdateOrders();
            LvMyOrders.IsRefreshing = false;
        }

        private void BtnAddOrder_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Client.CreateOrderPage());
        }

        private async void LeftSwipeItem_Invoked(object sender, EventArgs e)
        {
            try
            {
                var currItem = (sender as SwipeItem).BindingContext as Order;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var currOrderResponse = await client.GetStringAsync($"http://mslogisticslz.somee.com/api/Orders/{currItem.Id}");
                var currOrder = JsonConvert.DeserializeObject<Order>(currOrderResponse);
                if (currOrder.StatusId == 1 || currOrder.StatusId == 2 || currOrder.StatusId == 3)
                {
                    currOrder.StatusId = 0;
                    var task = await client.PutAsync($"http://mslogisticslz.somee.com/api/Orders/{currOrder.Id}",
                        new StringContent(JsonConvert.SerializeObject(currOrder),
                        Encoding.UTF8, "application/json"));
                    UpdateOrders();
                }
                else
                    Toast.MakeText(Android.App.Application.Context, "Вы не можете отменить эти заказы", ToastLength.Long).Show();
            }
            catch 
            {
                Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long).Show();

            }

        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            UpdateOrders();
        }
    }
}