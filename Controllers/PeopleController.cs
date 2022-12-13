using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVDB.Data;
using MVDB.Models;
using System.Linq;
using System.Security.Cryptography.Xml;

namespace MVDB.Controllers
{
    public class PeopleController : Controller
    {
        private MoviesContext _context;

        public PeopleController()
        {
            _context = new MoviesContext();
        }

        [Route("people")]     
        public IActionResult Index()
        {

            var people = _context.People.Take(100);

            var orderedPeople = people.OrderByDescending(p => p.StarredMovies.Sum(m => m.Rating.Votes) + p.DirectedMovies.Sum(m => m.Rating.Votes));
            return View(people);            
        }

        [Route("people/details/{id}")]
        public IActionResult Details(int id)
        { 
            var person = _context.People
                .Where(x => x.Id == id)
                .Include(p => p.StarredMovies)
                .Include(p => p.DirectedMovies)
                .SingleOrDefault();
            
            if (person == null)
                return NotFound();
            return View(person);
        }
    }
}
