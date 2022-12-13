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
        public IActionResult Index(int pageIndex = 1)
        {
            var people = _context.People;
            
            int pageSize = 10;
            
            if (pageIndex < 1) pageIndex = 1;
            
            int peopleCount = people.Count();
            
            var pager = new Pager(peopleCount, pageIndex, pageSize);
            
            var skip = (pageIndex - 1) * pageSize;

            var paginatedPeople = people.Skip(skip).Take(pager.PageSize);

            ViewBag.Pager = pager;

            return View(paginatedPeople);            
        }

        [Route("people/details/{id}")]
        public IActionResult Details(int id)
        {
            /*var person = _context.People
                .Where(x => x.Id == id)
                .Include(p => p.StarredMovies)
                .ThenInclude(m => m.Rating)
                .Include(p => p.DirectedMovies)
                .ThenInclude(m => m.Rating)
                .SingleOrDefault();*/
            var person = _context.People.Single(p => p.Id == id);
            _context.Entry(person).Collection(p => p.StarredMovies).Query().Include(m => m.Rating).Load();
            _context.Entry(person).Collection(p => p.DirectedMovies).Query().Include(m => m.Rating).Load();

            if (person == null)
                return NotFound();
            return View(person);
        }
    }
}
