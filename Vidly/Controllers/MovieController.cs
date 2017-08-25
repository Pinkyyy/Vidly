using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels.Movie;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Name = "Shrek!"
            };
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"},
                new Customer {Name = "Customer 3"},
                new Customer {Name = "Customer 4"}
            };

            var viewModel = new RandomViewModel
            {
                Customers = customers,
                Movie = movie
            };

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("id" + id);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }
            return Content(string.Format("pageIndex={0}, sortBy={1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}