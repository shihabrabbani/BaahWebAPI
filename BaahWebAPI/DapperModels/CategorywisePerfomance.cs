namespace BaahWebAPI.DapperModels
{
    public class CategorywisePerfomance
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ItemsSold { get; set; }
        public string TotalAmount { get; set; }
        public string AverageOrderValue { get; set; }
        public List<TopSellingProduct> TopSellingProducts { get; set; }
    }
}
