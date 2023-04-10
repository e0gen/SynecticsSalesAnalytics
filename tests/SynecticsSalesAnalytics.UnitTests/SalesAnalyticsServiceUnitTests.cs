using SynecticsSalesAnalytics.Contracts;
using SynecticsSalesAnalytics.Models;
using SynecticsSalesAnalytics.Service;
using FluentAssertions;
using NSubstitute;

namespace SynecticsSalesAnalytics.UnitTests
{
    public class SalesAnalyticsServiceUnitTests
    {
        ISalesDataContext _stabDataContext;
        SalesAnalyticsService _sut;

        public SalesAnalyticsServiceUnitTests()
        {
            _stabDataContext = Substitute.For<ISalesDataContext>();
            _stabDataContext.GetAllSales().Returns(new SaleRecord[] {
                new SaleRecord(new DateOnly(2021, 1, 1), 1.10),
                new SaleRecord(new DateOnly(2021, 2, 1), 1.20),
                new SaleRecord(new DateOnly(2021, 3, 1), 1.30),
                new SaleRecord(new DateOnly(2021, 4, 1), 1.40),

                new SaleRecord(new DateOnly(2022, 1, 1), 1.00),
                new SaleRecord(new DateOnly(2022, 2, 1), 1.50),
                new SaleRecord(new DateOnly(2022, 3, 1), 2.00),
                new SaleRecord(new DateOnly(2022, 4, 1), 2.50)
            });
            _sut = new SalesAnalyticsService(_stabDataContext);
        }

        [Fact]
        public void CalculateAverageEarnings_ExitingPeriod_ReturnValidQueryObjects()
        {
            var results = _sut.CalculateAverageEarnings(2020, 2022).ToArray();

            results.Should().HaveCount(3);
            results.Should().ContainEquivalentOf(new QueryAverageByYear() { Year = 2020, AverageSale = 0 });
            results.Should().ContainEquivalentOf(new QueryAverageByYear() { Year = 2021, AverageSale = 1.25 });
            results.Should().ContainEquivalentOf(new QueryAverageByYear() { Year = 2022, AverageSale = 1.75 });
        }

        [Fact]
        public void CalculateAverageEarnings_SingleYear_ReturnValidQueryObject()
        {
            var results = _sut.CalculateAverageEarnings(2021, 2021).ToArray();

            results.Should().HaveCount(1);
            results.Should().ContainEquivalentOf(new QueryAverageByYear() { Year = 2021, AverageSale = 1.25 });
        }

        [Fact]
        public void CalculateAverageEarnings_NotExitingPeriod_ReturnValidQueryObjects()
        {
            var results = _sut.CalculateAverageEarnings(2018, 2019).ToArray();

            results.Should().HaveCount(2);
            results.Should().ContainEquivalentOf(new QueryAverageByYear() { Year = 2018, AverageSale = 0 });
            results.Should().ContainEquivalentOf(new QueryAverageByYear() { Year = 2019, AverageSale = 0 });
        }

        [Fact]
        public void CalculateStandartDeviationOfEarning_ExitingPeriod_ReturnValidQueryObjects()
        {
            var results = _sut.CalculateStandartDeviationOfEarnings(2020, 2022).ToArray();

            results.Should().HaveCount(3);
            results[0].Year.Should().Be(2020);
            results[0].StandardDeviation.Should().Be(0);
            results[1].Year.Should().Be(2021);
            results[1].StandardDeviation.Should().BeApproximately(0.11d, 0.01d);
            results[2].Year.Should().Be(2022);
            results[2].StandardDeviation.Should().BeApproximately(0.56d, 0.01d);
        }

        [Fact]
        public void CalculateStandartDeviationOfEarning_SingleYear_ReturnValidQueryObject()
        {
            var results = _sut.CalculateStandartDeviationOfEarnings(2021, 2021).ToArray();

            results.Should().HaveCount(1);
            results[0].Year.Should().Be(2021);
            results[0].StandardDeviation.Should().BeApproximately(0.11d, 0.01d);
        }

        [Fact]
        public void CalculateStandartDeviationOfEarning_NotExitingPeriod_ReturnValidQueryObjects()
        {
            var results = _sut.CalculateStandartDeviationOfEarnings(2018, 2019).ToArray(); ;

            results.Should().HaveCount(2);
            results.Should().ContainEquivalentOf(new QueryStandardDeviationByYear() { Year = 2018, StandardDeviation = 0 });
            results.Should().ContainEquivalentOf(new QueryStandardDeviationByYear() { Year = 2019, StandardDeviation = 0 });
        }
    }
}