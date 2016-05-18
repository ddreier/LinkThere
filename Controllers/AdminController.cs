using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkThere.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LinkThere.Controllers
{
    public class AdminController : Controller
    {
        public IConfiguration _config { get; set; }

        private LinkThereContext _context;

        public AdminController(LinkThereContext context, IConfiguration config)
        {
            _context = context;

            _config = config;
        }

        public IActionResult Index()
        {
            ViewBag.AppUrl = _config.GetSection("AppUrl").Value;

            return View(_context.Links.ToList());
        }

        public IActionResult Add()
        {
            ViewBag.AppUrl = _config.GetSection("AppUrl").Value;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Link link)
        {
            ViewBag.AppUrl = _config.GetSection("AppUrl").Value;

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
            ViewBag.AppUrl = _config.GetSection("AppUrl").Value;

            _context.Links.Remove(_context.Links.Where(l => l.Id == id).FirstOrDefault());
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
