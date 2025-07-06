using System.Collections.Generic;

namespace KhanhTin.BusinessLogic.Models
{
    public class SearchResult<T>
    {
        public List<T> Results { get; set; } = new List<T>();
        public int TotalResults { get; set; }
        public string SearchTerm { get; set; }
        public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();
        public TimeSpan SearchTime { get; set; }
        public List<string> Suggestions { get; set; } = new List<string>();
        public Dictionary<string, int> Facets { get; set; } = new Dictionary<string, int>();

        public bool HasResults => Results.Count > 0;
        public string SearchSummary => $"Found {TotalResults} results for '{SearchTerm}' in {SearchTime.TotalMilliseconds}ms";

        public static SearchResult<T> Create(List<T> results, int totalResults, string searchTerm, TimeSpan searchTime)
        {
            return new SearchResult<T>
            {
                Results = results,
                TotalResults = totalResults,
                SearchTerm = searchTerm,
                SearchTime = searchTime
            };
        }

        public static SearchResult<T> Empty(string searchTerm)
        {
            return new SearchResult<T>
            {
                SearchTerm = searchTerm,
                SearchTime = TimeSpan.Zero
            };
        }
    }
}
