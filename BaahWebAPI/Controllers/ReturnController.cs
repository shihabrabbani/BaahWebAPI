using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReturnController:ControllerBase
    {
        private readonly ILogger<ReturnController> _logger;
        clsDapper dapper = new clsDapper();

        public ReturnController(ILogger<ReturnController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Return> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', 'Not Refunded') as Status from `wp_c84s672ma8_wc_order_stats` where `wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded' and cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;";
            var list = dapper.Con().Query<Return>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<Return> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', 'Not Refunded') as Status from `wp_c84s672ma8_wc_order_stats` where `wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded' and cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;";
            var list = dapper.Con().Query<Return>(query).ToList();

            return list;
        }
    }
}
