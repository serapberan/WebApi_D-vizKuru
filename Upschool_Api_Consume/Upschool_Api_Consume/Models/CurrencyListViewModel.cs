﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upschool_Api_Consume.Models
{
    public class CurrencyListViewModel
    {
        public Exchange_Rates[] exchange_rates { get; set; }
        public string base_currency { get; set; }
        public string base_currency_date { get; set; }


        public class Exchange_Rates
        {
            public string exchange_rate_buy { get; set; }
            public string currency { get; set; }
        }
    }
}
