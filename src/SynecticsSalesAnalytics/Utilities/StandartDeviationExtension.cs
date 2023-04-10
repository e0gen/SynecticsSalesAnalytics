namespace SynecticsSalesAnalytics.Utilities;

public static class StandartDeviationExtension
{
    public static double StandartDeviation(this IEnumerable<double> values)
    {
        double result = 0;
        int count = values.Count();
        if (count > 1)
        {
            double avg = values.Average();
            double sum = values.Sum(d => (d - avg) * (d - avg));
            result = Math.Sqrt(sum / count);
        }
        return result;
    }
}
