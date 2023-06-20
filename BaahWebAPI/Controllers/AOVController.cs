using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using BaahWebAPI.Models;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AOVController:ControllerBase
    {
        private readonly ILogger<AOVController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public AOVController(ILogger<AOVController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<AOV> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "SELECT     DATE_FORMAT(DATE, '%d/%m/%Y') AS DateString,    SUM(TotalSale) AS TotalSale,    SUM(ItemsSold) AS ItemsSold,    (SUM(TotalSale) / SUM(ItemsSold)) AS AverageOrderValue FROM view_salesreport WHERE DATE BETWEEN '" + fDate + "' AND '" + tDate + "' GROUP BY DateString";
            var list = dapper.Con().Query<AOV>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<AOV> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;
            //string query = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale, SUM(ItemsSold) AS ItemsSold, (SUM(TotalSale) / SUM(ItemsSold)) AS AverageOrderValue  from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";


            DateTime from = DateTime.ParseExact(fDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(tDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan timeDifference = to - from;
            double daysDifference = timeDifference.TotalDays;

            string query = "";
            if (daysDifference < 32)
            {
                query = "SELECT     DATE_FORMAT(DATE, '%d/%m/%Y') AS DateString,    SUM(TotalSale) AS TotalSale,    SUM(ItemsSold) AS ItemsSold,    (SUM(TotalSale) / SUM(ItemsSold)) AS AverageOrderValue FROM view_salesreport WHERE DATE BETWEEN '" + fDate + "' AND '" + tDate + "' GROUP BY DateString";
            }
            else if (daysDifference > 31 && daysDifference < 366)
            {
                query = "SELECT     MONTHNAME(DATE) AS DateString,    SUM(TotalSale) AS TotalSale,    SUM(ItemsSold) AS ItemsSold,    (SUM(TotalSale) / SUM(ItemsSold)) AS AverageOrderValue FROM view_salesreport WHERE DATE BETWEEN '" + fDate + "' AND '" + tDate + "' GROUP BY DateString";
            }
            else if (daysDifference > 365)
            {
                query = "SELECT     Year(DATE) AS DateString,    SUM(TotalSale) AS TotalSale,    SUM(ItemsSold) AS ItemsSold,    (SUM(TotalSale) / SUM(ItemsSold)) AS AverageOrderValue FROM view_salesreport WHERE DATE BETWEEN '" + fDate + "' AND '" + tDate + "' GROUP BY DateString";
            }
            var list = dapper.Con().Query<AOV>(query).ToList();

            return list;
        }
    }
}
