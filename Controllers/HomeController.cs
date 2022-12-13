using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVDB.Data;
using MVDB.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace MVDB.Controllers
{
    public class HomeController : Controller
    {
        private MoviesContext _context;

        public HomeController()
        {
            _context = new MoviesContext();
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index(string searchText = "")
        {
            SearchViewModel query = new();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return View();
            }
            else
            {
                query.Query = searchText;
                return RedirectToAction("Search", "Home", new { searchTerm = query.Query });
            }
        }

        [Route("Home/Search/{searchTerm}")]
        public IActionResult Search(string searchTerm)  
        {
            SearchResultViewModel vm = new()
            {
                Movies = _context.Movies.Include(m => m.Rating)
                            .Where(m => m.Title.ToLower().Contains(searchTerm.ToLower()))
                            .OrderByDescending(m => m.Rating.Votes).ToList(),
                People = _context.People.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList(),
            };

            return View(vm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}