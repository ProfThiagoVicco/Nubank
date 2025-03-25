namespace Nubank.CapitalCaculator.Tests;

public class PortfolioTests
{
    [Fact]
    public void Buy_ShouldUpdateWeightedAverageCorrectly()
    {
        var portfolio = new Portfolio();

        portfolio.Buy(10.00m, 100); // 100 shares at $10
        portfolio.Buy(20.00m, 100); // 100 shares at $20

        Assert.Equal(200, portfolio.Quantity);
        Assert.Equal(15.00m, portfolio.AveragePrice);
    }

    [Fact]
    public void Sell_WithProfitAndTotalAbove20k_ShouldApplyTax()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 1000); // Average price = 10

        var result = portfolio.Sell(30.00m, 1000); // Total sale = $30,000, profit = $20,000

        Assert.Equal(4000.00m, result.Value); // 20% tax over $20,000
    }

    [Fact]
    public void Sell_WithProfitAndTotalBelow20k_ShouldBeTaxFree()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 100);

        var result = portfolio.Sell(15.00m, 100); // Total = $1500

        Assert.Equal(0.00m, result.Value); // tax-exempt because total <= 20k
    }

    [Fact]
    public void Sell_WithLoss_ShouldAccumulateForFutureSales()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 1000);

        var loss = portfolio.Sell(5.00m, 500); // Loss = $2500
        var profit = portfolio.Sell(20.00m, 250); // Profit = $2500, should be tax-exempt due to previous loss

        Assert.Equal(0.00m, loss.Value);
        Assert.Equal(0.00m, profit.Value);
    }

    [Fact]
    public void Sell_WithAccumulatedLoss_ShouldApplyTaxOnlyOnRemainingProfit()
    {
        var portfolio = new Portfolio();
        portfolio.Buy(10.00m, 1000);

        portfolio.Sell(5.00m, 500); // Loss = $2500
        var result = portfolio.Sell(20.00m, 500); // Profit = $5000 → net taxable = $2500

        Assert.Equal(500.00m, result.Value); // 20% of $2500
    }
}