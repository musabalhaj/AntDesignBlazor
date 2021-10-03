using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }

        // convert model into viewmodel
        // to map the viewmodel with the model.
        // bind values here insted of binded in the page.
        // in the page when you get the data from the api you will create instans
        // of the viewmodel and say  categoryviewmodel = category;.
        public static implicit operator CategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        // convert viewmodel into model
        // in the page when you pass the data from the page you will create instans
        // of the model and say  category = categoryviewmodel;.
        public static implicit operator Category(CategoryViewModel categoryViewModel)
        {
            return new Category
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };
        }
    }
}
