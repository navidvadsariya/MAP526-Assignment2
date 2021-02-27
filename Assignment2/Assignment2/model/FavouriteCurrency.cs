using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2.model
{
    public class FavouriteCurrency
    { public string toCurrency { get; set; }

    public string rate { get; set; }


    public FavouriteCurrency(string toCurrency, string rate)
    {
        this.toCurrency = toCurrency;
        this.rate = rate;
    }

    }
}