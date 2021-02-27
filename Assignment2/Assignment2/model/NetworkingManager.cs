using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assignment2.model
{
    public class NetworkingManager
    {
        public ObservableCollection<CurrencyClass> currencyList { get; set; }

        private string allCurrencieApi = "https://www.cryptonator.com/api/currencies";

        private string fromToApi = "https://api.cryptonator.com/api/ticker/{0}-{1}";

        private HttpClient client = new HttpClient();

        public NetworkingManager()
        {
        }

        public async Task<ObservableCollection<CurrencyClass>> GetCurrencies()
        {
            var response = await client.GetStringAsync(allCurrencieApi);
            var json = JObject.Parse(response);
            currencyList = JsonConvert.DeserializeObject<ObservableCollection<CurrencyClass>>(Convert.ToString(json.GetValue("rows")));

            return currencyList;
        }

        //public async Task<List<FavouriteCurrency>> GetExchangeRateForFavCurr(string fromCurrency, List<string> favCurrencies)
        //{
        //    List<FavouriteCurrency> favCurrWithPrice = new List<FavouriteCurrency>();
        //    string url = "https://api.cryptonator.com/api/ticker/";

        //    foreach (var toCurrency in favCurrencies)
        //    {
        //        dynamic json = await GetExchangeRate(fromCurrency, toCurrency);
        //        if ((bool)json.success)
        //        {
        //            dynamic ticker = json.ticker;
        //            favCurrWithPrice.Add(new FavouriteCurrency(Convert.ToString(ticker.target), Convert.ToString(ticker.price)));
        //        }

        //    }
        //    return favCurrWithPrice;
        //}

        public async Task<ConvertCurrencyClass> GetExchangeRate(string fromCurrency, string toCurrency)
        {

            ConvertCurrencyClass currencyConversion = new ConvertCurrencyClass();

            var response = await client.GetStringAsync(String.Format(fromToApi, fromCurrency, toCurrency));

            var json = JObject.Parse(response);
            currencyConversion = JsonConvert.DeserializeObject<ConvertCurrencyClass>(Convert.ToString(json.GetValue("ticker")));

            var successRes = json.GetValue("success");
            currencyConversion.success = (bool)successRes;

            return currencyConversion;
        }





    }
}