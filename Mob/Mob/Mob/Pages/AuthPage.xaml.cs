using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Mob.Classes;
using Mob.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace Mob.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();
            //int currUserId = 0;
            //try
            //{
            //    currUserId = int.Parse(Application.Current.Properties["UserId"].ToString());
            //}
            //catch (Exception)
            //{
            //    Application.Current.Properties["UserId"] = 0;
            //    Application.Current.SavePropertiesAsync();
            //}
            //if (currUserId != 0)
            //{
            //    HttpClient client = new HttpClient();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //    var task = client.GetStringAsync($"http://mslogisticslz.somee.com/api/Users/{currUserId}");
            //    if (task.IsCompleted == true)
            //    {
            //        var currUser = JsonConvert.DeserializeObject<User>(task.Result);
            //        AppData.CurrUser = currUser;
            //        if (AppData.CurrUser.RoleId == 1)
            //            Navigation.PushAsync(new Client.ClientMainPage());
            //        else  // навигировать на меню курьера
            //            Toast.MakeText(Android.App.Application.Context, "Пажжи", ToastLength.Long);
            //    }
            //    else
            //        Toast.MakeText(Android.App.Application.Context, "Произошла ошибка, пройдите авторизацию ещё раз", ToastLength.Long);
            //}
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var task = client.GetStringAsync($"http://mslogisticslz.somee.com/api/Users?login={EntryLogin.Text}&password={EntryPassword.Text}").Result;
                
                //Toast.MakeText(Android.App.Application.Context, "Все ок, мы нашли", ToastLength.Short).Show();
                AppData.CurrUser = JsonConvert.DeserializeObject<User>(task);
                if (SwitchRememberMe.IsToggled == true)//Если запомнить меня
                {
                    //Application.Current.Properties["UserId"] = AppData.CurrUser.Id;
                    //Application.Current.SavePropertiesAsync();
                }
                if (AppData.CurrUser.RoleId == 1)
                    Navigation.PushAsync(new Client.ClientMainPage());
                else if (AppData.CurrUser.RoleId == 3) // навигировать на меню курьера
                    Navigation.PushAsync(new Courier.CourierMainPage());
                else
                    Toast.MakeText(Android.App.Application.Context, "Для менеджеров мобильное приложение не предусмотрено. Пожалуйста, зайдите с пк версии", ToastLength.Long).Show();
            }
            catch 
            {
                Toast.MakeText(Android.App.Application.Context, "Такого пользователя не существует, пожалуйста, зарегистрируйтесь или свяжитесь с тех. поддержкой", ToastLength.Long).Show();
            }
        }

        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            await MainFrameForSwipe.ScaleYTo(7, 200);
            await Navigation.PushAsync(new RegistrationPage());
            await MainFrameForSwipe.ScaleYTo(1, 20);

        }
    }
}