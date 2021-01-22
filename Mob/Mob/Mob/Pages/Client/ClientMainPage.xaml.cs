using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mob.Classes;
using Mob.Pages;
using Mob.Pages.Client;

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
        }

        private void ContentPage_Disappearing(object sender, EventArgs e)
        {
            //Application.Current.Properties["UserId"] = 0;
            //Application.Current.SavePropertiesAsync();
        }
    }
}