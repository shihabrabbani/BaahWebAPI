using BaahWebAPI.Models;
using BaahWebAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaahWebAPI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        BaahDbContext db = new BaahDbContext();

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }
        DashboardViewModel dashboard = new DashboardViewModel();


        //public IActionResult Index()
        //{
        //    List<ViewSalesreport> Salesreport = db.ViewSalesreports.ToList();
        //    //ViewBag.salesreportlist = Salesreport;

        //    var thismonth = Salesreport.Where(f=>f.Date > DateTime.Now.Date.AddDays(-30)).ToList();
        //    var thistotal = thismonth.Sum(f => f.TotalSale);

        //    var prevmonth = Salesreport.Where(f => f.Date > DateTime.Now.Date.AddDays(-60) && f.Date < DateTime.Now.Date.AddDays(-30)).ToList();
        //    var prevtotal = prevmonth.Sum(f => f.TotalSale);

        //    var increase = 100 * ((thistotal-prevtotal)/prevtotal);
        //    ViewBag.Increase = Math.Round(increase).ToString();


        //    ViewBag.TotalSales = thistotal.ToString();
        //    //var aaa = (total / (lastmonth.Count())).ToString();
        //    ViewBag.AverageSale = Math.Round(thistotal / 30).ToString();
        //    ViewBag.TodaysSale = Salesreport.Where(f => f.Date > DateTime.Now.Date).Sum(f => f.TotalSale);

        //    var topprods = db.ViewTopSellingProducts.Take(5).ToList();
        //    List<string> names = new List<string>();
        //    foreach (var product in topprods)
        //    {
        //        names.Add(product.name);
        //    }
        //    ViewBag.TopSellingList = topprods;
        //    ViewBag.TopSellingNames = names;
        //    ViewBag.TotalProducts = db.ViewTopSellingProducts.Count().ToString();

        //    ViewBag.Name = "Test";
        //    ViewBag.NetSale = db.ViewTodayssales.FirstOrDefault().Count.ToString();
        //    ViewBag.TotalSoldProducts = db.ViewTodayssales.FirstOrDefault().Count.ToString();

        //    ViewBag.ProcessingOrders = db.ViewProcessingorders.FirstOrDefault().Count.ToString();
        //    //ViewBag.TodaysSale = db.ViewTodayssales.FirstOrDefault().Count.ToString();
        //    //ViewBag.AverageSale = db.ViewTodayssales.FirstOrDefault().Count.ToString();



        //    return View();
        //}
        //public IActionResult SaleReport()
        //{
        //    //List<ViewSalesreport> Salesreport = db.ViewSalesreports.ToList();
        //    List<ViewSalesreport> Salesreport = db.ViewSalesreports.Where(a => a.Date.Date >= DateTime.Now.Date.AddDays(-7) && a.Date.Date <= DateTime.Now.Date).ToList();

        //    ViewBag.salesreportlist = Salesreport;


        //    var datewisesale = Salesreport.GroupBy(a => a.Date).ToList();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult SaleReport(DateTime FromDate, DateTime ToDate)
        //{
        //    List<ViewSalesreport> Salesreport = db.ViewSalesreports.Where(a=>a.Date.Date >= FromDate.Date && a.Date.Date <= ToDate.Date).ToList();
        //    ViewBag.salesreportlist = Salesreport;
        //    return View();
        //}
        //public IActionResult NetSaleReport()
        //{
        //    //List<ViewNetSalesreport> net = db.ViewNetSalesreports.ToList();
        //    List<ViewNetSalesreport> net = db.ViewNetSalesreports.Where(a => a.Date.Date >= DateTime.Now.Date.AddDays(-15) && a.Date.Date <= DateTime.Now.Date).ToList();
        //    ViewBag.netsalestlist = net;

        //    return View();
        //}
        //[HttpPost]
        //public IActionResult NetSaleReport(DateTime FromDate, DateTime ToDate)
        //{
        //    List<ViewNetSalesreport> net = db.ViewNetSalesreports.Where(a => a.Date.Date >= FromDate.Date && a.Date.Date <= ToDate.Date).ToList();
        //    ViewBag.netsalestlist = net;

        //    return View();
        //}




        //private void GetData()
        //{
        //    List<ViewSalesreport> list = db.ViewSalesreports.ToList();

        //    //ViewBag.ItemOrderList = list;
        //    dashboard.salesreport = list;
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}