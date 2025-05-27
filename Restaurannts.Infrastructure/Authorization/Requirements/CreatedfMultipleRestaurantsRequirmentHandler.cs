using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedfMultipleRestaurantsRequirmentHandler(IRestaurantsRepository restaurantRepository
        ,IUserContext userContext) : AuthorizationHandler<CreatedfMultipleRestaurantsRequirment>
    {
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CreatedfMultipleRestaurantsRequirment requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            var restaurants = await restaurantRepository.GetAllAsync();

            var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser!.Id);

            if(userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

        }
    }
}
