using Nubank.CapitalCalculador.Models;

namespace Nubank.CapitalCaculator.Tests.Models;

public class BuyOperationTests
{
    [Fact]
    public void Apply_ShouldUpdatePortfolioQuantityAndAveragePrice()
    {
        var portfolio = new Portfolio();
        var buy = new BuyOperation(10.00m, 100);

        var result = buy.Apply(portfolio);

        Assert.Equal(0.00m, result.Tax);
        Assert.Equal(100, portfolio.Quantity);
        Assert.Equal(10.00m, portfolio.AveragePrice);
    }

    [Fact]
    public void Apply_MultipleBuys_ShouldUpdateWeightedAverageCorrectly()
    {
        var portfolio = new Portfolio();

        new BuyOperation(10.00m, 100).Apply(portfolio);
        new BuyOperation(20.00m, 100).Apply(portfolio);

        Assert.Equal(200, portfolio.Quantity);
        Assert.Equal(15.00m, portfolio.AveragePrice);
    }

    [Fact]
    public void Apply_ShouldNeverAffectLossOrTax()
    {
        var portfolio = new Portfolio();
        portfolio.Sell(5.00m, 0);

        var result = new BuyOperation(10.00m, 100).Apply(portfolio);

        Assert.Equal(0.00m, result.Tax);
        Assert.Equal(100, portfolio.Quantity);
        Assert.True(portfolio.AveragePrice > 0);
    }
}