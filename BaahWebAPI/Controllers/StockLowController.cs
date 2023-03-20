using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockLowController:ControllerBase
    {
        private readonly ILogger<StockLowController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public StockLowController(ILogger<StockLowController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<StockLow> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "SELECT @a:=@a+1 SERIAL, product_id AS ProductId, (SELECT post_title FROM baahstore.wp_c84s672ma8_posts WHERE ID = product_id) AS ProductName, stock_quantity AS StockQty, stock_status AS StockStatus FROM baahstore.wp_c84s672ma8_wc_product_meta_lookup, (SELECT @a:= 0) AS a WHERE (SELECT post_title FROM baahstore.wp_c84s672ma8_posts WHERE ID = product_id) IS NOT NULL AND stock_quantity > 0 AND stock_quantity < 30 ORDER BY StockQty ASC LIMIT 5;";
            var list = dapper.Con().Query<StockLow>(query).ToList();

            return list;
        }

    }
}
