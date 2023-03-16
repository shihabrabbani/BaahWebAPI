using System;
using System.Collections.Generic;

namespace BaahWebAPI.DapperModels
{
    public partial class Return
    {
        public int OrderId { get; set; }
        public string Date { get; set; }
        public int ItemsSold { get; set; }
        public double TotalSale { get; set; }
        public string Status { get; set; }
    }
}
