using System;
using System.Collections.Generic;
using System.Linq;
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

        private void BtnAddOrder_Clicked(object sender, EventArgs e)
        {

        }
    }
}