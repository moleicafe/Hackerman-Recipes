using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSD_Assignment_Group_4.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter valid Title.")]
        public string Title { get; set; }
        public string Author { get; set; }
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter valid Cuisine.")]
        [Required]
        [StringLength(30)]
        public string Cuisine { get; set; }
        [RegularExpression("^[A-Za-z0-9 ,_-]*$", ErrorMessage = "Please enter valid ingredients.")]
        public string Ingredients { get; set; }
        [Display(Name = "Upload Date")]
        public string ReleaseDate { get; set; }
            
        [Range(0,5)]
        public int Rating { get; set; }

    }
}
