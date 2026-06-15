namespace Basket.API.Basket.DeleteBasket;
public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var command = new DeleteBasketCommand(userName);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        }).WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Delete Basket")
        .WithDescription("Deletes the shopping basket for the user");
    }
}
