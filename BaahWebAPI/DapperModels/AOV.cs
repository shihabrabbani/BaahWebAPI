using System;
using System.Collections.Generic;

namespace BaahWebAPI.DapperModels
{
    public partial class AOV
    { 
        public string Date { get; set; }
        public int ItemsSold { get; set; }
        public double TotalSale { get; set; }
        public double AverageOrderValue { get; set; }
    }
}
