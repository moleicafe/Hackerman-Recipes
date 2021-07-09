using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSD_Assignment_Group_4.Data;
using System;
using System.Linq;

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
                // Look for any recipes.
                if (context.Recipe.Any())
                {
                    return;   // DB has been seeded
                }

                context.Recipe.AddRange(
                    new Recipe
                    {
                        Title = "Best Brownies",
                        Author = "Molei",
                        Cuisine = "American",
                        Ingredients = "Butter, Sugar, Eggs, Vanilla Extract, Cocoa Powder, All-purpose flour, Salt, Baking powder",
                        ReleaseDate = DateTime.Parse("2021-7-4"),
                        Rating = 5
                    },

                    new Recipe
                    {
                        Title = "Classic Mac and Cheese",
                        Author = "Molei",
                        Cuisine = "Italian",
                        Ingredients = "Macaroni, Butter, Flour, Milk, Cheddar cheese, Swiss cheese, Bread crumbs",
                        ReleaseDate = DateTime.Parse("2021-7-4"),
                        Rating = 5
                    },

                    new Recipe
                    {
                        Title = "Cream of Mushroom Soup",
                        Author = "Molei",
                        Cuisine = "American",
                        Ingredients = "Mushrooms, Butter, Green onions, Garlic, Thyme, Flour, Vegetable broth, Light cream, Salt, Pepper, Chives",
                        ReleaseDate = DateTime.Parse("2021-7-4"),
                        Rating = 5
                    }
                );
                context.SaveChanges();
            }
        }
    }
}