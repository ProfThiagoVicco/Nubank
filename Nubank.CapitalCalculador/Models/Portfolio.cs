namespace Nubank.CapitalCalculador.Models;

public class Portfolio
{
    public int Quantity { get; private set; }
    public decimal AveragePrice { get; private set; }
    public decimal AccumulatedLoss { get; private set; }

    public void Buy(decimal unitCost, int quantity)
    {
        var totalCost = AveragePrice * Quantity + unitCost * quantity;
        Quantity += quantity;
        AveragePrice = totalCost / Quantity;
    }

    public TaxResult Sell(decimal unitCost, int quantity)
    {
        decimal totalSale = unitCost * quantity;
        decimal totalCost = AveragePrice * quantity;
        decimal profit = totalSale - totalCost;

        Quantity -= quantity;

        if (profit < 0)
        {
            AccumulatedLoss += -profit;
            return new TaxResult(0.0m);
        }

        if (totalSale <= 20000m)
        {
            AccumulatedLoss -= Math.Min(AccumulatedLoss, profit);
            return new TaxResult(0.0m);
        }

        decimal taxableProfit = Math.Max(0, profit - AccumulatedLoss);
        AccumulatedLoss = Math.Max(0, AccumulatedLoss - profit);
        decimal tax = Math.Round(taxableProfit * 0.2m, 2);

        return new TaxResult(tax);
    }
}