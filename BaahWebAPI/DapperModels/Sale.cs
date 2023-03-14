using System;
using System.Collections.Generic;

namespace BaahWebAPI.Models
{
    public partial class Sale
    {
        public string Date { get; set; }
        public int ItemsSold { get; set; }
        public double TotalSale { get; set; }
        public string Status { get; set; }
    }
}
