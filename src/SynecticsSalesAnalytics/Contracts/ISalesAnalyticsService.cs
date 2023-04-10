using SynecticsSalesAnalytics.Models;

namespace SynecticsSalesAnalytics.Contracts
{
    public interface ISalesAnalyticsService
    {
        IEnumerable<QueryAverageByYear> CalculateAverageEarnings(int startYear, int endYear);
        IEnumerable<QueryStandardDeviationByYear> CalculateStandartDeviationOfEarnings(int startYear, int endYear);
    }
}