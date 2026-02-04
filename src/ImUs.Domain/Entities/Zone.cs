namespace ImUs.Domain.Entities;

public class Zone
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;

    public ICollection<Device> Devices { get; set; } = new List<Device>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
