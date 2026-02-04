namespace ImUs.Domain.Entities;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    public ICollection<Zone> Zones { get; set; } = new List<Zone>();
    public ICollection<Device> Devices { get; set; } = new List<Device>();
}
