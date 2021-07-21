using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SSD_Assignment_Group_4.Models
{
    public class RecipeComment
    {

        public int Id { get; set; }

        [RegularExpression("^[A-Za-z0-9 ,.!?'#_\r\n]*$", ErrorMessage = "Special characters not allowed!")]
        public string Comments { get; set; }

        public DateTime PublishedDate { get; set; }

        public int RecipesID { get; set; }

        public Recipe Recipes { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
    }
}
