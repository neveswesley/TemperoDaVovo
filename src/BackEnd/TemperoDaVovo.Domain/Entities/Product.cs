using System.ComponentModel.DataAnnotations.Schema;
using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Domain.Entities;

public class Product : BaseEntity
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public virtual ICollection<ProductSideDishGroup> ProductSideDishGroups { get; set; }
    public bool IsPaused { get; set; }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new BusinessException([" O preço deve ser maior que zero."]);
        
        Price = newPrice;
    }
    
    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new BusinessException([ "O nome não pode ser vazio." ]);
        
        Name = newName;
    }

    public void UpdateDescription(string? newDescription)
    {
        Description = newDescription?.Trim() ?? string.Empty;
    }
}