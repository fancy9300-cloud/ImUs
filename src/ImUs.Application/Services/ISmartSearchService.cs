using System.Collections.Generic;
using System.Threading.Tasks;
using ImUs.Application.DTOs;

namespace ImUs.Application.Services;

public enum SearchMode
{
    ObjectBased,
    NaturalLanguage
}

public interface ISmartSearchService
{
    SearchMode DetermineSearchMode(string query);
    Task<List<CompletedProjectDTO>> SearchByObjectAsync(string query);
    Task<AISearchResult> SearchWithAIAsync(string query);
}
