using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using System.Text.RegularExpressions;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<modelHome> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public HomeController(ILogger<modelHome> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public modelHome Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");


            modelHome model = new modelHome();
            //string query = "SELECT DATE(`wp_c84s672ma8_wc_order_stats`.`date_created`) AS `Date`, SUM(`wp_c84s672ma8_wc_order_stats`.`num_items_sold`) AS `ItemsSold`, SUM(`wp_c84s672ma8_wc_order_stats`.`total_sales`) AS `TotalSale`, (((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_order_shipping')) - (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_wc_cost_of_shipping')) AS `TotalProfit` FROM `wp_c84s672ma8_wc_order_stats` WHERE CAST(`wp_c84s672ma8_wc_order_stats`.`date_created` AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY DATE(`wp_c84s672ma8_wc_order_stats`.`date_created`) ORDER BY `Date` DESC";
            //var list = dapper.Con().Query<Profit>(query).ToList();




            string query2 = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var sales = dapper.Con().Query<Sale>(query2).ToList();
            //List<Sale> sales = new List<Sale>();
            model.Trend = sales;



            string query3 = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc";
            var catlist = dapper.Con().Query<CategorywiseSale>(query3).ToList();
            model.CategorywiseSales = catlist;

            return model;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public modelHome Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            modelHome model = new modelHome();
            //string query = "SELECT DATE(`wp_c84s672ma8_wc_order_stats`.`date_created`) AS `Date`, SUM(`wp_c84s672ma8_wc_order_stats`.`num_items_sold`) AS `ItemsSold`, SUM(`wp_c84s672ma8_wc_order_stats`.`total_sales`) AS `TotalSale`, (((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_order_shipping')) - (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta WHERE post_id =`wp_c84s672ma8_wc_order_stats`.`order_id` AND meta_key = '_wc_cost_of_shipping')) AS `TotalProfit` FROM `wp_c84s672ma8_wc_order_stats` WHERE CAST(`wp_c84s672ma8_wc_order_stats`.`date_created` AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY DATE(`wp_c84s672ma8_wc_order_stats`.`date_created`) ORDER BY `Date` DESC";
            //var list = dapper.Con().Query<Profit>(query).ToList();

            string query2 = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var sales = dapper.Con().Query<Sale>(query2).ToList();
            //List<Sale> sales = new List<Sale>();
            model.Trend = sales;

            return model;
        }
    }
}
