using SynecticsSalesAnalytics.Contracts;
using SynecticsSalesAnalytics.Models;
using SynecticsSalesAnalytics.Utilities;

namespace SynecticsSalesAnalytics.Service;

public class SalesAnalyticsService : ISalesAnalyticsService
{
    private ISalesDataContext _salesDataContext;
    public SalesAnalyticsService(ISalesDataContext salesDataContext)
    {
        _salesDataContext = salesDataContext;
    }

    public IEnumerable<QueryAverageByYear> CalculateAverageEarnings(int startYear, int endYear)
    {
        if (endYear < startYear)
            (startYear, endYear) = (endYear, startYear);

        var query = _salesDataContext.GetAllSales()
            .Where(x => x.Date.Year >= startYear && x.Date.Year <= endYear)
            .GroupBy(g => g.Date.Year, r => r.Price)
            .Select(g => new QueryAverageByYear
            {
                Year = g.Key,
                AverageSale = g.Average()
            })
            .UnionBy(Enumerable.Range(startYear, endYear - startYear + 1)
            .Select(x => new QueryAverageByYear
            {
                Year = x,
                AverageSale = 0
            }), x => x.Year)
            .OrderBy(x => x.Year);

        return query;
    }

    public IEnumerable<QueryStandardDeviationByYear> CalculateStandartDeviationOfEarnings(int startYear, int endYear)
    {
        if (endYear < startYear)
            (startYear, endYear) = (endYear, startYear);

        var query = _salesDataContext.GetAllSales()
            .Where(x => x.Date.Year >= startYear && x.Date.Year <= endYear)
            .GroupBy(g => g.Date.Year, r => r.Price)
            .Select(g => new QueryStandardDeviationByYear
            {
                Year = g.Key,
                StandardDeviation = g.StandartDeviation()
            })
            .UnionBy(Enumerable.Range(startYear, endYear - startYear + 1)
            .Select(x => new QueryStandardDeviationByYear
            {
                Year = x,
                StandardDeviation = 0
            }), x => x.Year)
            .OrderBy(x => x.Year);

        return query;
    }
}