using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels.Customer;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private readonly List<Customer> _customers = new List<Customer>
            {
                new Customer {Name = "Jan Kowlaski", Id = 1},
                new Customer {Name = "Zbigniew Zielony", Id = 2},
                new Customer {Name = "Anna Pyszna", Id = 3}
            };
        // GET: Customer
        public ActionResult Index()
        {

            var listOfCustomers = new ListViewModel
            {
                Customers = _customers
            };

            return View(listOfCustomers);
        }

        public ActionResult Details(int id)
        {
            var customer = _customers.Find(c => c.Id == id);
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}