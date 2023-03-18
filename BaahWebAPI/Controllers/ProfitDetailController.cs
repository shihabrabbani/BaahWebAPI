using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfitDetailController:ControllerBase
    {
        private readonly ILogger<ProfitDetailController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public ProfitDetailController(ILogger<ProfitDetailController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<ProfitDetail> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-pending', 'Pending', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-processing', 'Processing', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-completed', 'Completed', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-trash', 'Trash', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-cancelled', 'Cancelled', `wp_c84s672ma8_wc_order_stats`.`status`))))))as Status ,(((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where  post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_order_shipping')) -  (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_wc_cost_of_shipping')) as TotalProfit from `wp_c84s672ma8_wc_order_stats` where cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;";
            var list = dapper.Con().Query<ProfitDetail>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<ProfitDetail> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;
            string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-pending', 'Pending', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-processing', 'Processing', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-completed', 'Completed', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-trash', 'Trash', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-cancelled', 'Cancelled', `wp_c84s672ma8_wc_order_stats`.`status`))))))as Status ,(((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where  post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_order_shipping')) -  (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_wc_cost_of_shipping')) as TotalProfit from `wp_c84s672ma8_wc_order_stats` where cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;";
            var list = dapper.Con().Query<ProfitDetail>(query).ToList();

            return list;
        }
    }
}
