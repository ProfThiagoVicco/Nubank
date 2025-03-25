using System.IO;
using Nubank.CapitalCalculador;

namespace Nubank.CapitalCaculator.Tests;

public class IntegrationTests
{
    [Theory]
    [InlineData(
        "[{\"operation\":\"buy\",\"unit-cost\":10.00,\"quantity\":100},{\"operation\":\"sell\",\"unit-cost\":15.00,\"quantity\":50},{\"operation\":\"sell\",\"unit-cost\":15.00,\"quantity\":50}]",
        "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}]"
    )]
    [InlineData(
        "[{\"operation\":\"buy\",\"unit-cost\":10.00,\"quantity\":10000},{\"operation\":\"sell\",\"unit-cost\":20.00,\"quantity\":5000},{\"operation\":\"sell\",\"unit-cost\":5.00,\"quantity\":5000}]",
        "[{\"tax\":0.0},{\"tax\":10000.0},{\"tax\":0.0}]"
    )]
    public void Run_ShouldProduceExpectedTaxOutput(string inputJson, string expectedOutputJson)
    {
        // Arrange
        using var input = new StringReader(inputJson + "\n");
        using var output = new StringWriter();

        // Act
        Program.Run(input, output);
        var actualOutput = output.ToString().Trim();

        // Assert
        Assert.Equal(expectedOutputJson, actualOutput);
    }
}