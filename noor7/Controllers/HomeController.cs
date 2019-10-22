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
        private readonly SchoolContext _context;

        public HomeController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            //why is empty?
            // what is this ?
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(Student student)
        {

            _context.Students.Add(student);
            _context.SaveChanges();

            ModelState.Clear();

            return View();

        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            //????????????

            var students = _context.Students.ToList();

            for (int i = 0; i < students.Count; i++)
            {
                course.StudentID = students[i].Id;
                _context.Courses.Add(course);
                _context.SaveChanges();
            }

            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult AddDefect()
        {
            //???????????
            var students = _context.Students.ToList();

            SelectList selectLists = new SelectList(_context.Students.Select(
                c => new
                {

                    StudentID = c.Id,
                    FullInfo = c.FirstName + " " + c.LastName + " " + " کلاس " + c.Class

                }
            ), "StudentID", "FullInfo") ;

            ViewBag.student = selectLists;

            //ViewData["student"] = selectLists;
            return View();

        }

        [HttpPost]
        public ActionResult AddDefect(FormCollection form)
        {
            var students = _context.Students.ToList();

            ViewBag.student = students;

            Defect obj = new Defect();

            var studentID = form[0];
            var defactType = form[1];
            var description = form[2];
            var defactDate = form[3];

            obj.StudentID = Convert.ToInt32(studentID);
            if (defactType == "انضباطی")
            {
                obj.Type = Enums.DefectType.انضباطی;
            }
            else
            {
                obj.Type = Enums.DefectType.علمی;
            }

            obj.Description = description;
            obj.DefaceDate = Convert.ToDateTime(defactDate);

            _context.Defects.Add(obj);
            _context.SaveChanges();

            ModelState.Clear();
            return View();
        }


    }
}