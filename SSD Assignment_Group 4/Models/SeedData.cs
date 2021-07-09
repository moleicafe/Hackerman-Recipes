using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSD_Assignment_Group_4.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

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

        public static void SeedUsers_Roles(UserManager<RecipeUser> _userManager,RoleManager<ApplicationRole> _roleManager)
        {
            SeedRoles(_roleManager);
            SeedUsers(_userManager);
        }

        public static void SeedUsers(UserManager<RecipeUser> _userManager)
        {
            if (_userManager.FindByNameAsync("user1").Result == null)
            {
                RecipeUser user = new RecipeUser();
                user.UserName = "user1";
                user.Email = "user1@gmail.com";
                user.Name = "Nancy Davolio";
                user.BirthDate = new DateTime(1960, 2, 7);

                IdentityResult result = _userManager.CreateAsync
                (user, "Password123").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user,
                                        "Users").Wait();
                }
            }


            if (_userManager.FindByNameAsync("admin1").Result == null)
            {
                RecipeUser user = new RecipeUser();
                user.UserName = "admin1";
                user.Email = "admin1@gmail.com";
                user.Name = "Mark Smith";
                user.BirthDate = new DateTime(1965, 12, 6);

                IdentityResult result = _userManager.CreateAsync
                (user, "P@ssword123").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> _roleManager)
        {
            if (!_roleManager.RoleExistsAsync("Users").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Users";
                role.Description = "Perform normal operations.";
                IdentityResult roleResult = _roleManager.
                CreateAsync(role).Result;
            }


            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Admin";
                role.Description = "Perform all the operations.";
                IdentityResult roleResult = _roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}