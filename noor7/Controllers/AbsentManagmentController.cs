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
    public class AbsentManagmentController : Controller
    {

        private readonly SchoolContext _context;

        public AbsentManagmentController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {
            var students = _context.Students.ToList();
            ViewBag.students = students;

            return View();
        }


        public class Rootobject
        {
            public string studentID { get; set; }
            public string problem { get; set; }
            public string fromDate { get; set; }
            public string toDate { get; set; }
            public bool isCertificate { get; set; }
            public bool isTrue { get; set; }
        }


        [HttpPost]
        public ActionResult AddAbsent(Rootobject jsonObject)
        {

            if (jsonObject.studentID != null)
            {

                var studentID = jsonObject.studentID;
                var problem = jsonObject.problem;
                var fromDate = jsonObject.fromDate;
                var toDate = jsonObject.toDate;
                var isCertificate = jsonObject.isCertificate;
                var isTrue = jsonObject.isTrue;

                var persianDateFrom = PersianDateTime.Parse(fromDate);
                var persianDateTo = PersianDateTime.Parse(toDate);
                var englishTimeFrom = persianDateFrom.ToDateTime();
                var englishTimeTo = persianDateTo.ToDateTime();

                _context.Absents.Add(new Absent
                {
                    StudentID = Convert.ToInt32(studentID),
                    Problem = problem,
                    FromDate = englishTimeFrom,
                    ToDate = englishTimeTo,
                    IsCertificate = isCertificate,
                    IsTrue = isTrue

                });

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