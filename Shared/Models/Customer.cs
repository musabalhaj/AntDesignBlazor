using Project.Shared.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [EmailDomainValidation(AllowedDomain = "gmail.com")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
