namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile);
public record UpdateProductResponse(bool IsSuccess);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest("ID in the URL does not match ID in the request body.");
            }
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            var response = new UpdateProductResponse(result.IsSuccess);
            return result.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Updates an existing product")
        .WithDescription("Updates an existing product with the provided details. The product is identified by its unique ID.");
    }
}
