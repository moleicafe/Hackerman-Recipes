using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment_Group_4.Models;

namespace SSD_Assignment_Group_4.Data
{
    public class SSD_Assignment_Group_4Context : DbContext
    {
        public SSD_Assignment_Group_4Context (DbContextOptions<SSD_Assignment_Group_4Context> options)
            : base(options)
        {
        }

        public DbSet<SSD_Assignment_Group_4.Models.Recipe> Recipe { get; set; }
    }
}
