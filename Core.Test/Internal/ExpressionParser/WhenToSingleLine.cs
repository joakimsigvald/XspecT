using XspecT.Assert;

namespace XspecT.Test.Internal.ExpressionParser;

public class WhenToSingleLine : Spec<string>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", "")]
    [InlineData("oneline", "oneline")]
    [InlineData("one line", "one line")]
    [InlineData("one  line", "one  line")]
    [InlineData("one\rline", "one line")]
    [InlineData("one\n line", "one line")]
    [InlineData("one \r\n line", "one line")]
    [InlineData("one(\r\nline)", "one(line)")]
    [InlineData("one[\r\nline]", "one[line]")]
    public void ThenReturnSingleLine(string str, string expected)
        => When(_ => str.ToSingleLine()).Then().Result.Is(expected);
}