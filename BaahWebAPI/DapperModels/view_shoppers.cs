using System;
using System.Collections.Generic;

namespace BaahWebAPI.DapperModels
{
    public class view_shoppers
    { 
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string DateString { get; set; }
        public string Status { get; set; }
        public double OrderAmount { get; set; }
        public string PhoneNumber { get; set; }
        public int ReturnCount { get; set; }
    }
}
