using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MVDB.Data;

namespace MVDB.Controllers
{
    public class MoviesController : Controller
    {
        private MoviesContext _context;

        public MoviesController()
        {
            _context = new MoviesContext();
        }

        [Route("movies")]
        public IActionResult Index()
        {
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            
            var movies = _context.Movies.Include(m => m.Rating).OrderByDescending(m => m.Rating.Votes).Take(99);
            
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            
            return View(movies);
        }

        [Route("movies/details/{id}")]
        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                                    .Where(m => m.Id == id)
                                    .Include(m => m.Stars)
                                    .Include(m => m.Directors)
                                    .SingleOrDefault();

            if (movie == null)
                return NotFound();
            return View(movie);
        }


        [Route("movies/released/{year:regex(^\\d{{4}}$):range(1970, 2029)}")]
        public IActionResult Released(int year)
        {
            return Content("Year: " + year);
        }
        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
