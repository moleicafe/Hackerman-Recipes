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
        public string Title { get; set; }
      
        public string Author { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string Cuisine { get; set; }
        public string Ingredients { get; set; }
        [Display(Name = "Upload Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
       
        public int Rating { get; set; }
    }
}
