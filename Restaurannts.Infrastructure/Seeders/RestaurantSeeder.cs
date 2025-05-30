﻿using Microsoft.AspNetCore.Identity;
using Restaurannts.Infrastructure.Persistence;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext,
        RoleManager<IdentityRole> roleManager) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync()) // Sprawdzamy czy mozemy się połaczyć 
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
            var roles = GetRoles();
            foreach (var role in roles)
            {
                var exists = await roleManager.RoleExistsAsync(role.Name!);
                if (!exists)
                {
                    await roleManager.CreateAsync(role);
                }
            }


        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    new (UserRoles.User) 
                    {
                        NormalizedName = UserRoles.User.ToUpper()
                    },
                    new (UserRoles.Owner)
                    {
                        NormalizedName = UserRoles.Owner.ToUpper()
                    },
                    new (UserRoles.Admin)
                    {
                        NormalizedName = UserRoles.Admin.ToUpper()
                    }
                ];
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [
              new()
              {

                  Name = "KFC",
                  Category = "Fast Food",
                  Description =
                      "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                  ContactEmail = "contact@kfc.com",
                  HasDelivery = true,
                  Dishes =
                  [
                      new ()
                      {
                          Name = "Nashville Hot Chicken",
                          Description = "Nashville Hot Chicken (10 pcs.)",
                          Price = 10.30M,
                      },

                      new ()
                      {
                          Name = "Chicken Nuggets",
                          Description = "Chicken Nuggets (5 pcs.)",
                          Price = 5.30M,
                      },
                  ],
                  Address = new ()
                  {
                      City = "London",
                      Street = "Cork St 5",
                      PostalCode = "WC2N 5DU"
                  },

              },
              new ()
              {

                  Name = "McDonald",
                  Category = "Fast Food",
                  Description =
                      "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                  ContactEmail = "contact@mcdonald.com",
                  HasDelivery = true,
                  Address = new Address()
                  {
                      City = "London",
                      Street = "Boots 193",
                      PostalCode = "W1F 8SR"
                  }
              }

            ];

            return restaurants;
        }
    }
}
