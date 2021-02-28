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

        private readonly string allCurrencieApi = "https://www.cryptonator.com/api/currencies";

        private readonly string fromToApi = "https://api.cryptonator.com/api/ticker/{0}-{1}";

        private readonly HttpClient client = new HttpClient();

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

        public async Task<ObservableCollection<FavouriteCurrencyModel>> GetExchangeRateForFavCurr(string fromCurrency, ObservableCollection<FavouriteCurrencyListClassDb> favCurrencies)
        {
            ObservableCollection<FavouriteCurrencyModel> favCurrWithPrice = new ObservableCollection<FavouriteCurrencyModel>();

            foreach (var toCurrency in favCurrencies)
            {
                var response = await client.GetStringAsync(String.Format(fromToApi, fromCurrency, toCurrency.code));
                var json = JObject.Parse(response);
                var rate = "-";
                if ((bool)json.GetValue("success"))
                {
                    rate = Convert.ToString(JObject.Parse(Convert.ToString(json.GetValue("ticker"))).GetValue("price"));
                }
                favCurrWithPrice.Add(new FavouriteCurrencyModel(toCurrency.code, rate));
            }
            return favCurrWithPrice;
        }

        public async Task<ConvertCurrencyClass> GetExchangeRate(string fromCurrency, string toCurrency)
        {

            ConvertCurrencyClass currencyConversion = new ConvertCurrencyClass();

            var response = await client.GetStringAsync(String.Format(fromToApi, fromCurrency, toCurrency));

            var json = JObject.Parse(response);
            var successRes = json.GetValue("error");
            currencyConversion.error = successRes.ToString();
            if (currencyConversion.error.Length == 0)
            {
                currencyConversion = JsonConvert.DeserializeObject<ConvertCurrencyClass>(Convert.ToString(json.GetValue("ticker")));
            }

            return currencyConversion;
        }
    }
}