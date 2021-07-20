using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SSD_Assignment_Group_4.Models
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter valid Name.")]
        public override string Name { get; set; }
        [RegularExpression("^[A-Za-z0-9 ,_-]*$", ErrorMessage = "Please enter valid Description.")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
    }

}
