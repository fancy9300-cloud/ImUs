using System;
using System.Collections.Generic;

namespace ImUs.Application.DTOs;

public class ObjectSearchRequest
{
    public string Query { get; set; } = string.Empty;
}

public class AISearchRequest
{
    public string Query { get; set; } = string.Empty;
}

public class CompletedProjectDTO
{
    public string Title { get; set; } = string.Empty;
    public decimal EstimatedROI { get; set; }
    public int ROIPaybackMonths { get; set; }
    public string Timeline { get; set; } = string.Empty;
    public string PricingBadge { get; set; } = string.Empty; // "€", "€€", "€€€"
    public string ClientName { get; set; } = string.Empty;
}

public class AISearchResult
{
    public List<string> SuggestedHardware { get; set; } = new();
    public string RecommendedTemplate { get; set; } = string.Empty;
    public decimal EstimatedCostMin { get; set; }
    public decimal EstimatedCostMax { get; set; }
    public string Explanation { get; set; } = string.Empty;
}
