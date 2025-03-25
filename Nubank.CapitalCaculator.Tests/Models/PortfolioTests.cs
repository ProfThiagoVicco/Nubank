using Nubank.CapitalCalculador.Models;

namespace Nubank.CapitalCaculator.Tests.Models;

public class PortfolioTests
{
    [Fact]
    public void Buy_ShouldUpdateWeightedAverageCorrectly()
    {
        var portfolio = new Portfolio();

        portfolio.Buy(10.00m, 100);
        portfolio.Buy(20.00m, 100);

        Assert.Equal(200, portfolio.Quantity);
        Assert.Equal(15.00m, portfolio.AveragePrice);
    }

    [Fact]
    public void Sell_WithProfitAndTotalAbove20k_ShouldApplyTax()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 1000);

        var result = portfolio.Sell(30.00m, 1000);

        Assert.Equal(4000.00m, result.Tax);
    }

    [Fact]
    public void Sell_WithProfitAndTotalBelow20k_ShouldBeTaxFree()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 100);

        var result = portfolio.Sell(15.00m, 100);
        Assert.Equal(0.00m, result.Tax);
    }

    [Fact]
    public void Sell_WithLoss_ShouldAccumulateForFutureSales()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 1000);

        var loss = portfolio.Sell(5.00m, 500);
        var profit = portfolio.Sell(20.00m, 250);

        Assert.Equal(0.00m, loss.Tax);
        Assert.Equal(0.00m, profit.Tax);
    }

}