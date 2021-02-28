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

        DBManager dbModel = new DBManager();

        ObservableCollection<CurrencyClass> currency_list = new ObservableCollection<CurrencyClass>();

        public MainPage()
        {
            InitializeComponent();
            Title = "€rypto";
        }
        protected async override void OnAppearing()
        {
            dbModel.createTable();
            // load only once when app starts
            if (currency_list.Count == 0)
            {
                isLoading.IsRunning = true;
                postList.ItemsSource = null;

                var list = await networkingManager.GetCurrencies();
                currency_list = list;
                isLoading.IsRunning = false;
                postList.ItemsSource = currency_list;
            }
           
            base.OnAppearing();

        }

        private async void addFavouriteButtonClicked(object sender, EventArgs e)
        {
            if (postList != null && postList.SelectedItem != null)
            {
                CurrencyClass currClass= (CurrencyClass) postList.SelectedItem;
                FavouriteCurrencyListClassDb favCurr = new FavouriteCurrencyListClassDb(currClass.code);
                bool inserted = await dbModel.insertNewToFavouriteCurrency(favCurr);
                var msg = "";
                if (inserted)
                {
                    msg = currClass.name + " added to favorite list";
                }
                else
                {
                    msg = currClass.name + " already exists in favorite list";
                }
                await DisplayAlert("Alert", msg, "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Select an item from the list", "OK");
            }
        }

        private async void convertButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConvertCurrencyPage());
        }

        private async void viewFavButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavouriteCurrency());
        }

    }
}
