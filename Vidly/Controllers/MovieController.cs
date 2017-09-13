using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels.Movie;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Movies
        public ActionResult Random()
        {
            var movie = _dbContext.Movies.Single(m => m.Id == 1);
            var customers = _dbContext.Customers.ToList();

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

            var movies = _dbContext.Movies.Include(m => m.Genre).ToList();

            if (User.IsInRole(RoleName.CanManageMovies))
                return View(movies);
            else
                return View("IndexReadOnly", movies);
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Create()
        {
            var viewModel = new FormViewModel
            {
                Movie = new Movie(),
                Genres = _dbContext.Genres.ToList()
            };
            return View("Form", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new FormViewModel
            {
                Movie = movie,
                Genres = _dbContext.Genres.ToList()
            };
            return View("Form", viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _dbContext.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult Delete(int id)
        {
            var movie = _dbContext.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "Id,Name,ReleaseDate,GenreId,DateAdded,NumberInStock")]Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new FormViewModel
                {
                    Movie = movie,
                    Genres = _dbContext.Genres.ToList()
                };
                return View("Form", viewModel);
            }

            if (movie.Id == 0)
            {
                //_dbContext.Movies.Add(movie);
                _dbContext.Entry(movie).State = EntityState.Added;
            }
            else
            {
                _dbContext.Entry(movie).State = EntityState.Modified;
                //var movieInDb = _dbContext.Movies.Single(m => m.Id == movie.Id);
                //movieInDb.Name = movie.Name;
                //movieInDb.DateAdded = movie.DateAdded;
                //movieInDb.GenreId = movie.GenreId;
                //movieInDb.ReleaseDate = movie.ReleaseDate;
                //movieInDb.NumberInStock = movie.NumberInStock;
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }
    }
}