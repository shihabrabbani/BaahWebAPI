using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController:ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public StockController(ILogger<StockController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<Stock> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "SELECT a.CategoryName, SUM(stock_quantity) AS StockQty FROM		(SELECT 	DISTINCT wp_c84s672ma8_wc_product_meta_lookup.product_id,	View_OrderDetail.CategoryId,	View_OrderDetail.CategoryName,	wp_c84s672ma8_wc_product_meta_lookup.stock_quantity,	wp_c84s672ma8_wc_product_meta_lookup.stock_status		FROM wp_c84s672ma8_wc_product_meta_lookup INNER JOIN	View_OrderDetail ON wp_c84s672ma8_wc_product_meta_lookup.product_id = View_OrderDetail.ProductId) a		GROUP BY a.CategoryName		ORDER BY StockQty";
            var list = dapper.Con().Query<Stock>(query).ToList();
            foreach (var cat in list)
            {
                cat.CategoryName = cat.CategoryName.Replace("amp;", "");
            }

            return list;
        }


        //[HttpGet]
        //public IEnumerable<ProductStock> Get()
        //{
        //    string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        //    string tDate = DateTime.Now.ToString("yyyy-MM-dd");

        //    string query = "select @a:=@a+1 Serial,product_id as ProductId, (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) as ProductName, stock_quantity as StockQty, stock_status as StockStatus from baahstore.wp_c84s672ma8_wc_product_meta_lookup, (SELECT @a:= 0) AS a where (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) is not null order by ProductName";
        //    var list = dapper.Con().Query<Stock>(query).ToList();

        //    return list;
        //}

    }
}
