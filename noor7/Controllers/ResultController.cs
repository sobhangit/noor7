using noor7.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class ResultController : Controller
    {
        private readonly SchoolContext _context;

        public ResultController()
        {
            _context = new SchoolContext();
        }

        [HttpGet]
        public ActionResult Index()
        {

            var objForTable = TempData["objForTable"] as string;

            return View();

        }


    }
}