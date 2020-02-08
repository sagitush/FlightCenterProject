using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightWebApplication.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
        public ActionResult Index()
        {
            return new FilePathResult("~/Views/Page/index.html", "text/html");
        }
        public ActionResult Landing()
        {
            return new FilePathResult("~/Views/Page/Landing.html", "text/html");
        }
        public ActionResult SearchFlights()
        {
            return new FilePathResult("~/Views/Page/searchFlights.html", "text/html");
        }
        public ActionResult Departure()
        {
            return new FilePathResult("~/Views/Page/departure.html", "text/html");
        }
    }
}