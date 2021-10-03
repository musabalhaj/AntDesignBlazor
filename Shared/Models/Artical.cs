using Project.Shared.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Models
{
    public class Artical
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Author { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string Image { get; set; }
        [Required]
        [SelectValidation(SelectName = "Cateogry")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
