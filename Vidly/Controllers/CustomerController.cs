using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels.Customer;

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
            var membershipTypes = _dbContext.MembershipTypes.ToList();
            var viewModel = new FormViewModel
            {
                Customer = customer,
                MembershipTypes = membershipTypes
            };
            return View("Form", viewModel);
        }

        public ActionResult Delete(int id)
        {
            var customer = _dbContext.Customers.Include(m => m.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Create()
        {
            var membershipTypes = _dbContext.MembershipTypes.ToList();
            var viewModel = new FormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new FormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _dbContext.MembershipTypes.ToList()
                };
                return View("Form", viewModel);
                //return (customer.Id == 0) ? View("Create", viewModel) : View("Edit", viewModel);
            }

            if (customer.Id == 0)
            {
                _dbContext.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _dbContext.Customers.Single(c => c.Id == customer.Id);
                //TryUpdateModel(customerInDb); //this updates all object properties
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
    }
}