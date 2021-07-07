using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SSD_Assignment_Group_4.Models
{
    public class RecipeUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}