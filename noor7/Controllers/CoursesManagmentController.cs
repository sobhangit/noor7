using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class CoursesManagmentController : Controller
    {

        private readonly SchoolContext _context;

        public CoursesManagmentController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCourse(string course)
        {
            if (!string.IsNullOrEmpty(course))
            {
                var students = _context.Students.ToList();
                var Courses = new List<Course>();
                for (int i = 0; i < students.Count; i++)
                {
                    Courses.Add(new Course
                    {
                        StudentID = students[i].Id,
                        Title = course
                    });
                }
                _context.Courses.AddRange(Courses);
                _context.SaveChanges();

            }
            return RedirectToAction("Index", "CoursesManagment");
        }
    }
}