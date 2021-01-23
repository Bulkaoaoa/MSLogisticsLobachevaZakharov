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

namespace Mob.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BtnRegister_Clicked(object sender, EventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(EntryLogin.Text)) errors += "Введите логин/почту \r\n";
            if (string.IsNullOrWhiteSpace(EntryPassword.Text)) errors += "Введите пароль \r\n";
            if (string.IsNullOrWhiteSpace(EntryFirstName.Text)) errors += "Введите имя \r\n";
            if (string.IsNullOrWhiteSpace(EntryLastName.Text)) errors += "Введите фамилию \r\n";

            if (errors.Length == 0)
            {
                User currUser = new User()
                {
                    Login = EntryLogin.Text,
                    Password = EntryPassword.Text,
                    RoleId = 1,
                    FirstName = EntryFirstName.Text,
                    LastName = EntryLastName.Text,
                    Patronymic = EntryPatronymic.Text == "" ? null : EntryPatronymic.Text,
                    Telephone = EntryTelephone.Text == "" ? null : EntryTelephone.Text,
                };

                HttpClient client = new HttpClient();
                var task = client.PostAsync("http://mslogisticslz.somee.com/api/Users", new StringContent(JsonConvert.SerializeObject(currUser),Encoding.UTF8,"application/json"));
                Navigation.PopAsync();
            }
            else
                Toast.MakeText(Android.App.Application.Context, errors, ToastLength.Long).Show();
        }
    }
}