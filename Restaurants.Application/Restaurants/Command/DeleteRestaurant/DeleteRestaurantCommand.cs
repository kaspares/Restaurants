using MediatR;

namespace Restaurants.Application.Restaurants.Command.DeleteRestaurant;

public class DeleteRestaurantCommand : IRequest
{
    public DeleteRestaurantCommand(int id) 
    {
        Id = id;
    }

    public int Id { get; }
}
