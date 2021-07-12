using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSD_Assignment_Group_4.Models
{
    public class RecipeUser : IdentityUser
    {
        [Display(Name = "Name")]
        [RegularExpression("^[a-zA-Z ]*$")]
        [StringLength(30)]
        public string Name { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}