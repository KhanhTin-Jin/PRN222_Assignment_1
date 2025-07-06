using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KhanhTinMVC.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class ConfirmDeleteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
        public string EntityType { get; set; }
        public Dictionary<string, string> AdditionalInfo { get; set; } = new Dictionary<string, string>();
    }

    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public string SearchTerm { get; set; }
        public Dictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>();

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int StartItem => (CurrentPage - 1) * PageSize + 1;
        public int EndItem => Math.Min(CurrentPage * PageSize, TotalItems);
    }

    public class BreadcrumbViewModel
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }

    public class AlertViewModel
    {
        public string Type { get; set; } // success, danger, warning, info
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Dismissible { get; set; } = true;

        public string AlertClass => $"alert alert-{Type}";
    }

    public class ModalViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Size { get; set; } = ""; // sm, lg, xl
        public bool Centered { get; set; } = false;
        public bool Scrollable { get; set; } = false;
        public bool StaticBackdrop { get; set; } = false;
    }
}
