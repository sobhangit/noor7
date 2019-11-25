﻿using noor7.Dtos.Report;
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

            var courseListForSelectedStudent = courseForSelectecStudent(studentForReport);
            var totalPolicy = policy(studentForReport);

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
                float absentControl = item.Grade;
                if (item.Grade == 0)
                {
                    try
                    {
                        var stdID = Convert.ToInt32(studentForReport.studentID);
                        List<Absent> absentForSelectedStudent = _context.Absents.Where(s => s.StudentID == stdID).ToList();
                        List<DateTime> tempDate = new List<DateTime> { };
                        if (absentForSelectedStudent != null)
                        {
                            
                            foreach (var date in absentForSelectedStudent)
                            {
                                var isGraterThan =  date.ToDate.CompareTo(date.FromDate);
                                if (isGraterThan == 1)
                                {
                                    var distanceOfTwoDate = date.FromDate - date.ToDate;
                                    int t = (int)distanceOfTwoDate.TotalDays;
                                    t *= -1;
                                    tempDate.Add(date.FromDate);
                                    for (int i = 1; i <= t; i++)
                                    {
                                        tempDate.Add(date.FromDate.AddDays(+i));
                                    }

                                    foreach (var d in tempDate)
                                    {
                                        if (d == item.ExamDate)
                                        {
                                            absentControl = 1.1F;
                                        }
                                    }
                                }
                                else
                                {

                                    if (date.FromDate == item.ExamDate || date.ToDate == item.ExamDate)
                                    {
                                        absentControl = 1.1F;
                                    }

                                }
                                
                                
                            }

                        }
                        
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                else
                {
                    absentControl = item.Grade;
                }
                
                examsForPrint.Add(

                    new ExamForReportDto
                    {

                        CourseID = item.CourseID,
                        Grade = absentControl,
                        FinalGrade = item.FinalGrade,
                        ExamType = item.ExamType.ToString(),
                        ExamDate = persianDateTime.ToString("yy/MM/dd"),
                        TeacherAdvice = item.TeacherAdvice
                        
                    }     
                    
               ) ;
            }

            var objForTable = practiceReportForStudent(practiceForSelectedStudent, studentForReport);

            var courseForSelected = new List<CourseForReportDto>();

            foreach (var item in courseListForSelectedStudent)
            {

                courseForSelected.Add(

                    new CourseForReportDto
                    {
                        CourseId = item.ID,
                        CourseName = item.Title
                    }
                    
                );
                
            }

            var gradeOfNotebook = notebookForStudent(studentForReport);



            var jsonObj = JsonConvert.SerializeObject(new forTableReportDto { CourseForReportDtos = courseForSelected, ReportDtos = objForTable , Exams = examsForPrint, GradeOfNotebook = gradeOfNotebook, Totalpolicy = totalPolicy });

            return Json(new { success = true, responseText = jsonObj }, JsonRequestBehavior.AllowGet);
        }

        public List<float> notebookForStudent(studentForReportDto studentForReport) {

            var stdID = Convert.ToInt32(studentForReport.studentID);
            var gradeOfNotebook = new List<float>();

            gradeOfNotebook = _context.Notebooks.Where(s => s.StudentID == stdID).Select(s => s.Grade).ToList();

            return gradeOfNotebook;
        }

        public List<ReportDto> practiceReportForStudent(List<Practice> practiceForSelectedStudent, studentForReportDto studentForReport)
        {
            List<ReportDto> reportList = new List<ReportDto>();

            //دریافت نام دروس و درصدشان
            var courseAndPercent = calCulatePercentOfCourseForClass(studentForReport);

            int beforeId = 0;
            int count = 0;
            int studentpersent = 0;
            int classpersent = 100;
            int checkCount = 0;
            var Advice = 0;

            foreach (var item in practiceForSelectedStudent)
            {
                count++;

                if (count == practiceForSelectedStudent.Count)
                {

                    if (beforeId != item.CourseID && beforeId != 0)
                    {

                        studentpersent = studentpersent / checkCount;

                        List<Practice> teacherAdvice = _context.Practices.Where(s => s.CourseID == beforeId).ToList();
                        if (teacherAdvice.Count > 1)
                        {
                            int teCount = 0;
                            foreach (var te in teacherAdvice)
                            {
                                ++teCount;

                                Advice += Convert.ToInt32((te.TeacherAdvice * 100) / te.Numbers);
                            }

                            Advice = Advice / teCount;
                        }
                        else
                        {
                            Advice = (teacherAdvice.Select(s => s.TeacherAdvice).SingleOrDefault() * 100) / teacherAdvice.Select(s => s.Numbers).SingleOrDefault();
                        }

                        reportList.Add(new ReportDto
                        {
                            CourseId = beforeId,
                            PercentOfWork = studentpersent,
                            PercentOfClass = Advice,
                            SeeNumbers = checkCount
                        });

                        Advice = 0;

                        checkCount = 0;
                        checkCount++;
                        studentpersent = calCulatePercentOfCourse(item.Numbers, item.PassedNumbers);


                    }

                    if (beforeId == item.CourseID)
                    {
                        studentpersent += calCulatePercentOfCourse(item.Numbers, item.PassedNumbers);
                    }

                    if (count == 1 || beforeId == item.CourseID)
                    {
                        checkCount++;
                    }

                    studentpersent = studentpersent / checkCount;

                    List<Practice> teacherAdvice1 = _context.Practices.Where(s => s.CourseID == item.CourseID).ToList();
                    if (teacherAdvice1.Count > 1)
                    {
                        int teCount = 0;
                        foreach (var te in teacherAdvice1)
                        {
                            ++teCount;

                            Advice += Convert.ToInt32((te.TeacherAdvice * 100) / te.Numbers);
                        }

                        Advice = Advice / teCount;
                    }
                    else
                    {
                        Advice = (teacherAdvice1.Select(s => s.TeacherAdvice).SingleOrDefault() * 100) / teacherAdvice1.Select(s => s.Numbers).SingleOrDefault();
                    }

                    reportList.Add(new ReportDto
                    {
                        CourseId = practiceForSelectedStudent.Last().CourseID,
                        PercentOfWork = studentpersent,
                        PercentOfClass = Advice,
                        SeeNumbers = checkCount
                    });

                    Advice = 0;

                } else if (beforeId != item.CourseID && beforeId != 0)
                {

                    studentpersent = studentpersent / checkCount;
                    List<Practice> teacherAdvice = _context.Practices.Where(s => s.CourseID == beforeId).ToList();
                    if (teacherAdvice.Count > 1) {
                        int teCount = 0;
                        foreach (var te in teacherAdvice)
                        {
                            ++teCount;
                            
                            Advice += Convert.ToInt32((te.TeacherAdvice * 100) / te.Numbers);
                        }

                        Advice = Advice / teCount;
                    }
                    else
                    {
                        Advice = (teacherAdvice.Select(s=>s.TeacherAdvice).SingleOrDefault()*100) / teacherAdvice.Select(s => s.Numbers).SingleOrDefault();
                    }

                    reportList.Add(new ReportDto
                    {
                        CourseId = beforeId,
                        PercentOfWork = studentpersent,
                        PercentOfClass = Advice,
                        SeeNumbers = checkCount
                    });

                    Advice = 0;

                    checkCount = 0;
                    checkCount++;
                    studentpersent = calCulatePercentOfCourse(item.Numbers, item.PassedNumbers);
                    

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
            var className = _context.Students.Where(s => s.Id == stdID).Select(s => s.Class).SingleOrDefault();
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

                    if (classPercent == 0)
                    {
                        percentOfClass.Add(classPercent);
                    }
                    else {

                        percentOfClass.Add(classPercent / dividTo);
                        classPercent = 0;
                        dividTo = 0;

                    }
                }

            }


            var courseAndPercent = new Dictionary<string, int>();

            for (int i = 0; i < courseNames.Count; i++)
            {
                courseAndPercent.Add(courseNames[i], percentOfClass[i]);
            }
            

            return courseAndPercent;
        }

        public Dictionary<string, int> calCulatePercentOfExamForClass(studentForReportDto studentForReport)
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

            var breakCourse = courseIdForAll.Count / courseNames.Count;
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

        public Dictionary<string,int> policy(studentForReportDto studentForReport) {

            var stdID = Convert.ToInt32(studentForReport.studentID);

            var totalPolicy = new Dictionary<string, int>();

            var naqsElmi = _context.Defects.Where(s => s.Type == Enums.DefectType.علمی && s.StudentID == stdID).Count();
            totalPolicy.Add("elmi", naqsElmi);

            var naqsEnzebati = _context.Defects.Where(s => s.Type == Enums.DefectType.انضباطی && s.StudentID == stdID).Count();
            var late = _context.Lates.Where(s => s.StudentID == stdID && s.IsTrue == false).Count();
            var absent = _context.Absents.Where(s => s.StudentID == stdID && s.IsTrue == false).Count();

            var total = naqsEnzebati + late + absent;
            totalPolicy.Add("total", total);


            return totalPolicy;
        }

        public int calCulatePercentOfCourse(int numbers, int passedNumbers)
        {
            return (passedNumbers * 100)/ numbers; 
        }

        public List<Course> courseForSelectecStudent(studentForReportDto studentForReport) {

            var stdID = Convert.ToInt32(studentForReport.studentID);

            return _context.Courses.Where(s => s.StudentID == stdID).ToList(); ;
        } 

    }
}