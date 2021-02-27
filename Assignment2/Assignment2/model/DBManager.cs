using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Persistence;
using SQLite;
using Xamarin.Forms;

namespace Assignment2.model
{
    public class DBManager
    {
        private SQLiteAsyncConnection _connection;
        public DBManager() {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        public async Task<ObservableCollection<FavouriteCurrencyListClassDb>> CreateTable()
        {
            await _connection.CreateTableAsync<FavouriteCurrencyListClassDb>();
            var currencyListFromDB = await _connection.Table<FavouriteCurrencyListClassDb>().ToListAsync();
            var allCurrencies = new ObservableCollection<FavouriteCurrencyListClassDb>(currencyListFromDB);
            return allCurrencies;
        }

    }
}
