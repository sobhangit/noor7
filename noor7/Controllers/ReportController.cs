using noor7.Dtos.Report;
using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MD.PersianDateTime;
using noor7.Enums;

namespace noor7.Controllers
{
    public class ReportController : Controller
    {

        private readonly SchoolContext _context;

        DateTime fromDateForSelect = new DateTime();
        DateTime toDateForSelect = new DateTime();

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

            var month = studentForReport.selectedMonth.ToString();
            calculateDate(month);

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
                    if (practice.PracticeDate >= fromDateForSelect && practice.PracticeDate <= toDateForSelect)
                    {
                        practiceForSelectedStudent.Add(practice);
                    }
                    
                }

                var exam_temp = _context.Exams.Where(s => s.CourseID == itemID).ToList();

                foreach (var exam in exam_temp)
                {
                    if (exam.ExamDate >= fromDateForSelect && exam.ExamDate <= toDateForSelect)
                    {
                        examForSelectedStudent.Add(exam);
                    }
                    
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
                        if (absentForSelectedStudent != null || absentForSelectedStudent.Count != 0)
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
                        else
                        {
                            absentControl = item.Grade;
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
            
            var notebook = _context.Notebooks.Where(s => s.StudentID == stdID).ToList();

            foreach (var item in notebook)
            {
                if (item.NoteBookDate >= fromDateForSelect && item.NoteBookDate <= toDateForSelect)
                {
                    gradeOfNotebook.Add(item.Grade);
                }
            }

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

            int naqsElmi = 0;
            int naqsEnzebati = 0;
            int late = 0;
            int absent = 0;

            var defectDates = _context.Defects.Where(s => s.Type == Enums.DefectType.علمی && s.StudentID == stdID).Select(s => s.DefaceDate).ToList();
            var defectDatesE = _context.Defects.Where(s => s.Type == Enums.DefectType.انضباطی && s.StudentID == stdID).Select(s => s.DefaceDate).ToList();
            var lateDates = _context.Lates.Where(s => s.StudentID == stdID && s.IsTrue == false).Select(s=>s.LateDate).ToList();
            var absentDates = _context.Absents.Where(s => s.StudentID == stdID && s.IsTrue == false).ToList();

            foreach (var date in defectDates)
            {
                if (date >= fromDateForSelect && date <= toDateForSelect )
                {
                    naqsElmi += 1;
                }
            }
            foreach (var date in defectDatesE)
            {
                if (date >= fromDateForSelect && date <= toDateForSelect)
                {
                    naqsEnzebati += 1;
                }
            }
            foreach (var date in lateDates)
            {
                if (date >= fromDateForSelect && date <= toDateForSelect)
                {
                    late += 1;
                }
            }
            foreach (var date in absentDates)
            {
              
                List<DateTime> tempDate = new List<DateTime> { };
                if (absentDates != null || absentDates.Count != 0)
                {
                    foreach (var absentD in absentDates)
                    {
                        var isGraterThan = absentD.ToDate.CompareTo(absentD.FromDate);
                        if (isGraterThan == 1)
                        {
                            var distanceOfTwoDate = absentD.FromDate - absentD.ToDate;
                            int t = (int)distanceOfTwoDate.TotalDays;
                            t *= -1;
                            tempDate.Add(absentD.FromDate);
                            for (int i = 1; i <= t; i++)
                            {
                                tempDate.Add(absentD.FromDate.AddDays(+i));
                            }

                            foreach (var d in tempDate)
                            {
                                if (d >= fromDateForSelect && d <= toDateForSelect )
                                {
                                    absent += 1;
                                }  
                            }
                        }
                        else
                        {
                            if (absentD.FromDate >= fromDateForSelect && absentD.ToDate <= toDateForSelect)
                            {
                                absent += 1;
                            }
                        }

                    }

                }

            }

            var total = naqsEnzebati + late + absent;

            totalPolicy.Add("elmi", naqsElmi);
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

        private void calculateDate(string month)
        {
            
            var persianMonthNow = new PersianDateTime(DateTime.Now);
            var year = persianMonthNow.Year;

            switch (month)
            {
                case "مهر":
                    fromDateForSelect = new PersianDateTime(year, 07, 01);
                    toDateForSelect = new PersianDateTime(year, 07, 30);
                    break;
                case "آبان":
                    fromDateForSelect = new PersianDateTime(year, 08, 01);
                    toDateForSelect = new PersianDateTime(year, 08, 30);
                    break;
                case "آذر":
                    fromDateForSelect = new PersianDateTime(year, 09, 01);
                    toDateForSelect = new PersianDateTime(year, 09, 30);
                    break;
                case "دی":
                    fromDateForSelect = new PersianDateTime(year, 10, 01);
                    toDateForSelect = new PersianDateTime(year, 10, 30);
                    break;
                case "بهمن":
                    fromDateForSelect = new PersianDateTime(year, 11, 01);
                    toDateForSelect = new PersianDateTime(year, 11, 30);
                    break;
                case "اسفند":
                    fromDateForSelect = new PersianDateTime(year, 12, 01);
                    toDateForSelect = new PersianDateTime(year, 12, 29);
                    break;
                case "فروردین":
                    fromDateForSelect = new PersianDateTime(year, 01, 01);
                    toDateForSelect = new PersianDateTime(year, 01, 31);
                    break;
                case "اردیبهشت":
                    fromDateForSelect = new PersianDateTime(year, 02, 01);
                    toDateForSelect = new PersianDateTime(year, 02, 31);
                    break;
                case "خرداد":
                    fromDateForSelect = new PersianDateTime(year, 03, 01);
                    toDateForSelect = new PersianDateTime(year, 03, 31);
                    break;
            }

        }
    }
}