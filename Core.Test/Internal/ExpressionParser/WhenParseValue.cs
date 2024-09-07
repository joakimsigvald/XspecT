﻿using XspecT.Assert;

namespace XspecT.Test.Internal.ExpressionParser;

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
    [InlineData("new (An<IMyLogger>(), An<int>())", "new(an IMyLogger, an int)")]
    [InlineData("new(An<IMyLogger>(), An<int>())", "new(an IMyLogger, an int)")]
    [InlineData("A<MyValue<int>>()", "a MyValue<int>")]
    [InlineData("A<(int, string, int, float)>", "a (int, string, int, float)")]
    [InlineData("i => $\"{2 * i}\"", "\"{2 * i}\"")]
    [InlineData("_ => _ with { Name = A<string>() }", "Name = a string")]
    [InlineData("_ => _ with { Name = A<string>(), Id = 1 }", "Name = a string, Id = 1")]
    [InlineData("A<MyModel?>", "a MyModel?")]
    [InlineData("The<TimeSpan>() / 2", "the TimeSpan / 2")]
    [InlineData("The<int>() + TheSecond<int>()", "the int + the second int")]
    [InlineData("The<int>() + TheSecond<int>() - TheThird<int>()", "the int + the second int - the third int")]
    [InlineData("The<int>() & TheSecond<int>() & TheThird<int>()", "the int & the second int & the third int")]
    [InlineData("_ => _.Name = A<string>() + ASecond<string>()", "Name = a string + a second string")]
    [InlineData(
        """
        _ => _.Name = A<string>()
                + ASecond<string>()
        """, "Name = a string + a second string")]
    public void ThenReturnDescription(string valueExpr, string expected)
        => When(_ => valueExpr.ParseValue())
        .Then().Result.Is(expected);
}