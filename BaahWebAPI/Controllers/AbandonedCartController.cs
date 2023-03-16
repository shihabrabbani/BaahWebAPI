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

            string query = "SELECT id as Id, (SELECT meta_value FROM baahstore.wp_c84s672ma8_usermeta where user_id=(baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite.user_id) and meta_key = 'billing_email') as Email, user_type as Customer, DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as AbandonedDate, 'Abandoned' as Status FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite where cast(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)";
            var list = dapper.Con().Query<AbandonedCart>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<AbandonedCart> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "SELECT id as Id, (SELECT meta_value FROM baahstore.wp_c84s672ma8_usermeta where user_id=(baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite.user_id) and meta_key = 'billing_email') as Email, user_type as Customer, DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as AbandonedDate FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite where cast(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)";
            var list = dapper.Con().Query<AbandonedCart>(query).ToList();

            return list;
        }
        //public IActionResult Datewise()
        //{
        //    string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        //    string tDate = DateTime.Now.ToString("yyyy-MM-dd");

        //    string query = "SELECT id as Id, (SELECT meta_value FROM baahstore.wp_c84s672ma8_usermeta where user_id=(baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite.user_id) and meta_key = 'billing_email') as Email, user_type as Customer, DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as AbandonedDate FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite where cast(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)";
        //    var list = dapper.Con().Query<DpAbandonedCart>(query);

        //    ViewBag.fDate = null;
        //    ViewBag.tDate = null;
        //    ViewBag.List = list;

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Datewise(DateTime FromDate, DateTime ToDate)
        //{
        //    string fDate = FromDate.ToString("yyyy-MM-dd");
        //    string tDate = ToDate.ToString("yyyy-MM-dd");

        //    var list = dapper.Con().Query<DpAbandonedCart>("SELECT id as Id, (SELECT meta_value FROM baahstore.wp_c84s672ma8_usermeta where user_id=(baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite.user_id) and meta_key = 'billing_email') as Email, user_type as Customer, DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as AbandonedDate FROM baahstore.wp_c84s672ma8_ac_abandoned_cart_history_lite where cast(DATE_FORMAT(FROM_UNIXTIME(`abandoned_cart_time`), '%Y-%m-%d %H:%i:%s') as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date)");



        //    ViewBag.fDate = fDate;
        //    ViewBag.tDate = tDate;
        //    ViewBag.List = list;

        //    return View();
        //}
    }
}
