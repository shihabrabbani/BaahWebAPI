using Microsoft.AspNetCore.Mvc;
using Dapper;
using BaahWebAPI.DapperModels;
using Newtonsoft.Json;

namespace BaahWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController:ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        clsDapper dapper = new clsDapper();

        public LocationController(ILogger<LocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Location> Get()
        {
            string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string tDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "SELECT @row_number:=@row_number+1 AS `SerialNo`, `name`, `value` FROM (SELECT DISTINCT districtname AS `name`, SUM(ItemsSold) AS `value` FROM baahstore.view_locationwisesale INNER JOIN zDistricts ON view_locationwisesale.Location = zDistricts.districtId WHERE CAST(Date AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY districtname ORDER BY SUM(ItemsSold) DESC) AS `result`, (SELECT @row_number:=0) AS `row_number`;";
            var locations = dapper.Con().Query<Location>(query).ToList();

            return locations;
        }


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<Location> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "SELECT @row_number:=@row_number+1 AS `SerialNo`, `name`, `value` FROM (SELECT DISTINCT districtname AS `name`, SUM(ItemsSold) AS `value` FROM baahstore.view_locationwisesale INNER JOIN zDistricts ON view_locationwisesale.Location = zDistricts.districtId WHERE CAST(Date AS DATE) BETWEEN CAST('" + fDate + "' AS DATE) AND CAST('" + tDate + "' AS DATE) GROUP BY districtname ORDER BY SUM(ItemsSold) DESC) AS `result`, (SELECT @row_number:=0) AS `row_number`;";
            var locations = dapper.Con().Query<Location>(query).ToList();

            return locations;
        }
    }

}
