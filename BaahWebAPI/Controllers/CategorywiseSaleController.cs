using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

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
    }
}
