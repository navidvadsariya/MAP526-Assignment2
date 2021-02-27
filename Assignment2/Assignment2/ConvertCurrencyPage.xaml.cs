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
        }

        private async void convertClicked(object sender, EventArgs e)
        {
            string fromCurr = fromCurrency.Text;
            string toCurr = toCurrency.Text;

            var conversionResult = await networkingManager.GetExchangeRate(fromCurr, toCurr);
            currencyConverted = conversionResult;
            if (currencyConverted.success)
            {
                result.Text = currencyConverted.price;
            }
            else
            {
                result.Text = "Pair not found";
            }
        
        }
    }
}