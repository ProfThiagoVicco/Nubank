namespace Nubank.CapitalCalculador.Models;

public abstract class TradeOperation(decimal unitCost, int quantity)
{
    public decimal UnitCost { get; } = unitCost;
    public int Quantity { get; } = quantity;

    public abstract TaxResult Apply(Portfolio portfolio);

    public static TradeOperation FromDto(string type, decimal cost, int qty)
    {
        return type == "buy"
            ? new BuyOperation(cost, qty)
            : new SellOperation(cost, qty);
    }
}