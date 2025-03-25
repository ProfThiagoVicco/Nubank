namespace Nubank.CapitalCalculador.Models;

public class BuyOperation(decimal unitCost, int quantity) : TradeOperation(unitCost, quantity)
{
    public override TaxResult Apply(Portfolio portfolio)
    {
        portfolio.Buy(UnitCost, Quantity);
        return new TaxResult(0.0m);
    }
}