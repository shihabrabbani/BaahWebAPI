using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    public class UniqueShopperController : ControllerBase
    {
        private readonly ILogger<UniqueShopperController> _logger;
        clsDapper dapper = new clsDapper();

        public UniqueShopperController(ILogger<UniqueShopperController> logger)
        {
            _logger = logger;
        }

        [HttpGet("UniqueShopper")]
        public IEnumerable<view_shoppers> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "SELECT * FROM baahstore.view_shoppers WHERE CAST(DateString AS DATE) BETWEEN  Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) ";
            var list = dapper.Con().Query<view_shoppers>(query).OrderBy(f=>f.Id).ToList();

            return list;
        }


        [HttpGet(("UniqueShopper/{FromDate}&{ToDate}"))]
        public IEnumerable<view_shoppers> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "SELECT * FROM baahstore.view_shoppers WHERE CAST(DateString AS DATE) BETWEEN  Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) ";
            var list = dapper.Con().Query<view_shoppers>(query).OrderBy(f => f.Id).ToList();

            return list;
        }
    }
}
