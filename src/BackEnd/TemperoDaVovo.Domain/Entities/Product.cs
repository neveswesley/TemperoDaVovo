using System.ComponentModel.DataAnnotations.Schema;
using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Domain.Entities;

public class Product : BaseEntity
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(10,2)")]

    public decimal Price { get; set; }
    public string? ImageUrl { get; set; } = string.Empty;
    
    // FK de category
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new BusinessException([" O preço deve ser maior que zero."]);
        
        Price = newPrice;
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new BusinessException([ "A descrição não pode ser vazia." ]);
    }

    public void DeactivateProduct()
    {
        IsActive = false;
    }

    public void ActiveProduct()
    {
        IsActive = true;
    }
}