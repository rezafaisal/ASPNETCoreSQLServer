using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Pesan data)
        {
            ViewBag.Pesan = data.Name + " mengatakan " + data.Message;
            return View();
        }

    }
}