using SynecticsSalesAnalytics.Models;
using FluentAssertions;

namespace SynecticsSalesAnalytics.UnitTests;

public class SaleRecordUnitTests
{
    [Theory]
    [InlineData("09/01/2020##279.57", "##", "dd/MM/yyyy", '.')]
    [InlineData("09/01/2020##279,57", "##", "dd/MM/yyyy", ',')]
    [InlineData("09/01/2020####279.57", "####", "dd/MM/yyyy", '.')]
    [InlineData("09.01.2020##279.57", "##", "dd.MM.yyyy", '.')]
    [InlineData("09-01-2020##279.57", "##", "dd-MM-yyyy", '.')]
    public void Parse_OnValidLineAndFormats_ReturnExpected(string line, 
        string delimiter, string dateFormat, char decimalSymbol)
    {
        var rec = SaleRecord.Parse(line, delimiter, dateFormat, decimalSymbol);

        rec.Date.Should().Be(new DateOnly(2020, 01, 09));
        rec.Price.Should().Be(279.57d);
    }
}