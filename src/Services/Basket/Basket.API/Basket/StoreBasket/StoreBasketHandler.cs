using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string userName);
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Shopping cart is required");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}
public class StoreBasketCommandHandler(IBasketRepository repository, DiscounProtoService.DiscounProtoServiceClient discounProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        await repository.StoreBasketAsync(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        // TODO : communicate with Discount.Grpc to calculate the discount for each product in the cart
        foreach (var item in cart.Items)
        {
            var coupon = await discounProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, null, null, cancellationToken);
            item.Price -= coupon.Amount;
        }
    }

}

