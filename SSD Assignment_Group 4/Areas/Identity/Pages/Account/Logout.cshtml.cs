using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SSD_Assignment_Group_4.Models;

namespace SSD_Assignment_Group_4.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<RecipeUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public LogoutModel(SignInManager<RecipeUser> signInManager, ILogger<LogoutModel> logger, SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Successful Logout";
                auditrecord.DateTimeStamp = DateTime.Now;
                // Get current logged-in user
                var userName = User.Identity.Name.ToString();
                auditrecord.Username = userName;

                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();

                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
