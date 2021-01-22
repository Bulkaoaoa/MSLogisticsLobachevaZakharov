using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            Toast.MakeText(Android.App.Application.Context, "Авторизация", ToastLength.Long).Show();
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