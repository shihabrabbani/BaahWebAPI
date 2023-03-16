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
        private readonly ILogger<ProfitController> _logger;
        clsDapper dapper = new clsDapper();
        //----Demo----//
        //var aaa = dapper.Con().Query<ViewSalesreport>("select * from view_salesreport");

        public SaleController(ILogger<ProfitController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<Sale> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var list = dapper.Con().Query<Sale>(query).ToList();

            return list;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<Sale> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;
            string query = "select DATE_FORMAT(cast(Date as date),'%d/%m/%Y') as Date,sum(TotalSale) as TotalSale from view_salesreport where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by CAST(Date as Date)";
            var list = dapper.Con().Query<Sale>(query).ToList();

            return list;
        }
    }
}
