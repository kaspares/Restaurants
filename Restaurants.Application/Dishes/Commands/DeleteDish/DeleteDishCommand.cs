using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishCommand : IRequest
{
    public DeleteDishCommand(int restaurantId )
    {
        RestaurantId = restaurantId;
    }

    public int RestaurantId { get; }
}
