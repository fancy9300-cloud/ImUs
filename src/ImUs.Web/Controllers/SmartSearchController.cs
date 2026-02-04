using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ImUs.Application.Services;
using ImUs.Application.DTOs;

namespace ImUs.Web.Controllers;

[ApiController]
[Route("api/search")]
public class SmartSearchController : ControllerBase
{
    private readonly ISmartSearchService _searchService;

    public SmartSearchController(ISmartSearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost("object")]
    public async Task<ActionResult<List<CompletedProjectDTO>>> SearchByObject([FromBody] ObjectSearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
            return BadRequest("Query cannot be empty");

        var projects = await _searchService.SearchByObjectAsync(request.Query);
        return Ok(projects);
    }

    [HttpPost("ai")]
    public async Task<ActionResult<AISearchResult>> SearchWithAI([FromBody] AISearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
            return BadRequest("Query cannot be empty");

        // Simple routing logic to demonstrate dual-mode
        var mode = _searchService.DetermineSearchMode(request.Query);
        
        if (mode == SearchMode.ObjectBased)
        {
            // If user typed a simple keyword in the AI box, redirect to object search logic
            // providing a structured response format
            var projects = await _searchService.SearchByObjectAsync(request.Query);
            return Ok(new AISearchResult 
            { 
                Explanation = "Found matching projects for your keyword.",
                RecommendedTemplate = projects.Count > 0 ? projects[0].Title : "Standard Template" 
            });
        }

        var result = await _searchService.SearchWithAIAsync(request.Query);
        return Ok(result);
    }
}
