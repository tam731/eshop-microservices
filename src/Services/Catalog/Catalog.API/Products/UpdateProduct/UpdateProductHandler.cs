namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Category, string ImageFile) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductCommandHandler
    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand query, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Query}", query);
        
        var product = session.LoadAsync<Product>(query.Id, cancellationToken).Result;

        if(product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = query.Name;
        product.Description = query.Description;
        product.Price = query.Price;
        product.Category = query.Category;
        product.ImageFile = query.ImageFile; 

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
