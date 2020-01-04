using MD.PersianDateTime;
using Newtonsoft.Json;
using noor7.Dtos.Edit;
using noor7.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class EditController : Controller
    {
        private readonly SchoolContext _context;

        public EditController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {
            var courses = _context.Courses.Where(s => s.StudentID == 1).ToList();//اسم دروس
            ViewBag.vv = courses;
            return View();
        }

        [HttpPost]
        public ActionResult showExam(ShowDto showDto) {

            var className = showDto.calssname;
            var courseName = showDto.course;
            var persianDate = PersianDateTime.Parse(showDto.date);
            var examDate = persianDate.ToDateTime();

            var allStudentInClass = _context.Students.Where(s => s.Class == className).OrderBy(s=>s.Id).ToList();

            List<int> courseIds = new List<int> { };

            foreach (var item in allStudentInClass)
            {
                var a = _context.Courses.Where(s => s.StudentID == item.Id && s.Title == courseName).OrderBy(s => s.StudentID).Select(s=>s.ID).SingleOrDefault();
                courseIds.Add(a);
            }

            var allExam = _context.Exams.Where(s => s.ExamDate == examDate).ToList();

            List<Exam> exams = new List<Exam> { };

            foreach (var item in courseIds)
            {
                var a = allExam.Where(s => s.CourseID == item).SingleOrDefault();
                exams.Add(a);
            }

            Dictionary<int, string> students = new Dictionary<int, string> { };
            foreach (var item in allStudentInClass)
            {
                students.Add(item.Id,item.FirstName + " " + item.LastName);
            }

            var counter = 0;
            var finalGrade = 0;

            Dictionary<int, float> examsForView = new Dictionary<int, float> { };
            foreach (var item in exams)
            {
                counter++;
                examsForView.Add(item.CourseID, item.Grade);
                if (counter == 1)
                {
                    finalGrade = item.FinalGrade;
                }
            }

            var result = new List<SendExamToViewDtos> { };

            result.Add(
                new SendExamToViewDtos {
                    Students = students,
                    CourseIds = courseIds,
                    Exams = examsForView,
                    FinalGrade = finalGrade
                }    
            );

            var jsonObj = JsonConvert.SerializeObject(result);
            return Json(new { success = true, responseText = jsonObj }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult showPractice(ShowDto showDto)
        {
            var className = showDto.calssname;
            var courseName = showDto.course;
            var persianDate = PersianDateTime.Parse(showDto.date);
            var examDate = persianDate.ToDateTime();

            var allStudentInClass = _context.Students.Where(s => s.Class == className).OrderBy(s => s.Id).ToList();

            List<int> courseIds = new List<int> { };

            foreach (var item in allStudentInClass)
            {
                var a = _context.Courses.Where(s => s.StudentID == item.Id && s.Title == courseName).OrderBy(s => s.StudentID).Select(s => s.ID).SingleOrDefault();
                courseIds.Add(a);
            }

            var allPractice = _context.Practices.Where(s => s.PracticeDate == examDate).ToList();

            List<Practice> practices = new List<Practice> { };

            foreach (var item in courseIds)
            {
                var a = allPractice.Where(s => s.CourseID == item).SingleOrDefault();
                practices.Add(a);
            }

            Dictionary<int, string> students = new Dictionary<int, string> { };
            foreach (var item in allStudentInClass)
            {
                students.Add(item.Id, item.FirstName + " " + item.LastName);
            }

            var counter = 0;
            var Numbers = 0;

            Dictionary<int, int> practiceForView = new Dictionary<int, int> { };
            foreach (var item in practices)
            {
                counter++;
                practiceForView.Add(item.CourseID, item.PassedNumbers);
                if (counter == 1)
                {
                    Numbers = item.Numbers;
                }
            }

            var result = new List<SendPracticeToViewDtos> { };

            result.Add(
                new SendPracticeToViewDtos
                {
                    Students = students,
                    CourseIds = courseIds,
                    Practices = practiceForView,
                    Numbers = Numbers
                }
            );

            var jsonObj = JsonConvert.SerializeObject(result);
            return Json(new { success = true, responseText = jsonObj }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult updateExam(UpdateDto updateDto)
        {
            var date = updateDto.examDateForUpdate;
            var persianTime = PersianDateTime.Parse(date);
            var dateTime = persianTime.ToDateTime();

            var courseIds = updateDto.courseIdsForUpdate;
            List<float> grades = new List<float> { };

            foreach (var item in updateDto.Grades)
            {
                if (item == "نمره")
                {
                    continue;
                }
                grades.Add(float.Parse(item));
            }


            for (int i = 0; i < grades.Count() ; i++)
            {
                var cid = courseIds[i];
                var result = _context.Exams.SingleOrDefault(s => s.ExamDate == dateTime && s.CourseID == cid);
                if (result != null)
                {
                    result.Grade = grades[i];
                    _context.SaveChanges();
                }
            }

            return Json(new { success = true, responseText = "امتحان بروزرسانی شد" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult updatePractice(UpdateDto updateDto)
        {

            var date = updateDto.examDateForUpdate;
            var persianTime = PersianDateTime.Parse(date);
            var dateTime = persianTime.ToDateTime();

            var courseIds = updateDto.courseIdsForUpdate;
            List<int> grades = new List<int> { };

            foreach (var item in updateDto.Grades)
            {
                if (item == "نمره")
                {
                    continue;
                }
                grades.Add(int.Parse(item));
            }

            for (int i = 0; i < grades.Count(); i++)
            {
                var cid = courseIds[i];
                var result = _context.Practices.SingleOrDefault(s => s.PracticeDate == dateTime && s.CourseID == cid);
                if (result != null)
                {
                    result.PassedNumbers = grades[i];
                    _context.SaveChanges();
                }
            }

            return Json(new { success = true, responseText = "تکلیف بروزرسانی شد" }, JsonRequestBehavior.AllowGet); return Content("ok");
        }
    }
}