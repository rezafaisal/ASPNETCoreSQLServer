using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using SqlServerBookStore.Data;
using SqlServerBookStore.Models;

namespace SqlServerBookStore.Controllers
{
    [Authorize(Roles = "user")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public BooksController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Books
        public IActionResult Index()
        {
            var bookList = _context.Books
                .Include(c => c.Category)
                .Include(ba => ba.BooksAuthors)
                .ThenInclude(a => a.Author)
                .ToList();

            IList<BookViewModel> items = new List<BookViewModel>();
            foreach (Book book in bookList)
            {
                BookViewModel item = new BookViewModel();

                item.ISBN = book.BookID;
                item.Title = book.Title;
                item.Photo = book.Photo;
                item.PublishDate = book.PublishDate;
                item.Price = book.Price;
                item.Quantity = book.Quantity;
                item.CategoryName = book.Category.Name;

                string authorNameList = string.Empty;
                var booksAuthorsList = book.BooksAuthors;
                foreach (BookAuthor booksAuthors in booksAuthorsList)
                {
                    var author = booksAuthors.Author;
                    authorNameList = authorNameList + author.Name + ", ";
                }
                item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);

                items.Add(item);
            }
            return View(items);
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            var book = _context.Books
                .Include(c => c.Category)
                .Include(ba => ba.BooksAuthors)
                .ThenInclude(a => a.Author).SingleOrDefault(b => b.BookID.Equals(id));

            BookViewModel item = new BookViewModel();
            item.ISBN = book.BookID;
            item.Title = book.Title;
            item.PublishDate = book.PublishDate;
            item.Price = book.Price;
            item.Quantity = book.Quantity;
            item.CategoryName = book.Category.Name;

            string authorNameList = string.Empty;
            var booksAuthorsList = book.BooksAuthors;
            foreach (BookAuthor booksAuthors in booksAuthorsList)
            {
                var author = booksAuthors.Author;
                authorNameList = authorNameList + author.Name + ", ";
            }
            item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);

            return View(item);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryID", "Name");
            ViewBag.Authors = new MultiSelectList(_context.Authors.ToList(), "AuthorID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookFormViewModel item)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book();
                book.BookID = item.ISBN;
                book.CategoryID = item.CategoryID;
                book.Title = item.Title;
                book.PublishDate = item.PublishDate;
                book.Price = item.Price;
                book.Quantity = item.Quantity;
                _context.Add(book);

                foreach (int authorId in item.AuthorIDs)
                {
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.BookID = item.ISBN;
                    bookAuthor.AuthorID = authorId;
                    _context.Add(bookAuthor);
                }

                _context.SaveChanges();

                if (item.Photo != null)
                {
                    var file = item.Photo;
                    var uploads = Path.Combine(_environment.WebRootPath, "upload");
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, item.ISBN + ".jpg"), FileMode.Create))
                        {
                            file.CopyToAsync(fileStream);
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryID", "Name");
            ViewBag.Authors = new MultiSelectList(_context.Authors.ToList(), "AuthorID", "Name");

            var book = _context.Books.SingleOrDefault(p => p.BookID.Equals(id));

            BookFormViewModel item = new BookFormViewModel();
            item.ISBN = book.BookID;
            item.Title = book.Title;
            item.PublishDate = book.PublishDate;
            item.Price = book.Price;
            item.Quantity = book.Quantity;
            item.CategoryID = book.CategoryID;

            var authorList = _context.BooksAuthors.Where(p => p.BookID.Equals(book.BookID)).ToList();
            List<int> authors = new List<int>();
            foreach (BookAuthor bookAuthor in authorList)
            {
                authors.Add(bookAuthor.AuthorID);
            }
            item.AuthorIDs = authors.ToArray();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ISBN, CategoryID, Title, Photo, PublishDate, Price, Quantity, AuthorIDs")] BookFormViewModel item)
        {
            if (ModelState.IsValid)
            {
                _context.BooksAuthors.RemoveRange(_context.BooksAuthors.Where(p => p.BookID.Equals(item.ISBN)));
                _context.SaveChanges();

                Book book = _context.Books.SingleOrDefault(p => p.BookID.Equals(item.ISBN));
                book.CategoryID = item.CategoryID;
                book.Title = item.Title;
                book.PublishDate = item.PublishDate;
                book.Price = item.Price;
                book.Quantity = item.Quantity;
                _context.Update(book);

                foreach (int authorId in item.AuthorIDs)
                {
                    BookAuthor bookAuthor = new BookAuthor();
                    bookAuthor.BookID = item.ISBN;
                    bookAuthor.AuthorID = authorId;
                    _context.Add(bookAuthor);
                }

                _context.SaveChanges();

                if (item.Photo != null)
                {
                    var file = item.Photo;
                    var uploads = Path.Combine(_environment.WebRootPath, "upload");
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, item.ISBN + ".jpg"), FileMode.Create))
                        {
                            file.CopyToAsync(fileStream);
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int? id)
        {
            var book = _context.Books
                .Include(c => c.Category)
                .Include(ba => ba.BooksAuthors)
                .ThenInclude(a => a.Author).SingleOrDefault(b => b.BookID.Equals(id));

            BookViewModel item = new BookViewModel();
            item.ISBN = book.BookID;
            item.Title = book.Title;
            item.PublishDate = book.PublishDate;
            item.Price = book.Price;
            item.Quantity = book.Quantity;
            item.CategoryName = book.Category.Name;

            string authorNameList = string.Empty;
            var booksAuthorsList = book.BooksAuthors;
            foreach (BookAuthor booksAuthors in booksAuthorsList)
            {
                var author = booksAuthors.Author;
                authorNameList = authorNameList + author.Name + ", ";
            }
            item.AuthorNames = authorNameList.Substring(0, authorNameList.Length - 2);

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                _context.BooksAuthors.RemoveRange(_context.BooksAuthors.Where(p => p.BookID.Equals(id)));
                _context.SaveChanges();

                var book = _context.Books.SingleOrDefault(m => m.BookID == id);
                _context.Books.Remove(book);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }


        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}
