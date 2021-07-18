using SSD_Assignment_Group_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSD_Assignment_Group_4.Models
{
    public class RatingCommentViewModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public List<RecipeComment> ListOfComments { get; set; }

        public int RecipesId { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

    }
}
