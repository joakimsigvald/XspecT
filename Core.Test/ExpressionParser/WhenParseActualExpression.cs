namespace XspecT.Test.ExpressionParser;

public class WhenParseActualExpression : Spec<string>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("Then().Result.Name", "Result.Name")]
    [InlineData("And(Result).Id", "Result.Id")]
    public void ThenReturnDescription(string returnsExpr, string expected)
    {
        var actual = returnsExpr.ParseActual();
        Xunit.Assert.Equal(expected, actual);
    }
}