using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using RealEstate.Properties;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        public RealEstateContext Context = new RealEstateContext();

        public async Task<ActionResult> Index()
        {
            //var col = await Context.Database.ListCollectionsAsync();
            //ViewBag.DatabaseSettings = Json(col);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}