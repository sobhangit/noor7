using MD.PersianDateTime;
using Newtonsoft.Json;
using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class NotebookManagmentController : Controller
    {

        private readonly SchoolContext _context;

        public NotebookManagmentController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index(string className, string notebookDate, string numberOfDays)
        {

            var persianDate = PersianDateTime.Parse(notebookDate);
            var englishTime = persianDate.ToDateTime().ToString("yy/MM/dd");

            if (!string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(notebookDate) && !string.IsNullOrEmpty(numberOfDays))
            {

                var studentContext = _context.Students.Where(s => s.Class == className).ToList();//دریافت دانش اموزان بر اساس نام کلاس

                ViewBag.className = className;
                ViewBag.notebookDate = persianDate.ToString("yy/MM/dd");
                ViewBag.numberOfDays = numberOfDays;

                ViewBag.vv = studentContext;

            }

            return View();
        }



        public class Rootobject
        {
            public Notebookdata[] notebookData { get; set; }
            public string notebookDate { get; set; }
            public string numberOfDays { get; set; }
        }

        public class Notebookdata
        {
            public string StudentId { get; set; }
            public string StudentName { get; set; }
            public string Grade { get; set; }
        }

        [HttpPost]
        public ActionResult AddNotebook(Rootobject jsonObject)
        {

            if (jsonObject != null)
            {

                var notebookDate = jsonObject.notebookDate;
                var numberOfDays = jsonObject.numberOfDays;
                var notebookData = jsonObject.notebookData;

                var ratio = 1.0;
                var persianDate = PersianDateTime.Parse(notebookDate);
                var englishTime = persianDate.ToDateTime();


                switch (Convert.ToInt32(numberOfDays))
                {
                    case 1 :
                        ratio = 6;
                        break;
                    case 2:
                        ratio = 3;
                        break;
                    case 3:
                        ratio = 2;
                        break;
                    case 4:
                        ratio = 1.5;
                        break;
                    case 5:
                        ratio = 1.2;
                        break;
                    default:
                        ratio = 1;
                        break;
                }

                var notebooks = new List<Notebook>();

                for (int i = 0; i < notebookData.Length; i++)
                {

                    var stuID = Convert.ToInt32(notebookData[i].StudentId);
                    var grade = Convert.ToInt32(notebookData[i].Grade) * ratio;
                    //پرکردن ارایه از تمرین ها
                    notebooks.Add(new Notebook
                    {
                        StudentID = stuID,
                        Grade = Convert.ToInt32(grade),
                        NoteBookDate = englishTime
                    });
                }

                _context.Notebooks.AddRange(notebooks);
                _context.SaveChanges();



                return Content("اطلاعات وارد شد");

            }
            else
            {
                return Content("An Error Has occoured");
            }

        }

    }
}