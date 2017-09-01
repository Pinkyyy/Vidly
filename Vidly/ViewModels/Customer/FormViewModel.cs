using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels.Customer
{
    public class FormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Models.Customer Customer { get; set; }
    }
}