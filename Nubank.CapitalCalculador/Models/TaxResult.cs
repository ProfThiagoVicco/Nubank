namespace Nubank.CapitalCalculador.Models;

public class TaxResult(decimal value)
{
    public decimal Tax { get; } = Math.Round(value, 2);

    public static readonly TaxResult NoTax = new(0m);

    public bool HasTax => Tax > 0;

    public override string ToString() => $"{{\"tax\": {Tax}}}";
}