﻿namespace BaahWebAPI.DapperModels
{
    public class Profit
    {
        public int OrderId { get; set; }
        public string Date { get; set; }
        public string ItemsSold { get; set; }
        public string TotalSale { get; set; }
        public string Status { get; set; }
        public string TotalProfit { get; set; }
    }
}