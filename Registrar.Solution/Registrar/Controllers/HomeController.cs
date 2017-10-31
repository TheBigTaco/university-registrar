using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Registrar.Models;

namespace Registrar.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
    }
}
