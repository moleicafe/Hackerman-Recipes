using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment_Group_4.Data;
using SSD_Assignment_Group_4.Models;

namespace SSD_Assignment_Group_4.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context _context;

        public IndexModel(SSD_Assignment_Group_4.Data.SSD_Assignment_Group_4Context context)
        {
            _context = context;
        }

        public IList<RecipeComment> RecipeComment { get;set; }

        public async Task OnGetAsync()
        {
            RecipeComment = await _context.RecipeComments
                .Include(r => r.Recipes).ToListAsync();
        }
    }
}
