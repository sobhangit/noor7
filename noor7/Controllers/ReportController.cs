using noor7.Dtos.Report;
using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MD.PersianDateTime;

namespace noor7.Controllers
{
    public class ReportController : Controller
    {
        private readonly SchoolContext _context;

        public ReportController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {

            var students = _context.Students.ToList();
            ViewBag.vv = students;

            return View();
        }

        
        public ActionResult CreateReport(studentForReportDto studentForReport) {

            var stdID = Convert.ToInt32(studentForReport.studentID);

            var courseListForSelectedStudent = _context.Courses.Where(s => s.StudentID == stdID ).ToList();

            var practiceForSelectedStudent = new List<Practice>();
            var examForSelectedStudent = new List<Exam>();

            foreach (var item in courseListForSelectedStudent)
            {
                var itemID = Convert.ToInt32(item.ID);
                var practice_temp = _context.Practices.Where(s => s.CourseID == itemID).ToList();

                foreach (var practice in practice_temp)
                {
                    practiceForSelectedStudent.Add(practice);
                }

                var exam_temp = _context.Exams.Where(s => s.CourseID == itemID).ToList();


                foreach (var exam in exam_temp)
                {
                    examForSelectedStudent.Add(exam);
                }
                
            }

            var examsForPrint = new List<ExamForReportDto>();

            foreach (var item in examForSelectedStudent)
            {
                var persianDateTime = new PersianDateTime(item.ExamDate);

                examsForPrint.Add(

                    new ExamForReportDto
                    {

                        CourseID = item.CourseID,
                        Grade = item.Grade,
                        FinalGrade = item.FinalGrade,
                        ExamType = item.ExamType.ToString(),
                        ExamDate = persianDateTime.ToString("yy/MM/dd")

                    }     
                    
               ) ;
            }

            var objForTable = practiceReportForStudent(practiceForSelectedStudent, studentForReport);

            var jsonObj = JsonConvert.SerializeObject(new forTableReportDto { ReportDtos = objForTable , Exams = examsForPrint });

            return Json(new { success = true, responseText = jsonObj }, JsonRequestBehavior.AllowGet);
        }

        

        public List<ReportDto> practiceReportForStudent(List<Practice> practiceForSelectedStudent, studentForReportDto studentForReport)
        {
            List<ReportDto> reportList = new List<ReportDto>();

            //دریافت نام دروس و درصدشان
            var courseAndPercent = calCulatePercentOfCourseForClass(studentForReport);

            int beforeId = 0;

            int count = 0;
            int studentpersent = 0;
            var courseName = " ";

            int classpersent = 100;

            int innerCounter = 0;
            int counter = 0;
            int checkCount = 0;

            foreach (var item in practiceForSelectedStudent)
            {
                count++;

                if (beforeId != item.CourseID && beforeId != 0 )
                {
                    counter++;

                    foreach (var cp in courseAndPercent)
                    {
                        innerCounter++;
                        if (innerCounter == counter)
                        {
                            courseName = cp.Key;
                            classpersent = cp.Value;

                            innerCounter = 0;
                            break;
                        }
                    }

                    studentpersent = studentpersent / checkCount;

                    reportList.Add(new ReportDto
                    {
                        Id = counter,
                        CourseName = courseName,
                        CourseId = beforeId,
                        PercentOfWork = studentpersent,
                        PercentOfClass = classpersent,
                        SeeNumbers = checkCount
                    });

                    checkCount = 0;
                    studentpersent = calCulatePercentOfCourse(item.Numbers, item.PassedNumbers);
                    checkCount++;

                } else if(count == practiceForSelectedStudent.Count)
                {
                    counter++;
                    checkCount++;

                    foreach (var cp in courseAndPercent)
                    {
                        innerCounter++;
                        if (innerCounter == counter)
                        {
                            courseName = cp.Key;
                            classpersent = cp.Value;

                            innerCounter = 0;
                            break;
                        }
                    }

                    studentpersent = studentpersent / checkCount;

                    reportList.Add(new ReportDto
                    {
                        Id = counter,
                        CourseName = courseName,
                        CourseId = practiceForSelectedStudent.Last().CourseID,
                        PercentOfWork = studentpersent,
                        PercentOfClass = classpersent,
                        SeeNumbers = checkCount
                    });
                }
                else
                {
                    checkCount++;
                    studentpersent += calCulatePercentOfCourse(item.Numbers, item.PassedNumbers);
                }



                beforeId = item.CourseID;//برای بازدید بعدی


            }


            return reportList;
        }

        public Dictionary<string,int> calCulatePercentOfCourseForClass(studentForReportDto studentForReport)
        {
            var stdID = Convert.ToInt32(studentForReport.studentID);
            //پیدا کردن کلاس دانش اموز انتخابی
            var className = _context.Students.Where(s => s.Id == stdID).Select(s => s.Class).Single();
            //دریافت شناسه دانش اموزان ان کلاس
            var allStudentInClass = _context.Students.Where(s => s.Class == className).Select(s => s.Id).ToList();

            var course = _context.Courses.ToList();

            var initCourseName = " ";
            //دریافت نام دروس 
            List<string> courseNames = new List<string>();

            foreach (var item in course)
            {
                if (item.Title != initCourseName)
                {
                    initCourseName = item.Title;
                    courseNames.Add(item.Title);
                }

            }

            List<int> courseIdForAll = new List<int>();//ایدی دانش اموزان برای میانگین کلاس

            foreach (var item in courseNames)
            {

                foreach (var c in allStudentInClass)
                {
                    var stdIdOnce = Convert.ToInt32(c);
                    var courseOnce = _context.Courses.Where(s => s.StudentID == stdIdOnce && s.Title == item).Select(s => s.ID).Single();
                    courseIdForAll.Add(courseOnce);

                }

            }

            var practice = _context.Practices.ToList();

            List<int> percentOfClass = new List<int>();

            var classPercent = 0;

            var dividTo = 0;

            var breakCourse = courseIdForAll.Count/courseNames.Count;
            var count = 0;

            foreach (var cID in courseIdForAll)
            {
                count++;

                foreach (var item in practice)
                {
                    if (item.CourseID == cID)
                    {
                        classPercent += calCulatePercentOfCourse(item.Numbers, item.PassedNumbers);
                        dividTo++;
                    }
                }

                if (count % breakCourse == 0)
                {
                    percentOfClass.Add(classPercent / dividTo);
                    classPercent = 0;
                    dividTo = 0;
                }

            }


            var courseAndPercent = new Dictionary<string, int>();

            for (int i = 0; i < courseNames.Count; i++)
            {
                courseAndPercent.Add(courseNames[i], percentOfClass[i]);
            }
            

            return courseAndPercent;
        }

        public int calCulatePercentOfCourse(int numbers, int passedNumbers)
        {
            return (passedNumbers * 100)/ numbers; 
        }

    }
}