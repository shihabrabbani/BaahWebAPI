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
        clsUtility utility = new clsUtility();
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

            string fDateM = utility.GetDateString(1, "yyyy-MM-dd");
            string tDateM = utility.GetDateString(2, "yyyy-MM-dd");

            string query1 = "SELECT SUM(TotalSale) FROM view_salesreport WHERE CAST(DATE AS DATE) Between Cast('" + fDateM + "' as Date) and Cast('" + tDateM + "' as Date)";
            var TotalSaleAmount = dapper.Con().Query<decimal?>(query1).FirstOrDefault();
            if(TotalSaleAmount != null)
            {
                model.TotalSaleAmount = (decimal)TotalSaleAmount;
            }

            string query2 = "SELECT SUM(NetSale) FROM view_netsalesreport WHERE CAST(`Date` AS DATE) BETWEEN Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)"; 
            var GrossSale = dapper.Con().Query<decimal?>(query2).FirstOrDefault();
            if (GrossSale != null)
            {
                model.GrossSale = (decimal)GrossSale;
            }

            string query3 = "SELECT COUNT(*) FROM unique_shopper";
            var UniqueShopper = dapper.Con().Query<int?>(query3).FirstOrDefault();
            if (UniqueShopper != null)
            {
                model.UniqueShopper = (int)UniqueShopper;
            }

            string query4 = "SELECT SUM(ItemsSold) FROM view_salesreport WHERE CAST(DATE AS DATE) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)";
            var TotalUnitSold = dapper.Con().Query<decimal?>(query4).FirstOrDefault();
            if (TotalUnitSold != null)
            {
                model.TotalUnitSold = (decimal)TotalUnitSold;
            }

            string query5 = "SELECT COUNT(*) FROM (SELECT `wp_c84s672ma8_wc_order_stats`.`order_id` AS `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded', 'Refunded', 'Not Refunded') AS `STATUS` FROM `wp_c84s672ma8_wc_order_stats` WHERE `wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded' AND CAST(`wp_c84s672ma8_wc_order_stats`.`date_created` AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE)) AS subquery";
            var returnedsalecount = dapper.Con().Query<decimal>(query5).FirstOrDefault();

            query5 = "SELECT Count(TotalSale) FROM view_salesreport WHERE CAST(DATE AS DATE) Between Cast('" + fDateM + "' as Date) and Cast('" + tDateM + "' as Date)";
            var totalsalecount = dapper.Con().Query<decimal>(query5).FirstOrDefault();
            if(returnedsalecount>0 &&  totalsalecount > 0)
            {
                model.ReturnRate = ((decimal)returnedsalecount / (decimal)totalsalecount) * 100;
            }




            string query6 = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var sales = dapper.Con().Query<Sale>(query6).ToList();
            model.Trend = sales;



            string query7 = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc";
            var catlist = dapper.Con().Query<CategorywiseSale>(query7).ToList();
            foreach(var cat in catlist)
            {
                cat.CategoryName = cat.CategoryName.Replace("amp;", "");
            }

            model.CategorywiseSales = catlist;

            if (model.TotalUnitSold > 0)
            {
                model.AOV = model.TotalSaleAmount / model.TotalUnitSold;
            }
            return model;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public modelHome Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;


            modelHome model = new modelHome();

            string fDateM = utility.GetDateString(1, "yyyy-MM-dd");
            string tDateM = utility.GetDateString(2, "yyyy-MM-dd");

            string query1 = "SELECT SUM(TotalSale) FROM view_salesreport WHERE CAST(DATE AS DATE) Between Cast('" + fDateM + "' as Date) and Cast('" + tDateM + "' as Date)";
            var TotalSaleAmount = dapper.Con().Query<decimal?>(query1).FirstOrDefault();
            if (TotalSaleAmount != null)
            {
                model.TotalSaleAmount = (decimal)TotalSaleAmount;
            }

            string query2 = "SELECT SUM(NetSale) FROM view_netsalesreport WHERE CAST(`Date` AS DATE) BETWEEN Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)";
            var GrossSale = dapper.Con().Query<decimal?>(query2).FirstOrDefault();
            if (GrossSale != null)
            {
                model.GrossSale = (decimal)GrossSale;
            }

            string query3 = "SELECT COUNT(*) FROM unique_shopper";
            var UniqueShopper = dapper.Con().Query<int?>(query3).FirstOrDefault();
            if (UniqueShopper != null)
            {
                model.UniqueShopper = (int)UniqueShopper;
            }

            string query4 = "SELECT SUM(ItemsSold) FROM view_salesreport WHERE CAST(DATE AS DATE) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)";
            var TotalUnitSold = dapper.Con().Query<decimal?>(query4).FirstOrDefault();
            if (TotalUnitSold != null)
            {
                model.TotalUnitSold = (decimal)TotalUnitSold;
            }

            string query5 = "SELECT COUNT(*) FROM (SELECT `wp_c84s672ma8_wc_order_stats`.`order_id` AS `OrderId`,`wp_c84s672ma8_wc_order_stats`.`date_created` AS `Date`,`wp_c84s672ma8_wc_order_stats`.`num_items_sold` AS `ItemsSold`,`wp_c84s672ma8_wc_order_stats`.`total_sales` AS `TotalSale`,IF(`wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded', 'Refunded', 'Not Refunded') AS `STATUS` FROM `wp_c84s672ma8_wc_order_stats` WHERE `wp_c84s672ma8_wc_order_stats`.`status` = 'wc-refunded' AND CAST(`wp_c84s672ma8_wc_order_stats`.`date_created` AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE)) AS subquery";
            var returnedsalecount = dapper.Con().Query<decimal>(query5).FirstOrDefault();

            query5 = "SELECT Count(TotalSale) FROM view_salesreport WHERE CAST(DATE AS DATE) Between Cast('" + fDateM + "' as Date) and Cast('" + tDateM + "' as Date)";
            var totalsalecount = dapper.Con().Query<decimal>(query5).FirstOrDefault();
            if (returnedsalecount > 0 && totalsalecount > 0)
            {
                model.ReturnRate = ((decimal)returnedsalecount / (decimal)totalsalecount) * 100;
            }




            string query6 = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var sales = dapper.Con().Query<Sale>(query6).ToList();
            model.Trend = sales;



            string query7 = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc";
            var catlist = dapper.Con().Query<CategorywiseSale>(query7).ToList();
            model.CategorywiseSales = catlist;

            return model;
        }
    }
}
