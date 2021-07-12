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

namespace SSD_Assignment_Group_4.Pages.Recipes
{
    [Authorize(Roles = "Admin, Users")]
    public class EditModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public EditModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

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
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Recipe).State = EntityState.Modified;


            try
            {
                if (await _context.SaveChangesAsync() > 0)
                {
                    // Create an auditrecord object
                    var auditrecord = new AuditRecord();
                    auditrecord.KeyRecipeFieldID = Recipe.ID;

                    var recipeName = Recipe.Title;
                    auditrecord.AuditActionType = "Edited Recipe";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    
                    // Get current logged-in user
                    var userID = User.Identity.Name.ToString();
                    auditrecord.Username = userID;

                    _context.AuditRecords.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.ID == id);
        }
    }
}
