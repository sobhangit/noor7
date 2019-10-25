using noor7.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddStudent()
        {
            return RedirectToAction("Index", "StudentManagment");
        }

        public ActionResult AddCourse()
        {
            return RedirectToAction("Index", "CoursesManagment");
        }

        public ActionResult AddDefect()
        {
            return RedirectToAction("Index", "DefectManagment");
        }
       
        public ActionResult AddAbsent()
        {
            return RedirectToAction("Index", "AbsentManagment");
        }
    }
}