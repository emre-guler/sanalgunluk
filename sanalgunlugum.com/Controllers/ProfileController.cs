﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sanalgunlugum.com.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}