namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);
public class GetBasketEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var query = new GetBasketQuery(userName);
            var result = await sender.Send(query);
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        }).WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get basket for a user")
        .WithDescription("Get the shopping cart for a specific user by their username.");
    }
}
