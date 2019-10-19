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

        SchoolContext context = new SchoolContext();
         
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            
            context.Students.Add(student);
            context.SaveChanges();

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
            int count = context.Students.Count();

            List<Student> stu = context.Students.ToList();
            

            for (int i = 0; i < stu.Count; i++)
            {
                course.StudentID = stu[i].Id;
                context.Courses.Add(course);
                context.SaveChanges();
            }

            ModelState.Clear();
            return View();
        }


    }
}