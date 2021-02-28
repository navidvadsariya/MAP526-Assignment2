using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouriteCurrency : ContentPage
    {
        NetworkingManager networkingManager = new NetworkingManager();

        DBManager dbModel = new DBManager();

        ObservableCollection<FavouriteCurrencyModel> favCurrWithPrice = new ObservableCollection<FavouriteCurrencyModel>();

        public FavouriteCurrency()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            reloadList();
            base.OnAppearing();

        }
  
        private async void reloadList()
        {
            isLoading.IsRunning = true;
            setHomeCurrencyBtn.IsVisible = false;
            removeFavCurrencyBtn.IsVisible = false;
            removeAllFavCurrencyBtn.IsVisible = false;
            msg.IsVisible = false;
            fav_list.IsVisible = false;

            ObservableCollection<FavouriteCurrencyListClassDb> favCurrList = await dbModel.getFavouriteCurrencyList();

            if (favCurrList == null || favCurrList.Count == 0)
            {
                msg.Text = "You don't have any favourite currency. Add some currencies from the main page.";
                infoMsg.IsVisible = false;
            }
            else
            {
                string homeCurrency = await dbModel.getHomeCurrency();
                msg.Text = "Home currency: " + homeCurrency;
                favCurrWithPrice = await networkingManager.GetExchangeRateForFavCurr(homeCurrency, favCurrList);
                //favCurrWithPrice.Remove(favCurrWithPrice.Where(i => i.currency == homeCurrency).All);
                foreach (var item in favCurrWithPrice)
                {
                    if (item.currency == homeCurrency)
                    {
                        favCurrWithPrice.Remove(item);
                        break;
                    }
                }
                fav_list.ItemsSource = favCurrWithPrice;
                
                infoMsg.IsVisible = true;
                fav_list.IsVisible = true;
                setHomeCurrencyBtn.IsVisible = true;
                removeFavCurrencyBtn.IsVisible = true;
                removeAllFavCurrencyBtn.IsVisible = true;
                infoMsg.Text = "If api does not support conversion from home currency to the currency in the list hypen(-) will be displayed.";
            }
            isLoading.IsRunning = false;
            msg.IsVisible = true;
        }

        private async void setHomeCurrency(object sender, EventArgs e)
        {
            if (fav_list != null && fav_list.SelectedItem != null)
            {
                FavouriteCurrencyModel currClass = (FavouriteCurrencyModel) fav_list.SelectedItem;
                await dbModel.changeHomeCurrency(currClass.currency);
                await DisplayAlert("Alert", "Home currency set to " + currClass.currency, "OK");
                reloadList();
            }
            else
            {
                await DisplayAlert("Alert", "To change home currency select an item from the list", "OK");
            }
        }

        private async void removeFavCurrency(object sender, EventArgs e)
        {
            if (fav_list != null && fav_list.SelectedItem != null)
            {
                FavouriteCurrencyModel currClass = (FavouriteCurrencyModel) fav_list.SelectedItem;
                await dbModel.deleteFavouriteCurrency(currClass.currency);
                string alertMsg = currClass.currency + " removed from favourite list";
                await DisplayAlert("Alert", alertMsg, "OK");
                fav_list.SelectedItem = null; 
                reloadList();
            }
            else
            {
                await DisplayAlert("Alert", "Select an item from the list to remove", "OK");
            }
        }

        private async void removeAllFavCurrency(object sender, EventArgs e)
        {
            await dbModel.deleteAllMyFavouriteCurrency();
            string alertMsg = "All currencies removed from favourite list";
            await DisplayAlert("Alert", alertMsg, "OK");
            reloadList();
        }
    }

}