using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using BaahWebAPI.Models;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopSellingProductController:ControllerBase
    {
        private readonly ILogger<TopSellingProductController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public TopSellingProductController(ILogger<TopSellingProductController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<TopSellingProduct> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "Select `wp_c84s672ma8_wc_product_meta_lookup`.`total_sales` AS `TotalSale`,`wp_c84s672ma8_posts`.`post_title` AS `ProductName` from (`wp_c84s672ma8_wc_product_meta_lookup`  join `wp_c84s672ma8_posts` on((`wp_c84s672ma8_wc_product_meta_lookup`.`product_id` = `wp_c84s672ma8_posts`.`ID`))) where `total_sales` > 0 and cast(post_date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by `wp_c84s672ma8_wc_product_meta_lookup`.`total_sales` desc limit 5";
            var list = dapper.Con().Query<TopSellingProduct>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<TopSellingProduct> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;
            string query = "Select `wp_c84s672ma8_wc_product_meta_lookup`.`total_sales` AS `TotalSale`,`wp_c84s672ma8_posts`.`post_title` AS `ProductName` from (`wp_c84s672ma8_wc_product_meta_lookup`  join `wp_c84s672ma8_posts` on((`wp_c84s672ma8_wc_product_meta_lookup`.`product_id` = `wp_c84s672ma8_posts`.`ID`))) where `total_sales` > 0 and cast(post_date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by `wp_c84s672ma8_wc_product_meta_lookup`.`total_sales` desc limit 5";
            var list = dapper.Con().Query<TopSellingProduct>(query).ToList();

            return list;
        }
    }
}
