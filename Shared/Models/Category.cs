using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
