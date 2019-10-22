using noor7.Models;
using System;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class StudentManagmentController : Controller
    {
        private readonly SchoolContext _context;
        public StudentManagmentController()
        {
            _context = new SchoolContext();
        }
        // GET: StudentManagment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddStudent(string firstName, string lastName
       , string fatherName, string inSchoolFrom, string classType
       , string nationalCode, string classListNumber)
        {
            try
            {
                var student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    FatherName = fatherName,
                    InSchoolFrom = int.Parse(inSchoolFrom),
                    Class = classType,
                    Code = int.Parse(nationalCode),
                    ClassListNumber = int.Parse(classListNumber)
                };
                _context.Students.Add(student);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //handle errors
                throw;
            }
            return Content("done");
        }
    }
}