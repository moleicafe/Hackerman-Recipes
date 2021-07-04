using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SSD_Assignment_Group_4.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
        public string Cuisine { get; set; }
        public string Ingredients { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}
