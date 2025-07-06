using KhanhTin.BusinessLogic.DTOs;

namespace KhanhTin.BusinessLogic.Interfaces
{
    public interface IReportService
    {
        ReportResultDto GenerateReport(ReportRequestDto request);
        DashboardDto GetDashboardData();
    }
}
