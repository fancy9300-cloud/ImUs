using System.Collections.Generic;
using System.Threading.Tasks;
using ImUs.Application.Services;
using ImUs.Application.DTOs;
using ImUs.Domain.Enums;
using System;

namespace ImUs.Infrastructure.Services;

public class SmartSearchService : ISmartSearchService
{
    public SearchMode DetermineSearchMode(string query)
    {
        // Simple logic: short query without complex verbs -> Object
        if (query.Length < 15 && !query.Contains("voglio", StringComparison.OrdinalIgnoreCase))
            return SearchMode.ObjectBased;
        
        return SearchMode.NaturalLanguage;
    }

    public Task<List<CompletedProjectDTO>> SearchByObjectAsync(string query)
    {
        // Mock Data for "frigo" or "refrigerazione"
        var results = new List<CompletedProjectDTO>();

        if (query.Contains("frigo") || query.Contains("refrig"))
        {
            results.Add(new CompletedProjectDTO
            {
                Title = "Fresh Food Quality Monitoring",
                ClientName = "Conad (3 stores)",
                EstimatedROI = 45000,
                ROIPaybackMonths = 4,
                Timeline = "3 settimane",
                PricingBadge = "€€€"
            });
            
            results.Add(new CompletedProjectDTO
            {
                Title = "Predictive Maintenance Frigoriferi",
                ClientName = "Carrefour (8 stores)",
                EstimatedROI = 60000,
                ROIPaybackMonths = 6,
                Timeline = "4 settimane",
                PricingBadge = "€€"
            });
        }
        
        return Task.FromResult(results);
    }

    public Task<AISearchResult> SearchWithAIAsync(string query)
    {
        // Mock AI Response
        return Task.FromResult(new AISearchResult
        {
            Explanation = "Based on your request, we recommend a custom solution using vibration sensors.",
            SuggestedHardware = new List<string> { "Sensirion SHT85", "Monnit Vibration Sensor" },
            RecommendedTemplate = "Predictive Maintenance IoT",
            EstimatedCostMin = 2500,
            EstimatedCostMax = 4000
        });
    }
}
