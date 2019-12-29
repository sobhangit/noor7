using ClosedXML.Excel;
using MD.PersianDateTime;
using noor7.Dtos;
using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
            var avrageCount = _context.NotebookAves.Count();

            if (avrageCount > 0)
            {
                var notebooks = _context.Notebooks.OrderBy(s=>s.NoteBookDate).ToList();
                var notebookAve = _context.NotebookAves.Select(s => s.Month).OrderByDescending(x => x.Month).FirstOrDefault();
                var lastDate = notebooks.Select(s => s.NoteBookDate).LastOrDefault();

            //ایا جدول میانگین وجود دارد و ایا تاریخی در جدول دفترجه است که به میانگین اضافه نشده باشد

                if (notebookAve != null && lastDate > notebookAve)
                {
                    //ردیف هایی که میانگین انها محاسبه نشده است
                    var didnotAddToAve = _context.Notebooks.Where(s => s.NoteBookDate > notebookAve).ToList();
                    notebookAveGenerator(didnotAddToAve);
                }
                
            }else
            {
                var notebooks = _context.Notebooks.OrderBy(s => s.NoteBookDate).ToList();
                notebookAveGenerator(notebooks);
            }

            return View();
        }

        //ایجاد میانگین برای ردیف هایی از دفترچه که هنوز میانگینشان محاسبه نشده است
        public void notebookAveGenerator(List<Notebook> notebooks)
        {
            var studentsID = _context.Students.Select(s => s.Id).ToList();

            var gradeOfNotebook = new List<NotebookAve>();
            float ave = 0;
            var counter = 0;

            foreach (var id in studentsID)
            {
                foreach (var row in notebooks)
                {
                    if (id == row.StudentID)
                    {
                        counter++;
                        ave += row.Grade;

                        if (counter == 5)
                        {
                            ave = ave / counter;
                            counter = 0;
                            gradeOfNotebook.Add(new NotebookAve
                            {
                                StudentID = id,
                                Average = ave,
                                Month = row.NoteBookDate
                            });
                            ave = 0;
                        }
                    }
                }
            }

            _context.NotebookAves.AddRange(gradeOfNotebook);
            _context.SaveChanges();

        }

        public ActionResult AddStudent()
        {
            return RedirectToAction("Index", "StudentManagment");
        }

        public ActionResult AddCourse()
        {
            return RedirectToAction("Index", "CoursesManagment");
        }

        public ActionResult AddDefect()
        {
            return RedirectToAction("Index", "DefectManagment");
        }
       
        public ActionResult AddAbsent()
        {
            return RedirectToAction("Index", "AbsentManagment");
        }

        public ActionResult AddLate()
        {
            return RedirectToAction("Index", "LateManagment");
        }
        public ActionResult AddJob()
        {
            return RedirectToAction("Index", "JobManagment");
        }

        public ActionResult ExportToExcel() {

            var testKey = false ;

            var students = _context.Students.ToList();
            var allCourseForStudent = new List<ExcelReportDto> { };
            var idCoursename = new Dictionary<int, string> { };

            foreach (var stu in students)
            {

                var courseIds = _context.Courses.Where(s => s.StudentID == stu.Id).OrderBy(s => s.ID).Select(s=>s.ID).ToList();
                var courseNames = _context.Courses.Where(s => s.StudentID == stu.Id).OrderBy(s => s.ID).Select(s => s.Title).ToList();

                for (int i = 0; i < courseIds.Count; i++)
                {
                    idCoursename.Add(courseIds[i], courseNames[i]);
                }

                allCourseForStudent.Add(new ExcelReportDto{
                    Id = stu.Id,
                    Fullname = stu.FirstName + " " + stu.LastName,
                    IdAndCourseName = idCoursename,
                    Exams = getAllExamsGradeForStudent(courseIds)
                });

                idCoursename = new Dictionary<int, string> { };
                if (stu.Id == 2 && testKey)
                {
                    break;
                }
            }

            testCreateExcel(allCourseForStudent, testKey);

            return Content("عملیات موفقیت آمیز بود.");
        }

        private void testCreateExcel(List<ExcelReportDto> allCourseForStudent,bool testKey)
        {
            var wb = new XLWorkbook() { RightToLeft = true }; ;
            var ws = wb.Worksheets.Add("لیست نمرات");

            var rngTable = ws.Range("B1:AG1000");
            rngTable.Row(1).Merge();
            rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

            ws.Cell("B2").Value = "لیست نمرات";

            int cell = 3;

            string[] alphabet = {"D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
            "AA","AB","AC","AD","AE","AF","AG","AH","AI","AJ","AK","AL","AM","AN","AO","AP","AQ","AR","AS","AT","AU","AV","AW","AX","AY"};

            var courseTitleGenerate = true;
            var beforeId = 0;
            var c = 3;
            var aCounter = 0;
            var column = 4;
            var userRowsJumper = 3;

            var firstrow = 3;
            var lastrow = 14;

            float gradeOfAll = 0;
            var counterOfAll = 0;


            foreach (var item in allCourseForStudent)
            {

                if (item.Id == 2 && testKey)
                {
                    break;
                }
                var holder = cell + 11;

                //شماره لیست کلاسی
                ws.Cell("B"+cell).Value = item.Id.ToString();
                rngTable.Range("A"+cell+":A"+holder).Merge();

                // تیتر ها
                ws.Cell("C" + cell).Value = item.Fullname.ToString();
                ws.Cell("C" + Convert.ToInt32(cell + 1)).Value = "امتحان ۱";
                ws.Cell("C" + Convert.ToInt32(cell + 2)).Value = "امتحان ۲";
                ws.Cell("C" + Convert.ToInt32(cell + 3)).Value = "امتحان ۳";
                ws.Cell("C" + Convert.ToInt32(cell + 4)).Value = "امتحان ۴";
                ws.Cell("C" + Convert.ToInt32(cell + 5)).Value = "امتحان ۵";
                ws.Cell("C" + Convert.ToInt32(cell + 6)).Value = "امتحان ۶";
                ws.Cell("C" + Convert.ToInt32(cell + 7)).Value = "امتحان ۷";
                ws.Cell("C" + Convert.ToInt32(cell + 8)).Value = "امتحان ۸";
                ws.Cell("C" + Convert.ToInt32(cell + 9)).Value = "امتحان ۹";
                ws.Cell("C" + Convert.ToInt32(cell + 10)).Value = "امتحان ۱۰";
                ws.Cell("C" + holder).Value = "تعداد/میانگین";
                var loopCounter = 0;
                float grades = 0;
                var gradeCounter = 0;

                foreach (var exam in item.Exams)
                {
                    if (exam.Grade == 0)
                    {
                        
                    }

                    if (courseTitleGenerate)
                    {

                        c = headerGenerator(aCounter,alphabet,exam,beforeId,item,c,ws,column,courseTitleGenerate);
                        if (exam.FinalGrade == 20)
                        {
                            if (exam.Grade != 0)
                            {
                                grades += exam.Grade;
                                gradeCounter++;

                            }
                        }
                        else
                        {
                            var changedGrade = (20 / exam.FinalGrade) * exam.Grade;
                            if (exam.Grade != 0)
                            {
                                grades += changedGrade;
                                gradeCounter++;
                            }
                        }

                        beforeId = exam.CourseID;
                        courseTitleGenerate = false;
                    }
                    else
                    {

                        if (beforeId == exam.CourseID)
                        {
                            ws.Cell(c,column).Value = new PersianDateTime(exam.ExamDate).ToString("yy/MM/dd");
                            if (exam.FinalGrade == 20)
                            {
                                ws.Cell(c, column + 1).Value = exam.Grade;
                                if (exam.Grade != 0)
                                {
                                    grades += exam.Grade;
                                    gradeCounter++;
                                }
                            }
                            else
                            {
                                var firststep = float.Parse("20") / exam.FinalGrade;
                                var changedGrade = firststep * exam.Grade;
                                ws.Cell(c, column + 1).Value = changedGrade;
                                ws.Cell(c, column + 1).Style.Font.Bold = true;
                                ws.Cell(c, column + 1).Style.Font.Underline = XLFontUnderlineValues.Single;

                                if (exam.Grade != 0)
                                {
                                    grades += changedGrade;
                                    gradeCounter++;
                                }
                            }

                            c++;
                        }
                        if (beforeId != exam.CourseID)
                        {
                            

                            for (int i = c; i < cell+11; i++)
                            {
                                ws.Cell(i,column).Value = "-";
                                ws.Cell(i,column+1).Value = "-";
                            }

                            ws.Cell(cell + 11, column).Value = gradeCounter;

                            if (grades == 0)
                            {
                                ws.Cell(cell + 11, column + 1).Value = 0;
                            }
                            else
                            {
                                ws.Cell(cell + 11, column + 1).Value = grades / gradeCounter;
                                gradeOfAll += grades / gradeCounter;
                                counterOfAll += 1;
                            }
                            

                            //add bottom border
                            ws.Cell(cell + 11, column).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            ws.Cell(cell + 11, column).Style.Border.BottomBorderColor = XLColor.Red;
                            ws.Cell(cell + 11, column + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                            ws.Cell(cell + 11, column + 1).Style.Border.BottomBorderColor = XLColor.Red;

                            gradeCounter = 0;
                            grades = 0;

                            column += 2;

                            aCounter++;
                            c = userRowsJumper;//for start

                            c = headerGenerator(aCounter, alphabet, exam, beforeId, item, c, ws,column,courseTitleGenerate);

                            if (exam.FinalGrade == 20)
                            {
                                if (exam.Grade != 0)
                                {
                                    grades += exam.Grade;
                                    gradeCounter++;
                                }
                            }
                            else
                            {
                                var firststep = float.Parse("20") / exam.FinalGrade;
                                var changedGrade = firststep * exam.Grade;
                                if (exam.Grade != 0)
                                {
                                    grades += changedGrade;
                                    gradeCounter++;
                                }
                            }

                            beforeId = exam.CourseID;
                            courseTitleGenerate = false;

                            
                        }
                        
                    }

                    loopCounter++;
                    /*foreach (KeyValuePair<int, string> entry in item.IdAndCourseName)
                    {
                        // do something with entry.Value or entry.Key 
                    }*/
                    if (loopCounter == 25)
                    {
                        ws.Cell(cell + 11, column).Value = gradeCounter;
                        ws.Cell(cell + 11, column + 1).Value = grades / gradeCounter;

                        gradeOfAll += grades / gradeCounter;
                        counterOfAll += 1;

                        //add bottom border
                        ws.Cell(cell + 11, column).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        ws.Cell(cell + 11, column).Style.Border.BottomBorderColor = XLColor.Red;
                        ws.Cell(cell + 11, column + 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                        ws.Cell(cell + 11, column + 1).Style.Border.BottomBorderColor = XLColor.Red;

                        gradeCounter = 0;
                        grades = 0;

                        for (int i = c; i < cell + 11; i++)
                        {
                            ws.Cell(i, column).Value = "-";
                            ws.Cell(i, column + 1).Value = "-";
                        }
                    }
                }

                ws.Range(firstrow, column + 2, lastrow, column + 3).Merge();

                if (item.Exams.Count() == loopCounter)
                {
                    var average = gradeOfAll / counterOfAll;

                    ws.Cell(firstrow , 32).Value = average;
                    ws.Cell(firstrow , 32).Style.Font.FontColor = XLColor.Black;
                    ws.Cell(firstrow, 32).Style.Font.FontSize = 32;

                    courseTitleGenerate = true;
                    firstrow += 12;
                    lastrow += 12;

                    gradeOfAll = 0;
                    counterOfAll = 0;
                }

                column = 4;
                cell += 12;
                c = cell;
                userRowsJumper = cell;
            }

            wb.SaveAs(@"e:\test13.xlsx");

        }

        private int headerGenerator(int aCounter, string[] alphabet, Exam exam, int beforeId, ExcelReportDto item, int c, IXLWorksheet ws, int column, bool courseTitleGenerate)
        {
            //c = 3
            var cName = item.IdAndCourseName[exam.CourseID];

            ws.Cell(c,column).Value = cName;
            
            if (courseTitleGenerate)
            {
                ws.Range("D3:E3").Merge();
                ws.Range("F3:G3").Merge();
                ws.Range("H3:I3").Merge();
                ws.Range("J3:K3").Merge();
                ws.Range("L3:M3").Merge();
                ws.Range("N3:O3").Merge();
                ws.Range("P3:Q3").Merge();
                ws.Range("R3:S3").Merge();
                ws.Range("T3:U3").Merge();
                ws.Range("V3:W3").Merge();
                ws.Range("X3:Y3").Merge();
                ws.Range("Z3:AA3").Merge();
                ws.Range("AB3:AC3").Merge();
                ws.Range("AD3:AE3").Merge();

                //ws.Range("AF3:AG3").Merge();

             
            }
            
            c++;

            ws.Cell(c, column).Value = new PersianDateTime(exam.ExamDate).ToString("yy/MM/dd");

            if (exam.FinalGrade != 20)
            {
                
                var firststep = float.Parse("20") / exam.FinalGrade;
                var changedGrade = firststep * exam.Grade;
                ws.Cell(c, column + 1).Value = changedGrade;
                ws.Cell(c, column + 1).Style.Font.Bold = true;
                ws.Cell(c, column + 1).Style.Font.Underline = XLFontUnderlineValues.Single;

            }
            else
            {
                ws.Cell(c, column + 1).Value = exam.Grade;
            }

            c++;

            return c;
        }

        private List<Exam> getAllExamsGradeForStudent(List<int> courseIds)
        {
            var exams = new List<Exam> { };

            foreach (var cId in courseIds)
            {
                var ex = _context.Exams.Where(s => s.CourseID == cId).OrderBy(s => s.CourseID).ToList();
                foreach (var e in ex)
                {
                    exams.Add(e);
                }
            }

            return exams;
        }
    }
}