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

namespace Mob.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateOrderPage : ContentPage
    {
        public CreateOrderPage()
        {
            InitializeComponent();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = client.GetStringAsync("http://mslogisticslz.somee.com/api/OrderTypes");
            var listOfOrderTypes = JsonConvert.DeserializeObject<List<OrderType>>(task.Result);
            PickerTypeOfOrder.ItemsSource = listOfOrderTypes.ToList();
        }

        private void SwitchForDateTime_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwitchForDateTime.IsToggled == true)
            {
                //Показываем
                StackWithDateOFDelivery.IsVisible = true;
                StackWithTimeOFDelivery.IsVisible = true;
            }
            else
            {
                //Скрываем
                StackWithDateOFDelivery.IsVisible = false;
                StackWithTimeOFDelivery.IsVisible = false;
            }
        }

        private async void BtnAddOrder_Clicked(object sender, EventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(EntryCode.Text)) errors += "Вы не добавили код \r\n";
            if (string.IsNullOrWhiteSpace(EntryStartLocation.Text)) errors += "Вы не выбрали откуда доставлять\r\n";
            if (string.IsNullOrWhiteSpace(EntryEndLocation.Text)) errors += "Вы не выбрали куда доставлять\r\n";
            if (PickerTypeOfOrder.SelectedItem == null) errors += "Вы не выбрали тип доставки";

            if (errors.Length == 0)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var newStartLocation = new Location()
                    {
                        Address = EntryStartLocation.Text
                    };
                    var startLocTast = client.PostAsync("http://mslogisticslz.somee.com/api/Locations",
                        new StringContent(JsonConvert.SerializeObject(newStartLocation),
                        Encoding.UTF8, "application/json"));

                    newStartLocation = JsonConvert.DeserializeObject<Location>(startLocTast.Result.Content.ReadAsStringAsync().Result);

                    var newEndLocation = new Location()
                    {
                        Address = EntryEndLocation.Text
                    };
                    var endLocTast = client.PostAsync("http://mslogisticslz.somee.com/api/Locations",
                        new StringContent(JsonConvert.SerializeObject(newEndLocation),
                        Encoding.UTF8, "application/json"));

                    newEndLocation = JsonConvert.DeserializeObject<Location>(endLocTast.Result.Content.ReadAsStringAsync().Result);

                    Nullable<TimeSpan> timeSpanOfDelivery;
                    if (TimePDateOfDelivery.Time == DateTime.Now.TimeOfDay)
                        timeSpanOfDelivery = null;
                    else
                        timeSpanOfDelivery = TimePDateOfDelivery.Time;

                    Nullable<DateTime> dateTimeOfDelivery;
                    if (DatePDateOfDelivery.Date == DateTime.Now)
                        dateTimeOfDelivery = null;
                    else
                        dateTimeOfDelivery = DatePDateOfDelivery.Date;

                    var newOrder = new Order()
                    {
                        Code = EntryCode.Text,
                        Comment = EditorComment.Text,
                        StatusId = 1,
                        ClientId = AppData.CurrUser.Id,
                        StartLocation = newStartLocation.Id, // тут может ломаться
                        EndLocation = newEndLocation.Id,
                        ManagerId = 6,
                        OrderTypeId = (PickerTypeOfOrder.SelectedItem as OrderType).Id,
                        DateOfDelivery = dateTimeOfDelivery,
                        TimeOfDelivery = timeSpanOfDelivery
                    };
                    var content = JsonConvert.SerializeObject(newOrder);
                    var task = client.PostAsync("http://mslogisticslz.somee.com/api/Orders", new StringContent(content, Encoding.UTF8, "application/json"));
                    await Navigation.PopAsync();
                }
                catch (Exception)
                {
                    Toast.MakeText(Android.App.Application.Context, "К сожалению случилась ошибка работы с базой данных, повторите позже", ToastLength.Long).Show();
                }

            }
            else
                Toast.MakeText(Android.App.Application.Context, errors, ToastLength.Long).Show();
        }
    }
}