using XspecT.Assert;

namespace XspecT.Test.Internal.ExpressionParser;

public class WhenParseActual : Spec<string>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("Then().Result.Name", "Result.Name")]
    [InlineData("And(Result).Id", "Result.Id")]
    [InlineData("The<int>()", "the int")]
    public void ThenReturnDescription(string returnsExpr, string expected)
        => When(_ => returnsExpr.ParseActual()).Then().Result.Is(expected);
}