﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BolindersBil.Web.Controllers
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Search()
        {


            return View();
        }
    }
}