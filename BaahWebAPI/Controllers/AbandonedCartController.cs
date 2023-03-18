using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

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

            string query = "SELECT DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS `Date`, COUNT(id) AS `TotalAbandonedCarts` FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite WHERE CAST(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') ORDER BY `Date` DESC";
            var list = dapper.Con().Query<AbandonedCart>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<AbandonedCart> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "SELECT DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS `Date`, COUNT(id) AS `TotalAbandonedCarts` FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite WHERE CAST(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d') ORDER BY `Date` DESC";
            var list = dapper.Con().Query<AbandonedCart>(query).ToList();

            return list;
        }
    }
}
