using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlServerBookStore.Models;
using SqlServerBookStore.Data;

namespace SqlServerBookStore.Controllers
{
    public class LatihanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LatihanController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Template()
        {
            ViewBag.Authors = _context.Authors.ToList();
            return View();
        }


        public IActionResult ContohModel()
        {
            return View();
        }

        public IActionResult ContohAtributModel()
        {
            return View();
        }
    }
}