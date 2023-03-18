using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using System.Text.RegularExpressions;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfitController:ControllerBase
    {
        private readonly ILogger<ProfitController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public ProfitController(ILogger<ProfitController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<Profit> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "SELECT DATE(`wp_c84s672ma8_wc_order_stats`.`date_created`) AS `Date`, SUM(`wp_c84s672ma8_wc_order_stats`.`num_items_sold`) AS `ItemsSold`, SUM(`wp_c84s672ma8_wc_order_stats`.`total_sales`) AS `TotalSale`, (((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_order_shipping')) - (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_wc_cost_of_shipping')) AS `TotalProfit` FROM `wp_c84s672ma8_wc_order_stats` WHERE CAST(`wp_c84s672ma8_wc_order_stats`.`date_created` AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY DATE(`wp_c84s672ma8_wc_order_stats`.`date_created`) ORDER BY `Date` DESC";
            var list = dapper.Con().Query<Profit>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<Profit> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;
            string query = "SELECT DATE(`wp_c84s672ma8_wc_order_stats`.`date_created`) AS `Date`, SUM(`wp_c84s672ma8_wc_order_stats`.`num_items_sold`) AS `ItemsSold`, SUM(`wp_c84s672ma8_wc_order_stats`.`total_sales`) AS `TotalSale`, (((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_order_shipping')) - (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_wc_cost_of_shipping')) AS `TotalProfit` FROM `wp_c84s672ma8_wc_order_stats` WHERE CAST(`wp_c84s672ma8_wc_order_stats`.`date_created` AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY DATE(`wp_c84s672ma8_wc_order_stats`.`date_created`) ORDER BY `Date` DESC";
            var list = dapper.Con().Query<Profit>(query).ToList();

            return list;
        }
        //public IActionResult Datewise()
        //{
        //    string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        //    string tDate = DateTime.Now.ToString("yyyy-MM-dd");

        //    //var list = dapper.Con().Query<DpProfit>("select cast(Date as date) as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)");
        //    string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-pending', 'Pending', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-processing', 'Processing', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-completed', 'Completed', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-trash', 'Trash', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-cancelled', 'Cancelled', `wp_c84s672ma8_wc_order_stats`.`status`))))))as Status ,(((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where  post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_order_shipping')) -  (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_wc_cost_of_shipping')) as Profit from `wp_c84s672ma8_wc_order_stats` where cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;";
        //    var list = dapper.Con().Query<DpProfit>(query);


        //    //var dates = list.Select(x => x.Date.ToString("dd/MM/yyyy")).ToList();
        //    //var list = list.Select(x => x.TotalSale).ToList();
        //    //ViewBag.Dates = list.Select(x => x.Date.ToString("dd/MM/yyyy")).ToList();
        //    ViewBag.fDate = null;
        //    ViewBag.tDate = null;
        //    ViewBag.List = list.Where(f=>f.Profit != null).ToList();

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Datewise(DateTime FromDate, DateTime ToDate)
        //{
        //    string fDate = FromDate.ToString("yyyy-MM-dd");
        //    string tDate = ToDate.ToString("yyyy-MM-dd");

        //    var list = dapper.Con().Query<DpProfit>("select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-pending', 'Pending', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-processing', 'Processing', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-completed', 'Completed', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-trash', 'Trash', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-cancelled', 'Cancelled', `wp_c84s672ma8_wc_order_stats`.`status`))))))as Status ,(((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where  post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_order_shipping')) -  (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_wc_cost_of_shipping')) as Profit from `wp_c84s672ma8_wc_order_stats` where cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;");



        //    ViewBag.fDate = fDate;
        //    ViewBag.tDate = tDate;
        //    ViewBag.List = list.Where(f => f.Profit != null).ToList();

        //    return View();
        //}
    }
}
