using SynecticsSalesAnalytics.Models;
using FluentAssertions;

namespace SynecticsSalesAnalytics.UnitTests;

public class QueriesUnitTests
{
    [Fact]
    public void QueryAverageByYear_ToString_ReturnExpected()
    {
        var sut = new QueryAverageByYear() { Year = 2021, AverageSale = 10.01d };

        var result = sut.ToString();

        result.Should().Be(" Year: 2021, Average Sale: 10,01");
    }

    [Fact]
    public void QueryStandardDeviationByYear_ToString_ReturnExpected()
    {
        var sut = new QueryStandardDeviationByYear() { Year = 2021, StandardDeviation = 0.21d };

        var result = sut.ToString();

        result.Should().Be(" Year: 2021, Standard Deviation: 0,21");
    }
}