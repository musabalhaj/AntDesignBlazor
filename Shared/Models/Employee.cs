using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }
        public Department Department { get; set; }
    }
}
