using System;
using System.Collections.Generic;

namespace BaahWebAPI.DapperModels
{
    public partial class NetSale
    {
        public DateTime Date { get; set; }
        public int ItemsSold { get; set; }
        public double TotalSale { get; set; }
        public string? ShippingCost { get; set; }
        public double? NetSaleAmt { get; set; }
    }
}
