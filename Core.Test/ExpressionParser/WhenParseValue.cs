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
    [InlineData("(_, i) => _.Name = $\"X{i + 1}\"", "Name = \"X{i + 1}\"")]
    [InlineData("(a, b) => a.Name = $\"X{b + 1}\"", "Name = \"X{b + 1}\"")]
    [InlineData("Three<MyModel>().Last()", "three MyModel's Last()")]
    [InlineData("new MyComponent(An<IMyLogger>(), An<int>())", "new MyComponent(an IMyLogger, an int)")]
    [InlineData("A<MyValue<int>>()", "a MyValue<int>")]
    [InlineData("A<(int, string, int, float)>", "a (int, string, int, float)")]
    [InlineData("i => $\"{2 * i}\"", "\"{2 * i}\"")]
    [InlineData("_ => _ with { Name = A<string>() }", "Name = a string")]
    [InlineData("_ => _ with { Name = A<string>(), Id = 1 }", "Name = a string, Id = 1")]
    public void ThenReturnDescription(string valueExpr, string expected)
        => When(_ => valueExpr.ParseValue())
        .Then().Result.Is(expected);
}