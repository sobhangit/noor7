using noor7.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class JobManagmentController : Controller
    {
        private readonly SchoolContext _context;

        public JobManagmentController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {

            return View();
        }

        
    }
}