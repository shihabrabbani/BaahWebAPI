using System;
using System.Collections.Generic;

namespace BaahWebAPI.DapperModels
{
    public partial class modelHome
    {
        public decimal TotalSaleAmount { get; set; }
        public decimal GrossSale { get; set; }
        public int UniqueShopper { get; set; }
        public decimal TotalUnitSold { get; set; }
        public decimal ReturnRate { get; set; }
        public decimal AOV { get; set; }
        public List<Sale> Trend { get; set; }
        public List<CategorywiseSale> CategorywiseSales { get; set; }
    }

}
