using BaahWebAPI.Models;

namespace BaahWebAPI.DapperModels
{
    public class CategorywisePerfomance
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ItemsSold { get; set; }  // Updated data type to int
        public decimal TotalAmount { get; set; }  // Updated data type to decimal
        public decimal AverageOrderValue { get; set; }  // Updated data type to decimal
        public List<DatewiseSales> DatewiseSales { get; set; }
        public List<TopSellingProduct> TopSellingProducts { get; set; }
    }
}
