using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using BaahWebAPI.Models;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategorywiseSaleController:ControllerBase
    {
        private readonly ILogger<CategorywiseSaleController> _logger;
        clsDapper dapper = new clsDapper();

        public CategorywiseSaleController(ILogger<CategorywiseSaleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CategorywiseSale> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc";
            var list = dapper.Con().Query<CategorywiseSale>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<CategorywiseSale> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc";
            var list = dapper.Con().Query<CategorywiseSale>(query).ToList();

            return list;
        }

        [HttpGet("CategorywisePerfomance/{id}&{FromDate}&{ToDate}")]
        public CategorywisePerfomance CategorywisePerfomance(int id, string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc";
            var item = dapper.Con().Query<CategorywisePerfomance>(query).FirstOrDefault();
            item.AverageOrderValue = (Convert.ToDecimal(item.TotalAmount) / Convert.ToInt32(item.ItemsSold)).ToString();


            string query2 = "SELECT CAST(DATE AS DATE) AS DATE, SUM(ProductSoldAmount) AS TotalSale FROM View_OrderDetail  WHERE View_OrderDetail.Status = 'wc-completed' AND  cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) AND CategoryId = '" + id + "' GROUP BY ProductSoldAmount DESC LIMIT 5";
            var datewiseSales = dapper.Con().Query<DatewiseSales>(query2).ToList();
            item.DatewiseSales = datewiseSales;


            string query3 = "SELECT ProductName, ProductSoldAmount AS totalSale FROM View_OrderDetail  WHERE View_OrderDetail.Status = 'wc-completed' AND  cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) AND CategoryId = '" + id + "' ORDER BY ProductSoldAmount DESC LIMIT 5";
            var list = dapper.Con().Query<TopSellingProduct>(query3).ToList();
            item.TopSellingProducts = list;

            return item;
        }
    }
}
