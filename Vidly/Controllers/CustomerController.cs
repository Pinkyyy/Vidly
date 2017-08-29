using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerController()//(ApplicationDbContext dbContext)
        {
            _dbContext = new ApplicationDbContext(); //dbContext;
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            base.Dispose(disposing);
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customers = _dbContext.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            return Content("Data was saved");
        }
    }
}