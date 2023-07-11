using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using BaahWebAPI.Models;
using System.Diagnostics;
using System.Collections.Generic;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategorywiseSaleController:ControllerBase
    {
        private readonly ILogger<CategorywiseSaleController> _logger;
        clsDapper dapper = new clsDapper();

        public CategorywiseSaleController(ILogger<CategorywiseSaleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CategorywiseSale> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc";
            var list = dapper.Con().Query<CategorywiseSale>(query).ToList();
            foreach (var cat in list)
            {
                cat.CategoryName = cat.CategoryName.Replace("amp;", "");
            }
            list.RemoveAll(cat => cat.CategoryName == "Our Hotties" || cat.CategoryName == "Trending");

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<CategorywiseSale> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CategoryId, CategoryName order by ItemsSold desc";
            var list = dapper.Con().Query<CategorywiseSale>(query).ToList();
            foreach (var cat in list)
            {
                cat.CategoryName = cat.CategoryName.Replace("amp;", "");
            }
            list.RemoveAll(cat => cat.CategoryName == "Our Hotties" || cat.CategoryName == "Trending");

            return list;
        }

        [HttpGet("CategorywisePerfomance/{id}&{FromDate}&{ToDate}")]
        public CategorywisePerfomance CategorywisePerfomance(int id, string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            CategorywisePerfomance item = new CategorywisePerfomance();
            List<DatewiseSales> datewiseSales = new List<DatewiseSales>();
            List<TopSellingProduct> topsellingproducts = new List<TopSellingProduct>();

            string query = "select CategoryId, CategoryName, SUM(ProductSoldQty) as ItemsSold , SUM(ProductSoldAmount) as TotalAmount from View_OrderDetail where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) and categoryid = '" + id + "' group by CategoryId, CategoryName order by ItemsSold desc";
            item = dapper.Con().Query<CategorywisePerfomance>(query).FirstOrDefault();
            if (item != null)
            {
                item.CategoryName = item.CategoryName.Replace("amp;", "");
                item.AverageOrderValue = Convert.ToDecimal(item.TotalAmount) / Convert.ToInt32(item.ItemsSold);

                DateTime from = DateTime.ParseExact(fDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime to = DateTime.ParseExact(tDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                TimeSpan timeDifference = to - from;
                double daysDifference = timeDifference.TotalDays;

                string query2 = "";
                if (daysDifference < 32)
                {
                    //WHERE View_OrderDetail.Status = 'wc-completed' AND
                    query2 = "SELECT CAST(DATE AS DATE) AS DateString, SUM(ProductSoldAmount) AS TotalSale FROM View_OrderDetail WHERE cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) AND CategoryId = '" + id + "' GROUP BY DateString";
                }
                else if (daysDifference > 31 && daysDifference < 366)
                {
                    //WHERE View_OrderDetail.Status = 'wc-completed' AND
                    query2 = "SELECT CONCAT(SUBSTRING(MONTHNAME(`DATE`), 1, 3), '-', RIGHT(YEAR(`DATE`), 2)) AS DateString, SUM(ProductSoldAmount) AS TotalSale FROM View_OrderDetail  WHERE cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) AND CategoryId = '" + id + "' GROUP BY DateString ORDER BY YEAR(`DATE`), MONTH(`DATE`)";
                }
                else if (daysDifference > 365)
                {
                    //WHERE View_OrderDetail.Status = 'wc-completed' AND
                    query2 = "SELECT YEAR(`DATE`) AS DateString, SUM(ProductSoldAmount) AS TotalSale FROM View_OrderDetail  WHERE cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) AND CategoryId = '" + id + "' GROUP BY DateString";
                }


                datewiseSales = dapper.Con().Query<DatewiseSales>(query2).ToList();
                item.DatewiseSales = datewiseSales;


                //WHERE View_OrderDetail.Status = 'wc-completed' AND
                string query3 = "SELECT ProductName, ProductSoldAmount AS totalSale FROM View_OrderDetail  WHERE cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) AND CategoryId = '" + id + "' ORDER BY ProductSoldAmount DESC LIMIT 5";
                topsellingproducts = dapper.Con().Query<TopSellingProduct>(query3).ToList();
                item.TopSellingProducts = topsellingproducts;
                return item;
            }
            else
            {
                CategorywisePerfomance item2 = new CategorywisePerfomance()
                {
                    CategoryId = 0,
                    CategoryName = null,
                    ItemsSold = 0,
                    TotalAmount = 0m,
                    AverageOrderValue = 0m,
                    DatewiseSales = new List<DatewiseSales>(),
                    TopSellingProducts = new List<TopSellingProduct>()
                };
                return item2;

            }

        }
    }
}
