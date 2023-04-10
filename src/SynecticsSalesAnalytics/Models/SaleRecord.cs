using System.Globalization;

namespace SynecticsSalesAnalytics.Models;

public record SaleRecord(DateOnly Date, double Price)
{
    public DateOnly Date { get; set; } = Date;
    public double Price { get; set; } = Price;

    public static SaleRecord Parse(string line, string delimiter, string dateFormat, char decimalSymbol)
    {
        var lineValues = line.Split(delimiter);
        var date = DateOnly.ParseExact(lineValues[0], dateFormat, CultureInfo.InvariantCulture);
        if (decimalSymbol != '.')
            lineValues[1] = lineValues[1].Replace(decimalSymbol, '.');
        var price = double.Parse(lineValues[1], CultureInfo.InvariantCulture);

        return new SaleRecord(date, price);
    }
}
