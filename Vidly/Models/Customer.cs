using System;
using System.ComponentModel.DataAnnotations;
using Vidly.AttributeValidators;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Is subscribed to newsletter")]
        public bool IsSubscribedToNewsletter { get; set; }

        //navigation property
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership type")]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of birth")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}