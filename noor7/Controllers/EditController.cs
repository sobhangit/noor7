using noor7.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class EditController : Controller
    {
        private readonly SchoolContext _context;

        public EditController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}