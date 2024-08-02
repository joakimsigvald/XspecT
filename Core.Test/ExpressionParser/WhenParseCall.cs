namespace XspecT.Test.ExpressionParser;

public class WhenParseCall : Spec<string>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("_ => _.Get(The<int>())", "Get(the int)")]
    [InlineData("Get(The<int>())", "Get(the int)")]
    public void ThenReturnDescription(string callExpr, string expected)
        => When(_ => callExpr.ParseCall())
        .Then().Result.Is(expected);
}