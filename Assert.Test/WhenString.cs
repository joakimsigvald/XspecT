using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenString : StaticSpec<string>
{
    [Fact] public void IsNot() => "A".Is().Not("B");
    [Fact] public void IsEmpty() => "".Is().Empty();
    [Fact] public void IsNotEmpty() => "x".Is().NotEmpty();
    [Fact] public void DoesContainSubstring() => "abcd".Does().Contain("abc");
    [Fact] public void DoesContainAndContainSubstrings() => "abcd".Does().Contain("abc").And.Contain("bcd");
    [Fact] public void DoesNotContainOtherString() => "abcd".Does().NotContain("xxx");
    [Fact] public void IsNotNullAndContainSubstring() => "abcd".Is().NotNull().And.Contain("abc");
    [Fact] public void IsNotNullAndNotContainOtherString() => "abcd".Is().NotNull().And.NotContain("xxx");
    [Fact] public void IsNotNullAndNot() => "A".Is().NotNull().And.Not("B");

    [Fact]
    public void DoesContainOtherStringFails()
        => Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => "abcd".Does().Contain("xxx"));

    [Fact]
    public void DoesNotContainSubstringFails()
        => Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => "abcd".Does().Contain("xxx"));
}