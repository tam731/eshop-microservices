namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        }).WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get a product by its ID")
        .WithDescription("Retrieves a product from the catalog using its unique identifier (ID).");
    }
}
