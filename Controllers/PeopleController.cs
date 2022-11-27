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

            var people = _context.People
                .Take(300);

            return View(people);            
        }

        [Route("people/details/{id}")]
        public IActionResult Details(int id)
        {
            var person = _context.People
                .Include(m => m.StarredMovies)
                .Include(m => m.DirectedMovies)
                .SingleOrDefault(x => x.Id == id);
            if (person == null)
                return NotFound();
            return View(person);
        }
    }
}
