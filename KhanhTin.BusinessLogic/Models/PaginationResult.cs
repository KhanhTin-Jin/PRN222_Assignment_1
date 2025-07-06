using System.Collections.Generic;

namespace KhanhTin.BusinessLogic.Models
{
    public class PaginationResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public PaginationResult()
        {
        }

        public PaginationResult(List<T> items, int totalItems, int currentPage, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            HasPreviousPage = currentPage > 1;
            HasNextPage = currentPage < TotalPages;
            StartIndex = (currentPage - 1) * pageSize + 1;
            EndIndex = Math.Min(currentPage * pageSize, totalItems);
        }

        public static PaginationResult<T> Create(List<T> items, int totalItems, int currentPage, int pageSize)
        {
            return new PaginationResult<T>(items, totalItems, currentPage, pageSize);
        }

        public static PaginationResult<T> Empty(int currentPage = 1, int pageSize = 10)
        {
            return new PaginationResult<T>(new List<T>(), 0, currentPage, pageSize);
        }
    }
}
