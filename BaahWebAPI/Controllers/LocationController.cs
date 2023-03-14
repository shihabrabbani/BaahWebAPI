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

            string query = "SELECT distinct districtname as  'name', Sum(ItemsSold) as 'value' FROM baahstore.view_locationwisesale INNER JOIN zDistricts ON view_locationwisesale.Location = zDistricts.districtId where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by districtname order by Sum(ItemsSold) desc";
            var locations = dapper.Con().Query<Location>(query).ToList();

            return locations;
        }
        //public IActionResult Datewise()
        //{
        //    string fDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        //    string tDate = DateTime.Now.ToString("yyyy-MM-dd");



        //    string query = "Select distinct Location as label, Sum(ItemsSold) value from LocationWiseSale where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by label order by Sum(TotalSale) desc";
        //    var list = dapper.Con().Query<Tree>(query);



        //    ViewBag.fDate = fDate;
        //    ViewBag.tDate = tDate;
        //    ViewBag.ListJson = JsonConvert.SerializeObject(list);


        //    return View();
        //}

        //[HttpGet("{searchTerm}")]
        //public async Task<IEnumerable<HomeBannerSlider>> Get(int searchTerm)


        [HttpGet("{FromDate}&{ToDate}")]
        public IEnumerable<Location> Get(string FromDate, string ToDate)
        {
            string fDate = FromDate;
            string tDate = ToDate;

            string query = "SELECT distinct districtname as  'name', Sum(ItemsSold) as 'value' FROM baahstore.view_locationwisesale INNER JOIN zDistricts ON view_locationwisesale.Location = zDistricts.districtId where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by districtname order by Sum(ItemsSold) desc";
            var locations = dapper.Con().Query<Location>(query).ToList();

            return locations;
        }
        //[HttpPost]
        //public IActionResult Datewise(DateTime FromDate, DateTime ToDate)
        //{
        //    string fDate = FromDate.ToString("yyyy-MM-dd");
        //    string tDate = ToDate.ToString("yyyy-MM-dd");

        //    var list = dapper.Con().Query<DpShippingDetail>("Select distinct Location, Count(ItemsSold) as ItemsSold, Sum(TotalSale) TotalSale from LocationWiseSale where cast(Date as Date) Between Cast('" + fDate + "' as Date) and Cast('" + tDate + "' as Date) group by Location order by Sum(TotalSale) desc");

        //    ViewBag.fDate = fDate;
        //    ViewBag.tDate = tDate;
        //    ViewBag.List = list;

        //    return View();
        //}



        //internal class Tree
        //{
        //    public string label { get; set; }
        //    public string value { get; set; }
        //}
        public class Location
        {
            public string name { get; set; }
            public int value { get; set; }
        }
    }

}
