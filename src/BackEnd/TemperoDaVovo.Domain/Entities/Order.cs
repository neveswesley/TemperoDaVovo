using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Domain.Entities;

public class Order : BaseEntity
{
    public Guid RestaurantId { get; private set; }

    // Customer info
    public int OrderNumber { get; private set; }
    public string ClientSessionId { get; private set; } = string.Empty;
    public string CustomerName { get; private set; } = string.Empty;
    public string CustomerPhone { get; private set; } = string.Empty;

    public OrderStatus Status { get; private set; }

    // Price
    public decimal? DeliveryFee { get; set; }
    public decimal SubTotal { get; private set; }
    public decimal Total { get; private set; }

    // Address
    public DeliveryMode DeliveryMode { get; set; }
    public string? Street { get; private set; }
    public string? Number { get; private set; }
    public string? Complement { get; private set; }
    public string? Reference { get; private set; }
    public Guid? NeighborhoodId { get; private set; }
    public Neighborhood? Neighborhood { get; private set; }
    public string? City { get; private set; }
    public AddressName? AddressName { get; private set; }
    
    // Payment
    public Guid? PaymentId { get; private set; }
    public Payment? Payment { get; private set; }

    public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();

    protected Order()
    {
    }

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
        Total = SubTotal + (DeliveryFee ?? 0);
    }

    private void RecalculateTotal()
    {
        Total = SubTotal + (DeliveryFee ?? 0);
    }

    public void RemoveItemAndRecalculate(Guid orderItemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == orderItemId);
        if (item != null)
            Items.Remove(item);

        CalculateTotals();
    }

    public void RemoveAllOrderItems(Guid orderId)
    {
        var item = Items.Where(i => i.OrderId == orderId).ToList();
        item.ForEach(i => Items.Remove(i));
    }

    public void CompleteCheckout(string customerName, string customerPhone)
    {
        CustomerName = customerName;
        CustomerPhone = customerPhone;
    }

    public void OrderAddress(string street, string number, string complement, string reference, Guid? neighborhoodId,
        string city, AddressName? addressName)
    {
        Street = street;
        Number = number;
        Complement = complement;
        Reference = reference;
        NeighborhoodId = neighborhoodId;
        City = city;
        AddressName = addressName;
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }

    public void SetDeliveryFee(decimal deliveryFee)
    {
        if (deliveryFee < 0)
            throw new ArgumentException("Taxa de entrega inválida.");

        DeliveryFee = deliveryFee;
        RecalculateTotal();
    }

    public void SetPayment(Guid paymentId)
    {
        PaymentId = paymentId;
    }

    public void SetOrderNumber(int number)
    {
        OrderNumber = number;
    }
    
}