using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels.Movie;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private readonly List<Movie> _movies = new List<Movie>
        {
            new Movie {Name = "Movie 1", Id = 1},
            new Movie {Name = "Movie 2", Id = 2},
            new Movie {Name = "Movie 3", Id = 3},
            new Movie {Name = "Movie 4", Id = 4}
        };

        // GET: Movies
        public ActionResult Random()
        {
            var movie = _movies.Single(m => m.Id == 1);

            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1", Id = 1},
                new Customer {Name = "Customer 2", Id = 2},
                new Customer {Name = "Customer 3", Id = 3},
                new Customer {Name = "Customer 4", Id = 4}
            };

            var viewModel = new RandomViewModel
            {
                Customers = customers,
                Movie = movie
            };

            return View(viewModel);
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

            var movies = new ListViewModel
            {
                Movies = _movies
            };

            //return Content(string.Format("pageIndex={0}, sortBy={1}", pageIndex, sortBy));
            return View(movies);
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Create()
        {
            return Content("Create");
        }

        public ActionResult Edit(int id)
        {
            return Content("Edit id" + id);
        }

        public ActionResult Details(int id)
        {
            return Content("Details id" + id);
        }

        public ActionResult Delete(int id)
        {
            return Content("Delete id" + id);
        }
    }
}