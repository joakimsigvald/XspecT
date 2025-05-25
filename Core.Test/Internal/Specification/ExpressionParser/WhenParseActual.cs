using XspecT.Assert;
using XspecT.Internal.Specification;

namespace XspecT.Test.Internal.Specification.ExpressionParser;

public class WhenParseActual : Spec<string>
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("Something().That", "")]
    [InlineData("Then().Result.Name", "Result.Name")]
    [InlineData("And(Result).Id", "Result.Id")]
    [InlineData("The<int>()", "the int")]
    public void ThenReturnDescription(string returnsExpr, string expected)
        => When(_ => returnsExpr.ParseActual()).Then().Result.Is(expected);
}