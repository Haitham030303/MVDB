using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MVDB.Data;
using MVDB.Models;

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
        public IActionResult Index(int pageIndex = 1)
        {
            var movies = _context.Movies.Include(m => m.Rating).OrderByDescending(m => m.Rating.Votes);


            int pageSize = 100;

            if (pageIndex < 1) pageIndex = 1;

            int moviesCount = movies.Count();

            var pager = new Pager(moviesCount, pageIndex, pageSize);

            var skip = (pageIndex - 1) * pageSize;

            var paginatedMovies = movies.Skip(skip).Take(pager.PageSize);

            ViewBag.Pager = pager;

            return View(paginatedMovies);
        }

        [Route("movies/details/{id}")]
        public IActionResult Details(int id)
        {
            /* var movie = _context.Movies
                                     .Where(m => m.Id == id)
                                     .Include(m => m.Rating)
                                     .Include(m => m.Stars)
                                     .Include(m => m.Directors)
                                     .SingleOrDefault();
             */
            var validId = _context.Movies.Any(m => m.Id == id);
            if (!validId)
                return NotFound();

            var movie = _context.Movies.Single(m => m.Id == id);
            _context.Entry(movie).Collection(m => m.Stars).Load();
            _context.Entry(movie).Collection(m => m.Directors).Load();

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
