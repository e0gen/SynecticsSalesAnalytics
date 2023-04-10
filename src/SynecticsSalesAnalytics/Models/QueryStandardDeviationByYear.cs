namespace SynecticsSalesAnalytics.Models;

public class QueryStandardDeviationByYear
{
    public int Year { get; set; }
    public double StandardDeviation { get; set; }

    public override string ToString()
    {
        return $" Year: {Year}, Standard Deviation: {StandardDeviation.ToString("0.00")}";
    }
}
