using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manitouage1.Controllers
{
    public class HomeController: Controller
    {
        public ActionResult Index()
        {
            // This is a workaround to make sure everyone has the same roles in 
            // their database.
            new RolesController();

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