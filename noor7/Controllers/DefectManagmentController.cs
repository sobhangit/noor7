using MD.PersianDateTime;
using Newtonsoft.Json;
using noor7.Dtos;
using noor7.Dtos.Defect;
using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class DefectManagmentController : Controller
    {

        private readonly SchoolContext _context;

        public DefectManagmentController()
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
        public ActionResult AddDefect(DefectClassDto defectDto)
        {
            if (defectDto.studentID != null)
            {
                var persianDate = PersianDateTime.Parse(defectDto.defectDate);

                var defect = new Defect
                {
                    StudentID = int.Parse(defectDto.studentID),
                    Type = (Enums.DefectType)Enum.Parse(typeof(Enums.DefectType), defectDto.defectType, true),
                    Description = defectDto.defectDescription,
                    DefaceDate = persianDate.ToDateTime()
                };

                try
                {
                    _context.Defects.Add(defect);
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