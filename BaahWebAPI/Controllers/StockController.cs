using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

namespace BaahWebAPI.Controllers
{
    public class StockController:Controller
    {
        private readonly ILogger<StockController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public StockController(ILogger<StockController> logger)
        {
            _logger = logger;
        }

        public IActionResult Current()
        {
            string query = "select @a:=@a+1 Serial,product_id as ProductId, (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) as ProductName, stock_quantity as StockQty, stock_status as StockStatus from baahstore.wp_c84s672ma8_wc_product_meta_lookup, (SELECT @a:= 0) AS a where (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) is not null order by ProductName";
            var list = dapper.Con().Query<DpStock>(query);

            ViewBag.List = list;

            return View();
        }

    }
}
