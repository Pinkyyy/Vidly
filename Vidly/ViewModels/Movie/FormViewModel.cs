using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels.Movie
{
    public class FormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Models.Movie Movie { get; set; }
    }
}