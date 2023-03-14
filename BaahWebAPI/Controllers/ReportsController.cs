using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using BaahWebAPI.Models;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaahWebAPI.Controllers
{
    public class ReportsController:Controller
    {
        private readonly ILogger<ReportsController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public ReportsController(ILogger<ReportsController> logger)
        {
            _logger = logger;
        }


        #region Sale Report
        public IActionResult SaleReport()
        {
            string fDate = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<ViewSalesreport>("select DATE_FORMAT(`date`,'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(`date` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by `date`");

            var prod = dapper.Con().Query<DpProduct>("Select `wp_c84s672ma8_wc_product_meta_lookup`.`total_sales` AS `TotalSale`,`wp_c84s672ma8_posts`.`post_title` AS `ProductName` from (`wp_c84s672ma8_wc_product_meta_lookup`  join `wp_c84s672ma8_posts` on((`wp_c84s672ma8_wc_product_meta_lookup`.`product_id` = `wp_c84s672ma8_posts`.`ID`))) where `total_sales` > 0 and cast(post_date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by `wp_c84s672ma8_wc_product_meta_lookup`.`total_sales` desc limit 5");

            var cat = dapper.Con().Query<DpCategory>("Select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc limit 3");

            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.Dates = list.Select(x => x.Date).ToList();
            ViewBag.Totals = list.Select(x => x.TotalSale).ToList();
            ViewBag.TopProds = prod;
            ViewBag.TopCats = cat;
            ViewBag.TotalSale = list.Sum(f => f.TotalSale);

            return View();
        }

        [HttpPost]
        public IActionResult SaleReport(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<ViewSalesreport>("select DATE_FORMAT(`date`,'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(`date` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by `date`");

            var prod = dapper.Con().Query<DpProduct>("Select `wp_c84s672ma8_wc_product_meta_lookup`.`total_sales` AS `TotalSale`,`wp_c84s672ma8_posts`.`post_title` AS `ProductName` from (`wp_c84s672ma8_wc_product_meta_lookup`  join `wp_c84s672ma8_posts` on((`wp_c84s672ma8_wc_product_meta_lookup`.`product_id` = `wp_c84s672ma8_posts`.`ID`))) where `total_sales` > 0 and cast(post_date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by `wp_c84s672ma8_wc_product_meta_lookup`.`total_sales` desc limit 5");

            var cat = dapper.Con().Query<DpCategory>("Select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc limit 3");

            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.Dates = list.Select(x => x.Date).ToList();
            ViewBag.Totals = list.Select(x => x.TotalSale).ToList();
            ViewBag.TopProds = prod;
            ViewBag.TopCats = cat;
            ViewBag.TotalSale = list.Sum(f => f.TotalSale);

            return View();
        }
        #endregion

        #region Category Report
        public IActionResult CategoryReport()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<DpCategory>("select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc");


            //ViewBag.fDate = null;
            //ViewBag.tDate = null;
            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.Dates = list.Select(x => x.CategoryName.ToString()).ToList();
            ViewBag.Totals = list.Select(x => x.TotalAmount).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CategoryReport(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<DpCategory>("select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc");


            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.Dates = list.Select(x => x.CategoryName.ToString()).ToList();
            ViewBag.Totals = list.Select(x => x.TotalAmount).ToList();

            return View();
        }

        #endregion

        #region NetSaleReport
        public IActionResult NetSaleReport()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select cast(Date as date) as Date,sum(TotalSale) as TotalSale from view_netsalesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";

            var list = dapper.Con().Query<ViewNetSalesreport>(query);
            //List<ViewNetSalesreport> net = db.ViewNetSalesreports.ToList();
            //List<ViewNetSalesreport> net = db.ViewNetSalesreports.Where(a => a.Date.Date >= DateTime.Now.Date.AddDays(-15) && a.Date.Date <= DateTime.Now.Date).ToList();
            ViewBag.netsalestlist = list;

            return View();
        }
        [HttpPost]
        public IActionResult NetSaleReport(DateTime FromDate, DateTime ToDate)
        {
            //List<ViewNetSalesreport> net = db.ViewNetSalesreports.Where(a => a.Date.Date >= FromDate.Date && a.Date.Date <= ToDate.Date).ToList();

            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<ViewNetSalesreport>("select cast(Date as date) as Date,sum(TotalSale) as TotalSale from view_netsalesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)");


            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.netsalestlist = list;
            //ViewBag.Dates = list.Select(x => x.Date.ToString("dd/MM/yyyy")).ToList();
            //ViewBag.Totals = list.Select(x => x.TotalSale).ToList();
            return View();
        }

        #endregion

        #region ProfitReport
        public IActionResult ProfitReport()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            //var list = dapper.Con().Query<DpProfit>("select cast(Date as date) as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)");
            string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-pending', 'Pending', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-processing', 'Processing', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-completed', 'Completed', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-trash', 'Trash', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-cancelled', 'Cancelled', `wp_c84s672ma8_wc_order_stats`.`status`))))))as Status ,(((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where  post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_order_shipping')) -  (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_wc_cost_of_shipping')) as Profit from `wp_c84s672ma8_wc_order_stats` where cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;";
            var list = dapper.Con().Query<DpProfit>(query);


            //var dates = list.Select(x => x.Date.ToString("dd/MM/yyyy")).ToList();
            //var list = list.Select(x => x.TotalSale).ToList();
            //ViewBag.Dates = list.Select(x => x.Date.ToString("dd/MM/yyyy")).ToList();
            ViewBag.fDate = null;
            ViewBag.tDate = null;
            ViewBag.List = list.Where(f => f.Profit != null).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult ProfitReport(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<DpProfit>("select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-pending', 'Pending', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-processing', 'Processing', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-completed', 'Completed', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-trash', 'Trash', IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-cancelled', 'Cancelled', `wp_c84s672ma8_wc_order_stats`.`status`))))))as Status ,(((SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_alg_wc_cog_order_profit') + (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where  post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_order_shipping')) -  (SELECT meta_value FROM baahstore.wp_c84s672ma8_postmeta where post_id=`wp_c84s672ma8_wc_order_stats`.`order_id` and meta_key = '_wc_cost_of_shipping')) as Profit from `wp_c84s672ma8_wc_order_stats` where cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;");



            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.List = list.Where(f => f.Profit != null).ToList();

            return View();
        }
        #endregion

        #region LocationWiseSaleReport

        public IActionResult LocationWiseSaleReport()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            //string query = "Select distinct Location, Count(ItemsSold) as ItemsSold, Sum(TotalSale) TotalSale from LocationWiseSale where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by Location order by Sum(TotalSale) desc";


            string query = "SELECT distinct districtname as  'name', Sum(ItemsSold) as 'value' FROM baahstore.view_locationwisesale INNER JOIN zDistricts ON view_locationwisesale.Location = zDistricts.districtId where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by districtname order by Sum(ItemsSold) desc";
            var listfull = dapper.Con().Query<DpLocationMin>(query).OrderByDescending(f => f.value);
            var list = listfull.Take(5).OrderByDescending(f => f.value);

            List<string> names = new List<string>();
            foreach (var item in list)
            {
                names.Add(item.name);
            }


            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.LocationList = list;
            ViewBag.LocationNames = names;
            ViewBag.ListFull = listfull;

            return View();
        }

        [HttpPost]
        public IActionResult LocationWiseSaleReport(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");



            string query = "SELECT distinct districtname as  'name', Sum(ItemsSold) as 'value' FROM baahstore.view_locationwisesale INNER JOIN zDistricts ON view_locationwisesale.Location = zDistricts.districtId where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by districtname order by Sum(ItemsSold) desc";
            var listfull = dapper.Con().Query<DpLocationMin>(query).OrderByDescending(f => f.value);
            var list = listfull.Take(5).OrderByDescending(f => f.value);

            List<string> names = new List<string>();
            foreach (var item in list)
            {
                names.Add(item.name);
            }


            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.LocationList = list;
            ViewBag.LocationNames = names;
            ViewBag.ListFull = listfull;

            return View();
        }

        #endregion

        #region Stock
        public IActionResult StockReport()
        {
            string query = "select @a:=@a+1 Serial,product_id as ProductId, (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) as ProductName, stock_quantity as StockQty, stock_status as StockStatus from baahstore.wp_c84s672ma8_wc_product_meta_lookup, (SELECT @a:= 0) AS a where (select post_title from baahstore.wp_c84s672ma8_posts where ID = product_id) is not null order by ProductName";
            var list = dapper.Con().Query<DpStock>(query);

            ViewBag.List = list;

            return View();
        }


        #endregion

        #region Return Report
        public IActionResult ReturnReport()
        {
            string fDate = DateTime.Now.AddDays(-365).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', 'Not Refunded') as Status from `wp_c84s672ma8_wc_order_stats` where `wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded' and cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;";
            var list = dapper.Con().Query<DpSale>(query);

            ViewBag.fDate = null;
            ViewBag.tDate = null;
            ViewBag.List = list.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult ReturnReport(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<DpSale>("select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', 'Not Refunded') as Status from `wp_c84s672ma8_wc_order_stats` where `wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded' and cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;");



            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.List = list;

            return View();
        }


        #endregion

        #region AbandonedCartReport
        public IActionResult AbandonedCartReport()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "SELECT id as Id, (SELECT meta_value FROM baahstore.wp_c84s672ma8_usermeta where user_id=(baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite.user_id) and meta_key = 'billing_email') as Email, user_type as Customer, DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as AbandonedDate FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite where cast(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)";
            var list = dapper.Con().Query<DpAbandonedCart>(query);

            ViewBag.fDate = null;
            ViewBag.tDate = null;
            ViewBag.List = list;

            return View();
        }

        [HttpPost]
        public IActionResult AbandonedCartReport(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<DpAbandonedCart>("SELECT id as Id, (SELECT meta_value FROM baahstore.wp_c84s672ma8_usermeta where user_id=(baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite.user_id) and meta_key = 'billing_email') as Email, user_type as Customer, DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as AbandonedDate FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite where cast(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)");



            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.List = list;

            return View();
        }
        #endregion

    }
}
