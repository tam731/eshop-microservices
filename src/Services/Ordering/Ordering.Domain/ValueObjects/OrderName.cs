namespace Ordering.Domain.ValueObjects;

public class OrderName
{
    private const int DefaultLength = 5;
    public string Value { get; }
    private OrderName(string value) => Value = value;
    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        ArgumentOutOfRangeException.ThrowIfLessThan(value.Length, DefaultLength, nameof(value));

        return new OrderName(value);
    }
}
