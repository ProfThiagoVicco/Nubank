using Nubank.CapitalCalculador.Models;

namespace Nubank.CapitalCaculator.Tests.Models;

public class SellOperationTests
{
    [Fact]
    public void Apply_ShouldDelegateToPortfolioSellAndReturnTax()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 1000);

        var sell = new SellOperation(30.00m, 500); 

        var taxResult = sell.Apply(portfolio);

        Assert.Equal(2000.00m, taxResult.Tax);
        Assert.Equal(500, portfolio.Quantity); 
    }

    [Fact]
    public void Apply_WithLoss_ShouldReturnZeroTaxAndAccumulateLoss()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 1000);

        var sell = new SellOperation(5.00m, 500);

        var result = sell.Apply(portfolio);

        Assert.Equal(0.00m, result.Tax);
        Assert.Equal(500, portfolio.Quantity);
    }

    [Fact]
    public void Apply_WithTotalBelow20k_ShouldReturnZeroTax()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 100);

        var sell = new SellOperation(15.00m, 100);

        var result = sell.Apply(portfolio);

        Assert.Equal(0.00m, result.Tax);
    }

    [Fact]
    public void Apply_WithProfitAndAccumulatedLoss_ShouldDeductLossFirst()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 1000);
        portfolio.Sell(5.00m, 500);

        var sell = new SellOperation(20.00m, 500);

        var result = sell.Apply(portfolio);

        Assert.Equal(500.00m, result.Tax);
    }
}