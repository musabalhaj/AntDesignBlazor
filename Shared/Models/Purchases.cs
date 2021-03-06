using Project.Shared.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Models
{
    public class Purchases
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public double Total { get; set; }

        [Required]
        [Display(Name = "Customer")]
        [SelectValidation(SelectName = "Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
