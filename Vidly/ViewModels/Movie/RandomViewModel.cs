using System.Collections.Generic;

namespace Vidly.ViewModels.Movie
{
    public class RandomViewModel
    {
        public Models.Movie Movie { get; set; }
        public List<Models.Customer> Customers { get; set; }
    }
}