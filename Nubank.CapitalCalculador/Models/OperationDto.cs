namespace Nubank.CapitalCalculador.Models;

using System.Text.Json.Serialization;

public class OperationDto
{
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = string.Empty;

    [JsonPropertyName("unit-cost")]
    public decimal UnitCost { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    public TradeOperation ToDomain()
    {
        return TradeOperation.FromDto(Operation, UnitCost, Quantity);
    }
}
