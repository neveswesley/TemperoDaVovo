using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Domain.Entities;

public class Product : BaseEntity
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductType Category { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new BusinessException([" O preço deve ser maior que zero."]);
        
        Price = newPrice;
    }

    void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new BusinessException([ "A descrição não pode ser vazia." ]);
    }
}