using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class DefectManagementController : Controller
    {

        private readonly SchoolContext _context;

        public DefectManagementController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddDefect(string Harchi)
        {
            //this is a sample code
            var list = _context.Students.Where(a => a.FirstName.Contains(Harchi))
                .Select(a => new { ID = a.Id, Name = a.FirstName }).ToList();

            return Json(list);

        }
    }
}