using Android.Widget;
using Mob.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mob.Pages.Courier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrdersCourierPage : ContentPage
    {
        public MyOrdersCourierPage()
        {
            InitializeComponent();
            Timer refreshTimer = new Timer();
            refreshTimer.Interval = 30000;
            refreshTimer.Elapsed += RefreshTimer_Elapsed;
            refreshTimer.Start();
            UpdateLV();
        }

        private void RefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateLV();
        }

        private void UpdateLV()
        {
            LvMyOrders.IsRefreshing = true;
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var responseCourierOrders = client.GetStringAsync($"{AppData.CurrUser.Id}");
                var listOfCouriersOrders = JsonConvert.DeserializeObject<List<Order>>(responseCourierOrders.Result);
                // может падать из-за пустых полей, проверять на HasValue?
                LvMyOrders.ItemsSource = listOfCouriersOrders.ToList().OrderByDescending(p => p.DateOfDelivery).ToList();
            }
            catch (Exception)
            {
                Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long);
            }
            LvMyOrders.IsRefreshing = false;


        }
        private void LvMyOrders_Refreshing(object sender, EventArgs e)
        {
            UpdateLV();
        }
    }
}