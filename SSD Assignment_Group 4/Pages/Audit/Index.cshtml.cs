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
using System.ComponentModel.DataAnnotations;


namespace SSD_Assignment_Group_4.Pages.Audit
{
    public class IndexModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public IndexModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _context = context;
        }

        public IList<AuditRecord> AuditRecord { get;set; }

        [BindProperty(SupportsGet = true)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Please only enter alphanumeric characters.")]
        public string SearchStringAudit { get; set; }

        public SelectList Username { get; set; }
        [BindProperty(SupportsGet = true)]
        public string AuditUsername { get; set; }
        public string SQLmessageAudit { get; set; }


        public async Task OnGetAsync(string SearchStringAudit)
        {
            SQLmessageAudit = "Select * From AuditRecords Where AuditActionType Like '%" + SearchStringAudit + "%'";
            AuditRecord = await _context.AuditRecords.FromSqlRaw(SQLmessageAudit).ToListAsync();
            TempData["message"] = "Entered SQL :" + SQLmessageAudit;

            IQueryable<string> auditQuery = from m in _context.AuditRecords
                                              orderby m.Username
                                              select m.Username;

            var auditRecords = from m in _context.AuditRecords
                          select m;

            if (!string.IsNullOrEmpty(SearchStringAudit))
            {
                auditRecords = auditRecords.Where(s => s.AuditActionType.Contains(SearchStringAudit));
            }


            if (!string.IsNullOrEmpty(AuditUsername))
            {
                auditRecords = auditRecords.Where(x => x.Username == AuditUsername);
            }
            Username = new SelectList(await auditQuery.Distinct().ToListAsync());
            AuditRecord = await auditRecords.ToListAsync();
        }

    }
}
