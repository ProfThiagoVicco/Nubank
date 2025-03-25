using System.Text.Json;
using Nubank.CapitalCalculador.Models;

namespace Nubank.CapitalCalculador;

class Program
{
    static void Main()
    {
        string? line;
        while ((line = Console.ReadLine()) != null && line.Trim() != "")
        {
            var dtoList = JsonSerializer.Deserialize<List<OperationDto>>(line);
            var operations = dtoList!.Select(dto => dto.ToDomain()).ToList();

            var portfolio = new Portfolio();
            var results = operations.Select(op => op.Apply(portfolio)).ToList<TaxResult>();

            Console.WriteLine(JsonSerializer.Serialize(results));
        }
    }
}