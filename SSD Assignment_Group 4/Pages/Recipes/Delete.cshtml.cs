using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment_Group_4.Data;
using SSD_Assignment_Group_4.Models;
using Microsoft.AspNetCore.Authorization;

namespace SSD_Assignment_Group_4.Pages.Recipes
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public DeleteModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipe.FindAsync(id);

            if (Recipe != null)
            {
                _context.Recipe.Remove(Recipe);

                // Once a record is deleted, create an audit record
                if (await _context.SaveChangesAsync() > 0)
                {
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Delete Recipe";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.KeyRecipeFieldID = Recipe.ID;
                    var userID = User.Identity.Name.ToString();
                    auditrecord.Username = userID;
                    _context.AuditRecords.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToPage("./Index");
        }
    }
}
