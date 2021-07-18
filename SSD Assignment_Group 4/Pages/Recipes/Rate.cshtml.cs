using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment_Group_4.Data;
using SSD_Assignment_Group_4.Models;

namespace SSD_Assignment_Group_4.Pages.Recipes
{
    public class RateModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public RateModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _context = context;
        }

        public Recipe Recipe { get; set; }

        [BindProperty]
        public RecipeComment RecipeComment { get; set; }

        [BindProperty]
        public List<RecipeComment> ListofComments { get; set; }

        public int TotalRatings { get; set; }

        public string Comments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipe.FirstOrDefaultAsync(m => m.ID == id);

            if (Recipe == null)
            {
                return NotFound();
            }

            var listofcomments = from m in _context.RecipeComments.Where(d => d.RecipesID.Equals(id.Value))
                                 select m;
            
            ListofComments = await listofcomments.ToListAsync();
            

            var ratings = _context.RecipeComments.Where(d => d.RecipesID.Equals(id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingsum = 0;
                foreach(var r in ratings)
                {
                    ratingsum += 0;
                }
                var ratingCount = ratings.Count;
                decimal rating = 0;
                if (ratingCount > 0)
                {
                    rating = ratingsum / ratingCount;
                }
                TotalRatings = (int)Math.Truncate(rating);
            }
            else
            {
                TotalRatings = 0;
            }


            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Create Recipe Comments
            RecipeComment recipeComment = new RecipeComment();

            recipeComment.RecipesID = id.Value;
            recipeComment.Recipes = Recipe;
            recipeComment.PublishedDate = DateTime.Now;

            _context.RecipeComments.Add(recipeComment);

            await _context.SaveChangesAsync();



            return RedirectToPage("./Index");
        }
    }
}
