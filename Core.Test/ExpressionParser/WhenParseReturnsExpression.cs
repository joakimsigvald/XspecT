namespace XspecT.Test.ExpressionParser;

public class WhenParseReturnsExpression : Spec<string>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("A<MyModel>", "a MyModel")]
    [InlineData("() => A<MyModel>()", "a MyModel")]
    [InlineData("() => A<MyModel>(_ => _.Name = A<string>())", "a MyModel { Name = a string }")]
    public void ThenReturnDescription(string returnsExpr, string expected)
        => When(_ => returnsExpr.ParseReturnsExpression())
        .Then().Result.Is(expected);
}