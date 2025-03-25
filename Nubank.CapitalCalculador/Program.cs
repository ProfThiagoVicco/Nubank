using System.Text.Json;
using Nubank.CapitalCalculador.Models;

namespace Nubank.CapitalCalculador;

public class Program
{
    public static void Main() => Run(Console.In, Console.Out);

    public static void Run(TextReader input, TextWriter output)
    {
        string? line;
        while ((line = input.ReadLine()) != null && line.Trim() != "")
        {
            var dtoList = JsonSerializer.Deserialize<List<OperationDto>>(line);
            var operations = dtoList!.Select(dto => dto.ToDomain()).ToList();

            var portfolio = new Portfolio();
            var results = operations.Select(op => op.Apply(portfolio)).ToList();

            output.WriteLine(JsonSerializer.Serialize(
                results.Select(r => new { tax = r.Tax })
            ));
        }
    }
}