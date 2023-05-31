using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AbandonedCartController:ControllerBase
    {
        private readonly ILogger<AbandonedCartController> _logger;
        clsDapper dapper = new clsDapper();

        public AbandonedCartController(ILogger<AbandonedCartController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<AbandonedCart> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "SELECT DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS `DateString`, COUNT(id) AS `TotalAbandonedCarts` FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite WHERE CAST(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) 	GROUP BY DateString ORDER BY DateString";
            var list = dapper.Con().Query<AbandonedCart>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<AbandonedCart> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            //string query = "SELECT DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS `Date`, COUNT(id) AS `TotalAbandonedCarts` FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite WHERE CAST(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') ORDER BY `Date` DESC";
            //var list = dapper.Con().Query<AbandonedCart>(query).ToList();


            DateTime from = DateTime.ParseExact(fDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(tDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan timeDifference = to - from;
            double daysDifference = timeDifference.TotalDays;

            string query = "";
            if (daysDifference < 32)
            {
                query = "SELECT DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS `DateString`, COUNT(id) AS `TotalAbandonedCarts` FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite WHERE CAST(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) 	GROUP BY DateString ORDER BY DateString";
            }
            else if (daysDifference > 31 && daysDifference < 366)
            {
                query = "SELECT CONCAT(SUBSTRING(MONTHNAME(FROM_UNIXTIME(`abandoned_cart_time`)), 1, 3), '-', RIGHT(YEAR(FROM_UNIXTIME(`abandoned_cart_time`)), 2)) AS `DateString`, COUNT(id) AS `TotalAbandonedCarts` FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite 	WHERE CAST(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) 	GROUP BY DateString ORDER BY YEAR(FROM_UNIXTIME(`abandoned_cart_time`)), MONTH(FROM_UNIXTIME(`abandoned_cart_time`))";
            }
            else if (daysDifference > 365)
            {
                query = "SELECT YEAR(FROM_UNIXTIME(`abandoned_cart_time`)) AS `DateString`, COUNT(id) AS `TotalAbandonedCarts` FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite WHERE CAST(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) 	GROUP BY DateString ORDER BY DateString";
            }
            var list = dapper.Con().Query<AbandonedCart>(query).ToList();
            return list;
        }
    }
}
