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

            string query = "select @a:=@a+1 Serial,product_id as ProductId, (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) as ProductName, stock_quantity as StockQty, stock_status as StockStatus from baahstore.wp_c84s672ma8_wc_product_meta_lookup, (SELECT @a:= 0) AS a where (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) is not null order by ProductName";
            var list = dapper.Con().Query<Stock>(query).ToList();

            return list;
        }

    //    public IActionResult Current()
    //    {
    //        string query = "select @a:=@a+1 Serial,product_id as ProductId, (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) as ProductName, stock_quantity as StockQty, stock_status as StockStatus from baahstore.wp_c84s672ma8_wc_product_meta_lookup, (SELECT @a:= 0) AS a where (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) is not null order by ProductName";
    //        var list = dapper.Con().Query<DpStock>(query);

    //        ViewBag.List = list;

    //        return View();
    //    }

    }
}
