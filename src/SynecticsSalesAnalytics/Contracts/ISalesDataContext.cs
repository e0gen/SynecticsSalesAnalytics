using SynecticsSalesAnalytics.Models;

namespace SynecticsSalesAnalytics.Contracts
{
    public interface ISalesDataContext
    {
        IEnumerable<SaleRecord> GetAllSales();
    }
}