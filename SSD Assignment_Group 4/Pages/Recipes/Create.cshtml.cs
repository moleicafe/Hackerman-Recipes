using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSD_Assignment_Group_4.Data;
using SSD_Assignment_Group_4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;



namespace SSD_Assignment_Group_4.Pages.Recipes
{
    [Authorize(Roles = "Admin, Users")]
    public class CreateModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;
        private readonly UserManager<RecipeUser> _userManager;

        public CreateModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context, UserManager<RecipeUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            //throw new Exception("Test Error");
            return Page();


        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RecipeUser applicationUser = await _userManager.GetUserAsync(User);
            Recipe.Author = applicationUser?.UserName;
            Recipe.ReleaseDate = DateTime.Now.ToString("M/d/yyyy");
            _context.Recipe.Add(Recipe);

            // Once a record is added, create an audit record
            if (await _context.SaveChangesAsync() > 0)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Added New Recipe";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.KeyRecipeFieldID = Recipe.ID;
                // Get current logged-in user
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;

                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }


            return RedirectToPage("./Index");
        }
    }
}