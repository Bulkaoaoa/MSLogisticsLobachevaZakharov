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
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = client.GetStringAsync($"http://mslogisticslz.somee.com/api/Users?login={EntryLogin.Text}&password={EntryPassword.Text}").Result;
            //try
            //{
            //    task.Wait();
            //    if (task.Status == TaskStatus.RanToCompletion)
            //    { 
            if (SwitchRememberMe.IsToggled == true)//Если запомнить меня
            {
                //Application.Current.Properties[""]
            }
                    Toast.MakeText(Android.App.Application.Context, "Все ок, мы нашли", ToastLength.Short).Show();
                    AppData.CurrUser = JsonConvert.DeserializeObject<User>(task);
                    Navigation.PushAsync(new Client.ClientMainPage());
            //    }    
            //    else if (task.Status == TaskStatus.Faulted)
            //        Toast.MakeText(Android.App.Application.Context, $"Все не ок, error: {task.Exception.Message}", ToastLength.Short).Show();
            //    else if (task.Status == TaskStatus.Canceled)
            //        Toast.MakeText(Android.App.Application.Context, $"Все не ок, операция отменилась", ToastLength.Short).Show();
            //}
            //catch (Exception)
            //{
            //    Toast.MakeText(Android.App.Application.Context, $"Все не ок, error: {task.Exception.Message}", ToastLength.Short).Show();
            //}




        }

        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            Toast.MakeText(Android.App.Application.Context, "Регистрация", ToastLength.Long).Show();
        }

        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            await MainFrameForSwipe.ScaleYTo(7, 200);
            await Navigation.PushAsync(new MainPage());
            await MainFrameForSwipe.ScaleYTo(1, 20);

        }
    }
}