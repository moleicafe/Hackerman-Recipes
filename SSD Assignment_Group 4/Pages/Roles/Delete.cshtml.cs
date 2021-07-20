using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using SSD_Assignment_Group_4.Models;
using Microsoft.AspNetCore.Authorization;

namespace SSD_Assignment_Group_4.Pages.Roles
{
    [Authorize(Roles = "Role Manager")]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public DeleteModel(RoleManager<ApplicationRole> roleManager, SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public ApplicationRole ApplicationRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationRole = await _roleManager.FindByIdAsync(id);

            if (ApplicationRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationRole = await _roleManager.FindByIdAsync(id);
            IdentityResult roleResult = await _roleManager.DeleteAsync(ApplicationRole);

            // Create an auditrecord object
            var auditrecord = new AuditRecord();
            var roleName = ApplicationRole.Name;
            auditrecord.AuditActionType = "Deleted Role: " + roleName;
            auditrecord.DateTimeStamp = DateTime.Now;
            // Get current logged-in user
            var userName = User.Identity.Name.ToString();
            auditrecord.Username = userName;

            _context.AuditRecords.Add(auditrecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }

    }
}
