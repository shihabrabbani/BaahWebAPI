using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using BaahWebAPI.Models;
using BaahWebAPI.DapperModels;

namespace BaahWebAPI.Controllers
{
    public class SaleReportController : Controller
    {
        private readonly ILogger<SaleReportController> _logger;
        clsDapper dapper = new clsDapper();

        public SaleReportController(ILogger<SaleReportController> logger)
        {
            _logger = logger;
        }

        #region Sale Report
        public IActionResult DatewiseBar()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<ViewSalesreport>("select cast(Date as date) as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)");


            ViewBag.fDate = null;
            ViewBag.tDate = null;
            //ViewBag.Dates = list.Select(x => x.Date.ToString("dd/MM/yyyy")).ToList();
            ViewBag.Dates = list.Select(x => x.Date).ToList();
            ViewBag.Totals = list.Select(x => x.TotalSale).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult DatewiseBar(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<ViewSalesreport>("select cast(Date as date) as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)");


            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            //ViewBag.Dates = list.Select(x => x.Date.ToString("dd/MM/yyyy")).ToList();
            ViewBag.Dates = list.Select(x => x.Date).ToList();
            ViewBag.Totals = list.Select(x => x.TotalSale).ToList();

            return View();
        }
        #endregion

        #region Category Report
        public IActionResult CategoryBar()
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
        public IActionResult CategoryBar(DateTime FromDate, DateTime ToDate)
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








        public IActionResult DatewiseReturn()
        {
            string fDate = DateTime.Now.AddDays(-365).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', 'Not Refunded') as Status from `wp_c84s672ma8_wc_order_stats` where `wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded' and cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;";
            var list = dapper.Con().Query<DPSale>(query);

            ViewBag.fDate = null;
            ViewBag.tDate = null;
            ViewBag.List = list.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult DatewiseReturn(DateTime FromDate, DateTime ToDate)
        {
            string fDate = FromDate.ToString("yyyy-MM-dd");
            string tDate = ToDate.ToString("yyyy-MM-dd");

            var list = dapper.Con().Query<DPSale>("select `wp_c84s672ma8_wc_order_stats`.`order_id` as `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status`= 'wc-refunded', 'Refunded', 'Not Refunded') as Status from `wp_c84s672ma8_wc_order_stats` where `wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded' and cast(`wp_c84s672ma8_wc_order_stats`.`date_created` as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) order by order_id desc;");



            ViewBag.fDate = fDate;
            ViewBag.tDate = tDate;
            ViewBag.List = list;

            return View();
        }



    }
}