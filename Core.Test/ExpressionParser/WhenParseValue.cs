namespace XspecT.Test.ExpressionParser;

public class WhenParseValue : Spec<string>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("A<MyModel>", "a MyModel")]
    [InlineData("() => A<MyModel>()", "a MyModel")]
    [InlineData("() => A<MyModel>(_ => _.Name = A<string>())", "a MyModel { Name = a string }")]
    [InlineData("One(_theModel)", "one _theModel")]
    [InlineData("(_, i) => _.Name = $\"X{i + 1}\"", "Name = X{i + 1}")]
    [InlineData("(a, b) => a.Name = $\"X{b + 1}\"", "Name = X{b + 1}")]
    [InlineData("Three<MyModel>().Last()", "Last() of three MyModel")]
    public void ThenReturnDescription(string returnsExpr, string expected)
        => When(_ => returnsExpr.ParseValue())
        .Then().Result.Is(expected);
}