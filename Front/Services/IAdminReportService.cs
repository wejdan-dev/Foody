using Front.ViewModels.Admin;

namespace Front.Services
{
    public interface IAdminReportService
    {
        Task<decimal> GetTotalRevenueAsync();
        Task<int> GetTotalOrdersAsync();
        Task<List<MonthlySalesViewModel>> GetMonthlySalesAsync(int monthsBack = 6);
        Task<List<TopProductViewModel>> GetTopProductsAsync(int count = 5);
    }
}
