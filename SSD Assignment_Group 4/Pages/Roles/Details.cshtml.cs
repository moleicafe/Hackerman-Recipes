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
    [Authorize(Roles = "Admin, Role Manager")]
    public class DetailsModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DetailsModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

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
    }

}
