using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NetSaleController:ControllerBase
    {
        private readonly ILogger<NetSaleController> _logger;
        clsDapper dapper = new clsDapper();

        public NetSaleController(ILogger<NetSaleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<NetSale> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select cast(Date as date) as Date,sum(TotalSale) as TotalSale from view_netsalesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var list = dapper.Con().Query<NetSale>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<NetSale> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "select cast(Date as date) as Date,sum(TotalSale) as TotalSale from view_netsalesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var list = dapper.Con().Query<NetSale>(query).ToList();

            return list;
        }
    }
}
