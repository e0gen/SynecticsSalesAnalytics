namespace SynecticsSalesAnalytics.Models;

public class QueryAverageByYear
{
    public int Year { get; set; }
    public double AverageSale { get; set; }

    public override string ToString()
    {
        return $" Year: {Year}, Average Sale: {AverageSale.ToString("0.00")}";
    }
}
