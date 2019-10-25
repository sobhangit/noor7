using Newtonsoft.Json;
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

                var studentContext = 
                    _context.Students.Where(s => s.Class == className).ToList();//دریافت دانش اموزان بر اساس نام کلاس

                ViewBag.course = course;
                ViewBag.parcticeValue = parcticeValue;
                ViewBag.className = className;
                ViewBag.pDate = pDate;

                ViewBag.vv = studentContext;

            }


            return View();
        }

        public class Rootobject
        {
            public Practicedata[] practiceData { get; set; }
            public string courseName { get; set; }
            public string practiceDate { get; set; }
        }

        public class Practicedata
        {
            public string StudentId { get; set; }
            public string StudentName { get; set; }
            public string Value { get; set; }
            public string PassedValue { get; set; }
        }

        [HttpPost]
        public ActionResult AddPractice(Rootobject jsonObject)
        {

            if (jsonObject != null)
            {

                var practiceContext = _context.Practices;
                var courseContext = _context.Courses.ToList();

                var courseName = jsonObject.courseName;
                var practiceDate = jsonObject.practiceDate;
                var Data = jsonObject.practiceData;

                var practice = new List<Practice>();

                for (int i = 0; i < Data.Length; i++)
                {

                    var stuID = Convert.ToInt32(Data[i].StudentId);
                    var courseID = courseContext.Where(s => s.StudentID == stuID && s.Title == courseName).Select(s => s.ID).Single();
                   
                    //پرکردن ارایه از تمرین ها
                    practice.Add(new Practice
                    {
                        CourseID = Convert.ToInt32(courseID),
                        Numbers = Convert.ToInt32(Data[i].Value),
                        PassedNumbers = Convert.ToInt32(Data[i].PassedValue),
                        PracticeDate = Convert.ToDateTime(practiceDate)
                    });
                }

                _context.Practices.AddRange(practice);
                _context.SaveChanges();



                return Content("اطلاعات وارد شد");

            }
            else
            {
                return Content("An Error Has occoured");
            }

        }




        











    }
}