namespace TemperoDaVovo.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    
    // snapshot do produto
    public Guid? OriginalProductId { get; set; }
    public string ProductName { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public string? Observation { get; private set; }

    public decimal TotalPrice { get; private set; }
    public ICollection<OrderItemSideDish> SideDishes { get; private set; } = new List<OrderItemSideDish>();
    
    protected OrderItem() {}

    public OrderItem(Guid? originalProductId, string productName, decimal unitPrice, int quantity, string? observation)
    {
        Id = Guid.NewGuid();
        OriginalProductId = originalProductId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Observation = observation;

        Recalculate();
    }

    public void Recalculate()
    {
        var sideDishTotal = SideDishes.Sum(x => x.TotalPrice);
        TotalPrice = (UnitPrice * Quantity) + sideDishTotal;
    }

    public void AddSideDish(OrderItemSideDish sideDish)
    {
        SideDishes.Add(sideDish);
    }
    
}