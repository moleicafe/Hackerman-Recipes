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
        public RecipeUser RecipeUser { get; set; }

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
            
            
            var ratingCount = ListofComments.Count();
            if (ratingCount > 0)
            {
                var ratingsum = 0;
                foreach (var r in ListofComments)
                {
                    ratingsum += r.Rating;
                }

                decimal rating = ratingsum / ratingCount;
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
            Recipe = await _context.Recipe.FirstOrDefaultAsync(m => m.ID == id);

            RecipeUser = await _context.RecipeUser.FirstOrDefaultAsync(n => n.UserName == User.Identity.Name);
            bool admin = User.IsInRole("Admin");
            bool user = User.IsInRole("Users");

            if ((Recipe.Author == RecipeUser.UserName) && ((admin == false) || (user == false)))
            {
                
                return Redirect("~/Identity/Account/AccessDeniedRate");
            }

            //Create Recipe Comments
            RecipeComment recipeComment = new RecipeComment();

            recipeComment.RecipesID = id.Value;
            recipeComment.Recipes = Recipe;
            recipeComment.PublishedDate = DateTime.Now;
            recipeComment.Rating = int.Parse(Request.Form["Rating"]);
            recipeComment.Comments = Request.Form["Comment"].ToString();

            _context.RecipeComments.Add(recipeComment);


            if (await _context.SaveChangesAsync() > 0)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                var recipeName = Recipe.Title;
                auditrecord.AuditActionType = "Rated Recipe: " + recipeName + " - " + recipeComment.Rating + " stars";
                auditrecord.DateTimeStamp = DateTime.Now;
                // Get current logged-in user
                var userName = User.Identity.Name.ToString();
                auditrecord.Username = userName;

                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }



            return RedirectToPage("./Index");
        }
    }
}
