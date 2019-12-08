using Newtonsoft.Json;
using noor7.Dtos.Student;
using noor7.Models;
using System;
using System.Linq;
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
            var students = _context.Students.ToList();
            ViewBag.students = students;
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
            return RedirectToAction("Index", "StudentManagment");
        }

        public ActionResult RemoveStudent(RemoveStudentDto studentDto) {

            var stdID = Convert.ToInt32(studentDto.studentID.ToString());

            _context.Students.Where(s => s.Id == stdID).First().Exit = true;

            var courseIdForSelectedStudent = _context.Courses.Where(s => s.StudentID == stdID).ToList();

            try
            {
                foreach (var item in courseIdForSelectedStudent)
                {
                    _context.Exams.Remove(_context.Exams.Where(s => s.CourseID == item.ID).SingleOrDefault());
                    _context.Practices.Remove(_context.Practices.Where(s => s.CourseID == item.ID).SingleOrDefault());
                }

                _context.Courses.Remove(_context.Courses.Where(s => s.StudentID == stdID).SingleOrDefault());
                _context.Defects.Remove(_context.Defects.Where(s => s.StudentID == stdID).SingleOrDefault());
                _context.Speaks.Remove(_context.Speaks.Where(s => s.StudentID == stdID).SingleOrDefault());
                _context.Absents.Remove(_context.Absents.Where(s => s.StudentID == stdID).SingleOrDefault());
                _context.Lates.Remove(_context.Lates.Where(s => s.StudentID == stdID).SingleOrDefault());
                _context.Absents.Remove(_context.Absents.Where(s => s.StudentID == stdID).SingleOrDefault());
                _context.Notebooks.Remove(_context.Notebooks.Where(s => s.StudentID == stdID).SingleOrDefault());
                _context.SaveChanges();


            }
            catch (Exception)
            {

                throw;
            }
            

            
            return Json(new { success = true, responseText = "اطلاعات دانش آموز انتخاب شده پاک شد" }, JsonRequestBehavior.AllowGet);

        }
    }
}