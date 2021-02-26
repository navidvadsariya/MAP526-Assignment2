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
        public IList<Currency> Currency_list;
        public NetworkingManager networkingManager = new NetworkingManager();
        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {

            postList.ItemsSource = null;

            var list = await networkingManager.GetCurrencies();
            Currency_list = list.rows;

            postList.ItemsSource = Currency_list;
            base.OnAppearing();

        }

    }
}
