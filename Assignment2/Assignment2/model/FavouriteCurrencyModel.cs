using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2.model
{
    public class FavouriteCurrencyModel
    { 
        public string currency { get; set; }

        public string rate { get; set; }


    public FavouriteCurrencyModel(string currency, string rate)
    {
        this.currency = currency;
        this.rate = rate;
    }

    }
}