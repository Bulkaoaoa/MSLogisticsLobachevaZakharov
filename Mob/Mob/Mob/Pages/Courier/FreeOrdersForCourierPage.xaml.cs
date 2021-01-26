using Android.Widget;
using Mob.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mob.Pages.Courier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FreeOrdersForCourierPage : ContentPage
    {
        public FreeOrdersForCourierPage()
        {
            InitializeComponent();
            try
            {
                Device.StartTimer(new TimeSpan(0, 0, 30), () =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            UpdateLv();
                        });
                        return true;
                    });
            }
            catch
            {
                Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long).Show();

            }
            UpdateLv();
        }

        private async void UpdateLv()
        {
            LvMyOrders.IsRefreshing = true;
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var responseFreeOrders = await client.GetStringAsync($"http://mslogisticslz.somee.com/api/OrdersWithoutCourier");
                var listOfreeOrders = JsonConvert.DeserializeObject<List<Order>>(responseFreeOrders);
                // может падать из-за пустых полей, проверять на HasValue?
                LvMyOrders.ItemsSource = listOfreeOrders.ToList().Where(p=>p.StatusId != 0).OrderBy(p => p.DateOfDelivery).ToList();
            }
            catch
            {
                Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long).Show();
            }
            LvMyOrders.IsRefreshing = false;
        }

        private void LvMyOrders_Refreshing(object sender, EventArgs e)
        {
            UpdateLv();
        }

        private async void SwipeItemAccomplished_Invoked(object sender, EventArgs e)
        {
            var currItem = (sender as SwipeItem).BindingContext as Order;

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var currOrderResponse = await client.GetStringAsync($"http://mslogisticslz.somee.com/api/Orders/{currItem.Id}");
                var currOrder = JsonConvert.DeserializeObject<Order>(currOrderResponse);

                currOrder.StatusId = 4;
                currOrder.CourierId = AppData.CurrUser.Id;
                var task = await client.PutAsync($"http://mslogisticslz.somee.com/api/Orders/{currOrder.Id}",
                    new StringContent(JsonConvert.SerializeObject(currOrder), Encoding.UTF8, "application/json"));
            }
            catch
            {
                Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long).Show();
            }

            UpdateLv();
        }
    }
}