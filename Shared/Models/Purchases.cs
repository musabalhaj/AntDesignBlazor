﻿using Project.Shared.CustomValidation;
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
        [MinLength(2)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Required]
        [MinLength(2)]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Quantaty { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Category")]
        [SelectValidation(SelectName = "Cateogry")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
