using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using LinkThere.Models;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LinkThere.Controllers
{
    public class AdminController : Controller
    {
        private LinkThereContext _context;

        public AdminController(LinkThereContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Links.ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Link link)
        {
            if (ModelState.IsValid && Uri.IsWellFormedUriString(link.LinkUrl, UriKind.Absolute))
            {
                _context.Links.Add(link);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException.Message.Contains("UNIQUE"))
                    {
                        ModelState.AddModelError("Key", "The Link Key must be unique");
                        return View(link);
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            
            return View(link);
        }

        public IActionResult Delete(int id)
        {
            _context.Links.Remove(_context.Links.Where(l => l.Id == id).FirstOrDefault());
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
