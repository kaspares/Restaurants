using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Command.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italiann", "Mexican", "Japanese", "American", "Indian"];
    public CreateRestaurantCommandValidator()
    {
        // 1. property for which we defie the validation 2.
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(dto => dto.Category)
            .Must(validCategories.Contains);
           //.Custom((value, context) =>
           //{
           //    var isValidCategory = validCategories.Contains(value);
           //    if(!isValidCategory)
           //    {
           //        context.AddFailure("Category", "Invalid category. Please choose from the valid categories");
           //    }
           //});

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Insert a valid email");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code xx-xxx");
    }
}
