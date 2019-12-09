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
    }
}