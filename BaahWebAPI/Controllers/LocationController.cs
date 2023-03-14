using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using Newtonsoft.Json;

namespace BaahWebAPI.Controllers
{
    public class LocationController:Controller
    {
        private readonly ILogger<LocationController> _logger;
        clsDapper dapper = new clsDapper();

        public LocationController(ILogger<LocationController> logger)
        {
            _logger = logger;
        }

        public IActionResult Datewise()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            //string query = "Select distinct Location, Count(ItemsSold) as ItemsSold, Sum(TotalSale) TotalSale from LocationWiseSale where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by Location order by Sum(TotalSale) desc";


            string query = "Select distinct Location as label, Sum(ItemsSold) value from LocationWiseSale where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by label order by Sum(TotalSale) desc";
            var list = dapper.Con().Query<Tree>(query);



            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.ListJson = JsonConvert.SerializeObject(list);

            //ViewBag.List = list;

            return View();
        }

        internal class Tree
        {
            public string label { get; set; }
            public string value { get; set; }
        }

        [HttpPost]
        public IActionResult Datewise(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<DpShippingDetail>("Select distinct Location, Count(ItemsSold) as ItemsSold, Sum(TotalSale) TotalSale from LocationWiseSale where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by Location order by Sum(TotalSale) desc");

            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.List = list;

            return View();
        }

        //public IActionResult Datewise()
        //{
        //    string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        //    string tDate = DateTime.Now.ToString("yyyy-MM-dd");

        //    string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-pending', 'Pending', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-processing', 'Processing', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-completed', 'Completed', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-trash', 'Trash', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-cancelled', 'Cancelled', `wp_c84s672ma8_wc_order_stats`.`status`))))))as Status , (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_shipping_city')  as Location from `wp_c84s672ma8_wc_order_stats` where cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc";
        //    var list = dapper.Con().Query<DpShippingDetail>(query);



        //    ViewBag.fDate = null;
        //    ViewBag.tDate = null;
        //    //ViewBag.List = list.Where(f=>f.Location != null).ToList();
        //    ViewBag.List = list;

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Datewise(DateTime FromDate, DateTime ToDate)
        //{
        //    string fDate = FromDate.ToString("yyyy-MM-dd");
        //    string tDate = ToDate.ToString("yyyy-MM-dd");

        //    var list = dapper.Con().Query<DpShippingDetail>("select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-pending', 'Pending', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-processing', 'Processing', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-completed', 'Completed', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-trash', 'Trash', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-cancelled', 'Cancelled', `wp_c84s672ma8_wc_order_stats`.`status`))))))as Status` , (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_shipping_city')  as Location from `wp_c84s672ma8_wc_order_stats` where cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc");



        //    ViewBag.fDate = fDate;
        //    ViewBag.tDate = tDate;
        //    ViewBag.List = list;

        //    return View();
        //}
    }

}
