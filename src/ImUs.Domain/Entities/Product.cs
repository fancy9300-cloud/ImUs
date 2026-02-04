namespace ImUs.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int ZoneId { get; set; }
    public Zone Zone { get; set; } = null!;
}
