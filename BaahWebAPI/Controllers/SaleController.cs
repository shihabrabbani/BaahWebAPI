using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using BaahWebAPI.Models;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController:ControllerBase
    {
        private readonly ILogger<AOVController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public SaleController(ILogger<AOVController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<Sale> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as DateString,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var list = dapper.Con().Query<Sale>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<Sale> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;
            //string query = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";

            DateTime from = DateTime.ParseExact(fDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(tDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan timeDifference = to - from;
            double daysDifference = timeDifference.TotalDays;

            string query2 = "";
            if (daysDifference < 32)
            {
                query2 = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as DateString,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            }
            else if (daysDifference > 31 && daysDifference < 366)
            {
                query2 = "SELECT CONCAT(SUBSTRING(MONTHNAME(`DATE`), 1, 3), '-', RIGHT(YEAR(`DATE`), 2)) AS DateString, SUM(TotalSale) AS TotalSale FROM view_salesreport 	WHERE CAST(DATE AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE)	GROUP BY DateString ORDER BY YEAR(`DATE`), MONTH(`DATE`)";
            }
            else if (daysDifference > 365)
            {
                query2 = "SELECT YEAR(`DATE`) as DateString,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) GROUP BY DateString";
            }
            var list = dapper.Con().Query<Sale>(query2).ToList();

            return list;
        }
    }
}
