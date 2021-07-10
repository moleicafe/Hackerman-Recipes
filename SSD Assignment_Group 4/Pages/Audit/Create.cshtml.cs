﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSD_Assignment_Group_4.Data;
using SSD_Assignment_Group_4.Models;


namespace SSD_Assignment_Group_4.Pages.Audit
{
   
    public class CreateModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public CreateModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AuditRecord AuditRecord { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AuditRecords.Add(AuditRecord);
            await _context.SaveChangesAsync();

           
            


            return RedirectToPage("./Index");
        }
    }
}
