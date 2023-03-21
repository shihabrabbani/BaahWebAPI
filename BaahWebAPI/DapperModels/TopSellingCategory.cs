using System;
using System.Collections.Generic;

namespace BaahWebAPI.DapperModels
{
    public partial class TopSellingCategory
    { 
        public string CategoryName { get; set; }
        public int ItemsSold { get; set; }
        public double TotalAmount { get; set; }
    }
}
