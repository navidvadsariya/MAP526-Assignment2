using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvertCurrencyPage : ContentPage
    {
        NetworkingManager networkingManager = new NetworkingManager();
        ConvertCurrencyClass currencyConverted = new ConvertCurrencyClass();
        public ConvertCurrencyPage()
        {
            InitializeComponent();
            Title = "Converter";
        }

        private async void convertClicked(object sender, EventArgs e)
        {
            string fromCurr = fromCurrency.Text;
            string toCurr = toCurrency.Text;
            if(fromCurr != null && toCurr != null && fromCurr.Length > 0 && toCurr.Length > 0)
            {
                var conversionResult = await networkingManager.GetExchangeRate(fromCurr, toCurr);
                currencyConverted = conversionResult;
                this.BindingContext = currencyConverted;
            }
            else
            {
                DisplayAlert("Alert", "Please enter 2 currencies to convert", "OK");
            }
           

        }
    }
}