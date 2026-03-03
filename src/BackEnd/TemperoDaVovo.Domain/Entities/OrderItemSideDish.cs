namespace TemperoDaVovo.Domain.Entities;

public class OrderItemSideDish
{
    public Guid Id { get; private set; }
    public Guid? OrderItemId { get; private set; }
    public Guid? OriginalSideDishId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string GroupName { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }

    protected OrderItemSideDish()
    {
    }

    public OrderItemSideDish(
        Guid? originalSideDishId,
        string name,
        string groupName,
        decimal unitPrice,
        int quantity
    )
    {
        Id = Guid.NewGuid();
        OriginalSideDishId = originalSideDishId;
        Name = name;
        GroupName = groupName;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = unitPrice * quantity;
    }

    public static OrderItemSideDish Create(Guid orderItemId, Guid originalSideDishId, string name, string groupName,
    decimal unitPrice, int quantity)
    {
        return new OrderItemSideDish
        {
            Id = Guid.NewGuid(),
            OrderItemId = orderItemId,
            OriginalSideDishId = originalSideDishId,
            Name = name,
            GroupName = groupName,
            UnitPrice = unitPrice,
            Quantity = quantity,
            TotalPrice = unitPrice * quantity
        };
    }
}