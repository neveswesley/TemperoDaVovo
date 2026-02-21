using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Domain.Entities;

public class Order : BaseEntity
{
    public Guid RestaurantId { get; private set; }
    
    // Customer info
    public string ClientSessionId { get; private set; } = string.Empty;
    public string CustomerName { get; private set; } = string.Empty;
    public string CustomerPhone { get; private set; } = string.Empty;
    
    public OrderStatus Status { get; private set; }

    // Price
    public decimal SubTotal { get; private set; }
    public decimal Total { get; private set; }

    public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();
   
    protected Order() {}
   
    public Order(Guid restaurantId, string clientSessionId, string customerName, string customerPhone)
    {
        Id = Guid.NewGuid();
        RestaurantId = restaurantId;
        ClientSessionId = clientSessionId;
        CustomerName = customerName;
        CustomerPhone = customerPhone;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.PendingConfirmation;
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
    }

    public void CalculateTotals()
    {
        SubTotal = Items.Sum(i => i.TotalPrice);
        Total = SubTotal;
    }

}