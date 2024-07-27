namespace XspecT.Test.ReturnsExpressionParser;

public class WhenParseReturnsExpression : Spec<string>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("A<MyModel>", "a MyModel")]
    [InlineData("() => A<MyModel>()", "a MyModel")]
    [InlineData("() => A<MyModel>(_ => _.Name = A<string>())", "a MyModel { Name = a string }")]
    public void ThenReturnDescription(string returnsExpr, string expected)
    {
        var actual = returnsExpr.ParseReturnsExpression();
        Xunit.Assert.Equal(expected, actual);
    }
}