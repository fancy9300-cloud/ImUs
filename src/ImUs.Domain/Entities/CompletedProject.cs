using System;
using System.Collections.Generic;
using ImUs.Domain.Enums;

namespace ImUs.Domain.Entities;

public class CompletedProject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Pricing Strategy (Commercial approach)
    public PricingTier PricingTier { get; set; }
    public decimal MinPrice { get; set; }  // Hidden until login
    public decimal MaxPrice { get; set; }  // Hidden until login
    public decimal EstimatedROI { get; set; } // ALWAYS visible
    public int ROIPaybackMonths { get; set; }
    
    // Timeline
    public int DurationWeeks { get; set; }
    
    // Tags for Smart Search
    public List<string> ObjectTags { get; set; } = new(); // e.g., ["frigo", "refrigerazione"]
    public List<string> CategoryTags { get; set; } = new(); // e.g., ["IoT", "Predictive"]
    
    // Social Proof
    public string AnonymizedClientName { get; set; } = string.Empty; // e.g., "Grande Catena GDO"
    public int StoreCount { get; set; }
    
    // AI & Template Links
    public bool IsCloneable { get; set; }
    public Guid? TemplateId { get; set; }
}
