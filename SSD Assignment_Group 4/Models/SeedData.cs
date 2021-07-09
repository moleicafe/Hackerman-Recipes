using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSD_Assignment_Group_4.Data;
using SSD_Assignment_Group_4.Models;
using SSD_Assignment_Group_4.Migrations;
namespace SSD_Assignment_Group_4.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SSD_Assignment_Group_4Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SSD_Assignment_Group_4Context>>()))
            {
                // Look for any Recipes.
                if (context.Recipe.Any())
                {
                    return;   // DB has been seeded
                }

                context.Recipe.AddRange(
                    new Recipe
                    {
                        ID = 1,
                        Title = "Best Brownies",
                        Author = "Molei",
                        Cuisine = "American",
                        Ingredients = "Butter, Sugar, Eggs, Vanilla Extract, Cocoa Powder, All-purpose flour, Salt, Baking powder",
                        ReleaseDate = DateTime.Parse("2021-7-4"),
                        Rating = 5
                    },

                    new Recipe
                    {
                        ID = 2,
                        Title = "Classic Mac and Cheese",
                        Author = "Molei",
                        Cuisine = "Italian",
                        Ingredients = "Macaroni, Butter, Flour, Milk, Cheddar cheese, Swiss cheese, Bread crumbs",
                        ReleaseDate = DateTime.Parse("2021-7-4"),
                        Rating = 5
                    },

                    new Recipe
                    {
                        ID = 3,
                        Title = "Cream of Mushroom Soup",
                        Author = "Molei",
                        Cuisine = "American",
                        Ingredients = "Mushrooms, Butter, Green onions, Garlic, Thyme, Flour, Vegetable broth, Light cream, Salt, Pepper, Chives",
                        ReleaseDate = DateTime.Parse("2021-7-4"),
                        Rating = 5
                    }
                );
                context.RecipeUser.AddRange(
                    new RecipeUser
                    {
                        UserName = "admin1",
                        Email = "admin1@gmail.com",

                    });
                context.SaveChanges();
            }
        }
    }
}