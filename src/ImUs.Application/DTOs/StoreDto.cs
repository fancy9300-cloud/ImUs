namespace ImUs.Application.DTOs;

public class StoreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public int DeviceCount { get; set; }
    public int ZoneCount { get; set; }
}
