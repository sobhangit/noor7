﻿using noor7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace noor7.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new SchoolContext();
            context.Tests.Add(new Test
            {
                Name = ""
            });

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}