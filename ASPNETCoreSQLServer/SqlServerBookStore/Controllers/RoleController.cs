using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SqlServerBookStore.Models;
using SqlServerBookStore.Data;


namespace SqlServerBookStore.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> db;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.db = roleManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {

            var items = new List<RoleViewModel>();
            items = db.Roles.Select(r => new RoleViewModel
            {
                RoleID = r.Id,
                RoleName = r.Name,
                Description = r.Description
            }).ToList();

            return View(items);
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Detail(string id)
        {
            RoleViewModel item = new RoleViewModel();
            ApplicationRole role = await db.FindByIdAsync(id);
            if (role != null)
            {
                item.RoleID = role.Id;
                item.RoleName = role.Name;
                item.Description = role.Description;
            }
            return View(item);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(RoleViewModel item)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = new ApplicationRole();
                role.Id = item.RoleID;
                role.Name = item.RoleName;
                role.Description = item.Description;

                var result = await db.CreateAsync(role);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            RoleViewModel item = new RoleViewModel();
            ApplicationRole role = await db.FindByIdAsync(id);
            if (role != null)
            {
                item.RoleID = role.Id;
                item.RoleName = role.Name;
                item.Description = role.Description;
            }
            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("RoleID,RoleName,Description")] RoleViewModel item)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await db.FindByIdAsync(item.RoleID);
                if (role != null)
                {
                    role.Id = item.RoleID;
                    role.Name = item.RoleName;
                    role.Description = item.Description;
                    var result = await db.UpdateAsync(role);
                }
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            RoleViewModel item = new RoleViewModel();
            ApplicationRole role = await db.FindByIdAsync(id);
            if (role != null)
            {
                item.RoleID = role.Id;
                item.RoleName = role.Name;
                item.Description = role.Description;
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await db.FindByIdAsync(id);
                var result = await db.DeleteAsync(role);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}