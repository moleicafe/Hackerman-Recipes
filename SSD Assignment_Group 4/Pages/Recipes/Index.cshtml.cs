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

namespace SSD_Assignment_Group_4.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public IndexModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Cuisine { get; set; }
        [BindProperty(SupportsGet = true)]
        public string RecipeCuisine { get; set; }
        //public async Task OnGetAsync()
        //{
        //    var recipes = from r in _context.Recipe
        //                 select r;
        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        recipes = recipes.Where(s => s.Title.Contains(SearchString));
        //    }

        //    Recipe = await recipes.ToListAsync();
        //}
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
