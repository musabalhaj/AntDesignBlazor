using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        [Display(Name ="Department Name")]
        public string DepartmentName { get; set; }

    }
}
