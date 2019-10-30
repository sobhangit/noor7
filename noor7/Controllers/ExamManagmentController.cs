using MD.PersianDateTime;
using Newtonsoft.Json;
using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class ExamManagmentController : Controller
    {

        private readonly SchoolContext _context;

        public ExamManagmentController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index(string course, string finalGrade, string examtype, string className, string examDate)
        {

            if (!string.IsNullOrEmpty(course) && !string.IsNullOrEmpty(finalGrade) && !string.IsNullOrEmpty(examtype) && !string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(examDate))
            {

                var studentContext = _context.Students.Where(s => s.Class == className).ToList();//دریافت دانش اموزان بر اساس نام کلاس

                ViewBag.course = course;
                ViewBag.finalGrade = finalGrade;
                ViewBag.examtype = examtype;
                ViewBag.className = className;
                ViewBag.examDate = examDate;

                ViewBag.vv = studentContext;

            }


            return View();
        }

        public class Rootobject
        {
            public Examdata[] examData { get; set; }
            public string courseName { get; set; }
            public string finalGrade { get; set; }
            public string examType { get; set; }
            public string examDate { get; set; }
        }

        public class Examdata
        {
            public string StudentId { get; set; }
            public string StudentName { get; set; }
            public string Grade { get; set; }
        }

        
        [HttpPost]
        public ActionResult AddExam(Rootobject jsonObject)
        {
            var persianDate = PersianDateTime.Parse(jsonObject.examDate);


            if (jsonObject != null)
            {
                var courseContext = _context.Courses.ToList();

                var courseName = jsonObject.courseName;
                var examType = jsonObject.examType;
                var examDate = persianDate.ToDateTime();
                var finalGrade = jsonObject.finalGrade;
                var Data = jsonObject.examData;
                

                var exam = new List<Exam>();

                for (int i = 0; i < Data.Length; i++)
                {

                    var stuID = Convert.ToInt32(Data[i].StudentId);
                    var courseID = courseContext.Where(s => s.StudentID == stuID && s.Title == courseName).Select(s => s.ID).Single();

                    

                    //پرکردن ارایه از تمرین ها
                    exam.Add(new Exam
                    {
                        CourseID = courseID,
                        Grade = float.Parse(Data[i].Grade),
                        FinalGrade = Convert.ToInt32(finalGrade),
                        ExamDate = persianDate.ToDateTime(),
                        ExamType = (Enums.ExamType)Enum.Parse(typeof(Enums.ExamType), examType, true)
                });
                }

                _context.Exams.AddRange(exam);
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