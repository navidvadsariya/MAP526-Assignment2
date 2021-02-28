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

        public async void createTable()
        {
            // create table if it does not exists
            await _connection.CreateTableAsync<FavouriteCurrencyListClassDb>();
        }

        public async Task<ObservableCollection<FavouriteCurrencyListClassDb>> getFavouriteCurrencyList()
        {
            var currencyListFromDB = await _connection.Table<FavouriteCurrencyListClassDb>().ToListAsync();
            var allCurrencies = new ObservableCollection<FavouriteCurrencyListClassDb>(currencyListFromDB);
            return allCurrencies;
        }
        public async Task<bool> insertNewToFavouriteCurrency(FavouriteCurrencyListClassDb favCurr)
        {
            var t = await _connection.QueryAsync<FavouriteCurrencyListClassDb>("Select * from FavouriteCurrencyListClassDb where code ='"+favCurr.code+"'");
            if (t.Count == 0)
            {
                await _connection.InsertAsync(favCurr);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> changeHomeCurrency(string newHomeCurr)
        {
            string queryRemoveCurrentHomeCurr = "UPDATE FavouriteCurrencyListClassDb SET isHomeCurrency=false";
            await _connection.QueryAsync<FavouriteCurrencyListClassDb>(queryRemoveCurrentHomeCurr);
            string querySetNewHomeCurr = "UPDATE FavouriteCurrencyListClassDb SET isHomeCurrency=true WHERE code ='" + newHomeCurr + "'";
            await _connection.QueryAsync<FavouriteCurrencyListClassDb>(querySetNewHomeCurr);
            return true;
        }

        public async Task<string> getHomeCurrency()
        {
            List<FavouriteCurrencyListClassDb> homeCurrList = await _connection.QueryAsync<FavouriteCurrencyListClassDb>("Select * from FavouriteCurrencyListClassDb where isHomeCurrency=true");
            FavouriteCurrencyListClassDb[] curr = homeCurrList.ToArray();
            if (homeCurrList == null || homeCurrList.Count == 0 || curr == null || curr.Length == 0)
            {
                return "CAD";
            }
            else
            {
                return curr[0].code;
            }
        }

        public async Task<bool> deleteFavouriteCurrency(string code)
        {
            string query = "DELETE FROM FavouriteCurrencyListClassDb WHERE code ='" + code + "'";
            await _connection.QueryAsync<FavouriteCurrencyListClassDb>(query);
            return true;
        }

        public async Task<bool> deleteAllMyFavouriteCurrency()
        {
            await _connection.DeleteAllAsync<FavouriteCurrencyListClassDb>();
            return true;
        }
    }
}
