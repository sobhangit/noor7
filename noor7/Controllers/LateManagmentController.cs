using MD.PersianDateTime;
using Newtonsoft.Json;
using noor7.Dtos;
using noor7.Dtos.Defect;
using noor7.Dtos.Late;
using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class LateManagmentController : Controller
    {

        private readonly SchoolContext _context;

        public LateManagmentController()
        {
            _context = new SchoolContext();
        }

        public ActionResult Index()
        {
            var students = _context.Students.ToList();
            ViewBag.students = students;

            return View();
        }


        [HttpPost]
        public ActionResult AddLate(LateClassDto lateDto)
        {
            if (lateDto.studentID != null)
            {
                var persianDate = PersianDateTime.Parse(lateDto.lateDate);

                var late = new Late
                {
                    StudentID = Convert.ToInt32(lateDto.studentID),
                    LateDate = persianDate.ToDateTime(),
                    LateTime = Convert.ToInt32(lateDto.howMuch),
                    Problem = lateDto.problem,
                    IsTrue = lateDto.isTrue
                };

                try
                {
                    _context.Lates.Add(late);
                    _context.SaveChanges();
                    ModelState.Clear();
                    return Content("اطلاعات وارد شد");
                }
                catch (Exception exc)
                {

                    throw;
                }
            }
            else {
                return Content("صحیح نمیباشد");
            }
        }

    }
}