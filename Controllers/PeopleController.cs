using Microsoft.AspNetCore.Mvc;
using MVDB.Models;

namespace MVDB.Controllers
{
    public class PeopleController : Controller
    {
        /*
        private List<Customer> customers = new List<Customer>
        {
            new Customer() { Id = 1, Name = "John Smith" },
            new Customer() { Id = 2, Name = "Marry Lawrence" }
        };
        */
        [Route("people")]     
        public IActionResult Index()
        {

            //return View(customers);
            return View();
        }

        [Route("people/details/{id}")]
        public IActionResult Details(int id)
        {
        //    var customer = customers.Find(x => x.Id == id);
        //    if (customer == null)
        //        return NotFound();
        //    return View(customer);
        return View();
        }
    }
}
