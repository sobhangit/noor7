using noor7.Dtos.Job;
using noor7.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class JobManagmentController : Controller
    {
        private readonly SchoolContext _context;

        public JobManagmentController()
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
        public ActionResult AddJob(JobClassDto jobDto)
        {
            if (jobDto.studentID != null)
            {
                try
                {
                    var job = new Job
                    {
                        StudentID = Convert.ToInt32(jobDto.studentID),
                        Cycle = Convert.ToInt32(jobDto.cycle),
                        JobType = jobDto.jobType,
                        Grade = Convert.ToInt32(jobDto.grade)
                    };
                    _context.Jobs.Add(job);
                    _context.SaveChanges();
                    ModelState.Clear();
                    return Content("اطلاعات وارد شد");
                }
                catch (System.FormatException)
                {
                    return Content("فرمت اطلاعات وارد شده صحیح نمی باشد");
                }
                catch (Exception exc)
                {
                    LogError(exc);
                    Console.WriteLine(exc.Message);
                    throw;
                }
                finally
                {


                }
            }
            else
            {
                return Content("صحیح نمیباشد");
            }
        }

        private void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }

        }

    }
}