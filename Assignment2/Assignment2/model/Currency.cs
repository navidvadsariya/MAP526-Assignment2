using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2.model
{
    public class Currency
    {
        public string code { get; set; }
        public string name { get; set; }
        public IList<string> statuses { get; set; }

        public Currency()
        {
        }
    }
}
