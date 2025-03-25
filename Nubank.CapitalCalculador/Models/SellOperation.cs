namespace Nubank.CapitalCalculador.Models;

public class SellOperation(decimal unitCost, int quantity) : TradeOperation(unitCost, quantity)
{
    public override TaxResult Apply(Portfolio portfolio)
    {
        return portfolio.Sell(UnitCost, Quantity);
    }
}