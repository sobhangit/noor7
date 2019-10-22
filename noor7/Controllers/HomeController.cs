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
            ViewBag.student = students;
            return View();

        }

        [HttpPost]
        public ActionResult AddDefect(Defect defect,FormCollection form)
        {
            var students = _context.Students.ToList();
            ViewBag.student = students;

            var StudentID = form[0];
            defect.StudentID = Convert.ToInt32(StudentID);
            _context.Defects.Add(defect);
            _context.SaveChanges();

            ModelState.Clear();
            return View();
        }


    }
}