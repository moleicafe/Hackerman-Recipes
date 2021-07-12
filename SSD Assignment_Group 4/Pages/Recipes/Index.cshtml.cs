using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment_Group_4.Data;
using SSD_Assignment_Group_4.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;


namespace SSD_Assignment_Group_4.Pages.Recipes
{
    [Authorize(Roles = "Admin, Users")]
    public class IndexModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public IndexModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; }
        [BindProperty(SupportsGet = true)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Please only enter alphanumeric characters.")]
        public string SearchString { get; set; }
        public SelectList Cuisine { get; set; }
        [BindProperty(SupportsGet = true)]
        public string RecipeCuisine { get; set; }


        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> cuisineQuery = from m in _context.Recipe
                                            orderby m.Cuisine
                                            select m.Cuisine;

            var recipes = from m in _context.Recipe
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                recipes = recipes.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(RecipeCuisine))
            {
                recipes = recipes.Where(x => x.Cuisine == RecipeCuisine);
            }
            Cuisine = new SelectList(await cuisineQuery.Distinct().ToListAsync());
            Recipe = await recipes.ToListAsync();
        }
    }
}
