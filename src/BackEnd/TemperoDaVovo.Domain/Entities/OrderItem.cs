namespace TemperoDaVovo.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }

    // snapshot do produto
    public Guid? OriginalProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public string? Observation { get; private set; }
    public decimal TotalPrice { get; private set; }

    public List<OrderItemSideDish> SideDishes { get; private set; } = new();


    protected OrderItem() { }

    public OrderItem(
        Guid orderId,
        Guid? originalProductId,
        string productName,
        decimal unitPrice,
        int quantity,
        string? observation
    )
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        OriginalProductId = originalProductId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Observation = observation;

        Recalculate();
    }

    public void Update(int quantity, string? observation, List<OrderItemSideDish> sideDishes)
    {
        Quantity    = quantity;
        Observation = observation;

        SideDishes.Clear();
        foreach (var sd in sideDishes)
            SideDishes.Add(sd);

        Recalculate();
    }

    public void AddSideDish(OrderItemSideDish sideDish)
    {
        SideDishes.Add(sideDish);
        Recalculate();
    }

    public void RemoveSideDish(Guid sideDishId)
    {
        var sideDish = SideDishes.FirstOrDefault(x => x.Id == sideDishId);
        if (sideDish is null) return;

        SideDishes.Remove(sideDish);
        Recalculate();
    }

    public void Recalculate()
    {
        var sideDishTotal = SideDishes.Sum(x => x.TotalPrice);
        TotalPrice = (UnitPrice * Quantity) + sideDishTotal;
    }
}