using System;
using System.Collections.Generic;

namespace BaahWebAPI.Models
{
    public partial class ViewNetSalesreport
    {
        public DateTime Date { get; set; }
        public int ItemsSold { get; set; }
        public double TotalSale { get; set; }
        public string? ShippingCost { get; set; }
        public double? NetSale { get; set; }
    }
}
