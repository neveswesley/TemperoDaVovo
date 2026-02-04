namespace TemperoDaVovo.Domain.Entities;

public class ProductSideDishGroup
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid SideDishGroupId { get; set; }
    
    // Navigation Properties
    public Product Product { get; set; }
    public SideDishGroup SideDishGroup { get; set; }
    public bool IsRequired { get; set; }
    
    // Construtor
    public ProductSideDishGroup() { }
    
    public ProductSideDishGroup(Guid productId, Guid sideDishGroupId)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        SideDishGroupId = sideDishGroupId;
    }
}