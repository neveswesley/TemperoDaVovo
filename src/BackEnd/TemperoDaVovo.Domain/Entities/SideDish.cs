namespace TemperoDaVovo.Domain.Entities;

public class SideDish : BaseEntity
{
    public Guid SideDishGroupId { get; set; }
    public SideDishGroup SideDishGroup { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}