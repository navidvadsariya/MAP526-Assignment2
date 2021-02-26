using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assignment2.model
{
    public class NetworkingManager
    {
        private string url = "https://www.cryptonator.com/api/currencies";

        private HttpClient client = new HttpClient();

        public NetworkingManager()
        {
        }

        public async Task<CurrencyApi> GetCurrencies()
        {

            var respone = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<CurrencyApi>(respone);


        }





    }
}