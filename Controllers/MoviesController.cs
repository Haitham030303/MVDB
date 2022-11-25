using Microsoft.AspNetCore.Mvc;
using MVDB.Data;

namespace MVDB.Controllers
{
    public class MoviesController : Controller
    {

        [Route("movies")]
        public IActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue) pageIndex = 1;
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "Title";
            
            return View();
        }

        [Route("movies/released/{year:regex(^\\d{{4}}$):range(1970, 2029)}")]
        public IActionResult Released(int year)
        {
            return Content("Year: " + year);
        }
        
    }
}
