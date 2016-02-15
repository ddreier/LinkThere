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
    public class LinkController : Controller
    {
        private LinkThereContext _context;

        public LinkController(LinkThereContext context)
        {
            _context = context;
        }

        public IActionResult Get()
        {
            string requestKey = Request.Path.Value.Substring(1);
            var results = _context.Links.Where(l => l.Key == requestKey);
            
            if (results.Count() > 0)
            {
                Link link = results.First();
                IncrementLinkClickCount(link);
                return Redirect(link.LinkUrl);
            }
            else
            {
                return HttpNotFound();
            }
        }

        private void IncrementLinkClickCount(Link link)
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                ++link.ClickCount;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    saveFailed = true;

                    link = _context.Links.Where(l => l.Id == link.Id).SingleOrDefault();
                }
            } while (saveFailed);
        }
    }
}
