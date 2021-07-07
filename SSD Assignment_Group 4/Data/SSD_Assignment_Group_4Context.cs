﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment_Group_4.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace SSD_Assignment_Group_4.Data
{
    public class SSD_Assignment_Group_4Context : IdentityDbContext<RecipeUser>
    {
        public SSD_Assignment_Group_4Context (DbContextOptions<SSD_Assignment_Group_4Context> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<SSD_Assignment_Group_4.Models.Recipe> Recipe { get; set; }
    }
}
