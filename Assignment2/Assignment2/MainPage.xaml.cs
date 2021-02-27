using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.model;
using Xamarin.Forms;

namespace Assignment2
{
    public partial class MainPage : ContentPage
    {
        NetworkingManager networkingManager = new NetworkingManager();
        ObservableCollection<CurrencyClass> currency_list = new ObservableCollection<CurrencyClass>();

        public MainPage()
        {
            InitializeComponent();
            Title = "€rypto";
        }
        protected async override void OnAppearing()
        {
            // load only once when app starts
            if(currency_list.Count == 0)
            {
                postList.ItemsSource = null;

                var list = await networkingManager.GetCurrencies();
                currency_list = list;

                postList.ItemsSource = currency_list;
            }
           
            base.OnAppearing();

        }

        private void addFavouriteButtonClicked(object sender, EventArgs e)
        {

        }

        private async void convertButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConvertCurrencyPage());
        }

        private void CurrencySelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void viewFavButtonClicked(object sender, EventArgs e)
        {

        }

       

        //
        /*
        on click method(){
        
            which item was clicked get code 
        save code in sql table 
        
        }
        */
        ///

    }
}
