using MD.PersianDateTime;
using Newtonsoft.Json;
using noor7.Dtos;
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


        [HttpPost]
        public ActionResult AddAbsent(AbsentClassDto absentDto)
        {
            if (absentDto.StudentID != null)
            {
                var persianDateFrom = PersianDateTime.Parse(absentDto.FromDate);
                var persianDateTo = PersianDateTime.Parse(absentDto.ToDate);

                var absent = new Absent
                {
                    StudentID = int.Parse(absentDto.StudentID),
                    FromDate = persianDateFrom.ToDateTime(),
                    ToDate = persianDateTo.ToDateTime(),
                    IsCertificate = absentDto.IsCertificate,
                    Problem = absentDto.Problem,
                    IsTrue = absentDto.IsTrue,
                };

                try
                {
                    _context.Absents.Add(absent);
                    _context.SaveChanges();
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