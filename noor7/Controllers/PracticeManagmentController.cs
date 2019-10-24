using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class PracticeManagmentController : Controller
    {

        private readonly SchoolContext _context;

        public PracticeManagmentController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index(string course, string parcticeValue, string className, string pDate)
        {
            if (!string.IsNullOrEmpty(course) && !string.IsNullOrEmpty(parcticeValue) && !string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(pDate))
            {

                var studentContext = _context.Students.Where( s => s.Class == className).ToList();//دریافت دانش اموزان بر اساس نام کلاس
                var stdID = studentContext.Select(s => s.Id);//انتخاب ایدی دانش اموزان کلاس انتخابی

                var courseContext = _context.Courses.ToList();

                //پیدا کردن ایدی درس براساس ایدی دانش اموز و عنوان درس
                foreach (var item in stdID)
                {
                    var coID = courseContext.Where(s => s.StudentID == item & s.Title == course).Select(t=>t.ID).ToList();
                }


                ViewBag.course = course;
                ViewBag.parcticeValue = parcticeValue;
                ViewBag.className = className;
                ViewBag.pDate = pDate;

                ViewBag.vv = studentContext;

            }


            return View();
        }

        public class practiceData
        {
            public int[] StudentId { get; set; }
            public string[] StudentName { get; set; }
            public int[] Value { get; set; }
            public int[] PassedValue { get; set; }
        }

        [HttpPost]
        public ActionResult AddPractice(practiceData practice) {

            if (practice != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("An Error Has occoured");
            }

        }





       
    }
}