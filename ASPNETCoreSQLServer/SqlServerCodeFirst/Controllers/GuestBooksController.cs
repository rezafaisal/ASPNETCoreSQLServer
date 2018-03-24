using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SqlServerCodeFirst.Models;

namespace SqlServerCodeFirst.Controllers
{
    public class GuestBooksController : Controller
    {
        private readonly SqlServerCodeFirstContext _context;

        public GuestBooksController(SqlServerCodeFirstContext context)
        {
            _context = context;
        }

        // GET: GuestBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.GuestBooks.ToListAsync());
        }

        // GET: GuestBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.GuestBooks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (guestBook == null)
            {
                return NotFound();
            }

            return View(guestBook);
        }

        // GET: GuestBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Message")] GuestBook guestBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guestBook);
        }

        // GET: GuestBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.GuestBooks.SingleOrDefaultAsync(m => m.Id == id);
            if (guestBook == null)
            {
                return NotFound();
            }
            return View(guestBook);
        }

        // POST: GuestBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Message")] GuestBook guestBook)
        {
            if (id != guestBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookExists(guestBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(guestBook);
        }

        // GET: GuestBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.GuestBooks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (guestBook == null)
            {
                return NotFound();
            }

            return View(guestBook);
        }

        // POST: GuestBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guestBook = await _context.GuestBooks.SingleOrDefaultAsync(m => m.Id == id);
            _context.GuestBooks.Remove(guestBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestBookExists(int id)
        {
            return _context.GuestBooks.Any(e => e.Id == id);
        }
    }
}
