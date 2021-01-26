using Mob.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mob.Pages.Courier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourierMainPage : ContentPage
    {
        public CourierMainPage()
        {
            InitializeComponent();
            this.BindingContext = AppData.CurrUser;
        }

        private void BtnMyOrders_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Courier.MyOrdersCourierPage());
        }

        private void BtnSearchOrders_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Courier.FreeOrdersForCourierPage());
        }
    }
}