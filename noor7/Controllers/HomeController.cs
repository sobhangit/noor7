using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class HomeController : Controller
    {

        SchoolContext context = new SchoolContext();
         
        public ActionResult Index()
        {

            return View();
        }

        
    }
}