﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

internal class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantRepository,
    IDishesRepository dishesRepository) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting dishes for restaurant with id: {restaurantId}", request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if(restaurant == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }


        var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);


        return results;
    }
}
