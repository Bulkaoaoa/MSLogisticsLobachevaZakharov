﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = await client.GetStringAsync($"http://mslogisticslz.somee.com/api/OrdersByUser?clientId={AppData.CurrUser.Id}");
            if (task.Length != 0)
            {
                var listOfMyOrders = JsonConvert.DeserializeObject<List<Order>>(task);
                if (listOfMyOrders.Count == 0)
                {
                    //TODO: Надо будет сделать картинку для пустого списка заказов
                }
                else
                {
                    var groupsOfOrders = listOfMyOrders.ToList().Where(p => p.DateOfDelivery.HasValue == true && p.DateOfDelivery.Value.Date >= DateTime.Now.Date || p.DateOfDelivery.HasValue == false).GroupBy(p => p.StatusName).Select(p => new Grouping<string, Order>(p.Key, p));
                    LvMyOrders.ItemsSource = new ObservableCollection<Grouping<string, Order>>(groupsOfOrders);
                }
            }
            else
                Toast.MakeText(Android.App.Application.Context, "Произошла ошибка подключения, перезапустите приложение", ToastLength.Long);

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
    }
}