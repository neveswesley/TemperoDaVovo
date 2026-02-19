namespace TemperoDaVovo.Communications.Responses;

public class ProductSideDishGroupResponseJson
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid SideDishGroupId { get; set; }
    public bool IsRequired { get; set; }
    public SideDishGroupResponseJson SideDishGroup { get; set; } = null!;
}