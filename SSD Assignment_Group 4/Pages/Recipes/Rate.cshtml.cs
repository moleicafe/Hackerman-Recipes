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
        public RatingCommentViewModel RatingCommentViewModel { get; set; }
        [BindProperty]
        public RecipeComment RecipeComment { get; set; }


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

            RatingCommentViewModel vm = new RatingCommentViewModel();
            vm.RecipesId = id.Value;
            vm.Title = Recipe.Title;
            vm.ListOfComments = _context.RecipeComments.Where(d => d.RecipesID.Equals(id.Value)).ToList();

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
                vm.Rating = (int)Math.Truncate(rating);
            }
            else
            {
                vm.Rating = 0;
            }
            _context.RatingCommentViews.Add(vm);


            //Create Recipe Comments
            RecipeComment recipeComment = new RecipeComment();

            recipeComment.RecipesID = id.Value;
            recipeComment.Recipes = Recipe;
            recipeComment.PublishedDate = DateTime.Now;

            _context.RecipeComments.Add(recipeComment);

            await _context.SaveChangesAsync();

            return Page();
        }

    }
}
