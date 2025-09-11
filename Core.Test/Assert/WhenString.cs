using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenString : Spec<string>
{
    [Fact]
    public void IsNot()
    {
        "A".Is().Not("B");
        Specification.Is(
            """
            "A" is not "B"
            """);
    }

    [Fact]
    public void IsEmpty()
    {
        "".Is().Empty();
        Specification.Is(
            """
            "" is empty
            """);
    }

    [Fact]
    public void IsNotEmpty()
    {
        "x".Is().Not().Empty();
        Specification.Is(
            """
            "x" is not empty
            """);
    }

    [Fact]
    public void DoesContainSubstring()
    {
        "abcd".Does().Contain("abc");
        Specification.Is(
            """
            "abcd" contains "abc"
            """);
    }

    [Fact]
    public void DoesContainAndContainSubstrings()
    {
        "abcd".Does().Contain("abc").And.Contain("bcd");
        Specification.Is(
            """
            "abcd" contains "abc"
                and contains "bcd"
            """);
    }

    [Fact]
    public void DoesContainAndIsNotNull()
    {
        "abcd".Does().Contain("abc").And.Is().Not().Null();
        Specification.Is(
            """
            "abcd" contains "abc"
                and is not null
            """);
    }

    [Fact]
    public void DoesNotContainOtherString()
    {
        "abcd".Does().Not().Contain("xxx");
        Specification.Is(
            """
            "abcd" does not contain "xxx"
            """);
    }

    [Fact]
    public void IsNotNullAndContainSubstring()
    {
        "abcd".Is().Not().Null().And.Does().Contain("abc");
        Specification.Is(
            """
            "abcd" is not null
                and contains "abc"
            """);
    }

    [Fact]
    public void IsNotNullAndIs_Abc()
    {
        "abc".Is().Not().Null().But.Is("abc");
        Specification.Is(
            """
            "abc" is not null
                but is "abc"
            """);
    }

    [Fact]
    public void IsNotNullAndStartWithSubstring()
    {
        "abcd".Is().Not().Null().And.Does().StartWith("abc");
        Specification.Is(
            """
            "abcd" is not null
                and starts with "abc"
            """);
    }

    [Fact]
    public void IsNotNullAndEndWithSubstring()
    {
        "abcd".Is().Not().Null().And.Does().EndWith("bcd");
        Specification.Is(
            """
            "abcd" is not null
                and ends with "bcd"
            """);
    }

    [Fact]
    public void IsNotNullAndNotContainOtherString()
    {
        "abcd".Is().Not().Null().And.Does().Not().Contain("xxx");
        Specification.Is(
            """
            "abcd" is not null
                and does not contain "xxx"
            """);
    }

    [Fact]
    public void IsNotNullAndNot()
    {
        "A".Is().Not().Null().And.Not("B");
        Specification.Is(
            """
            "A" is not null
                and not "B"
            """);
    }

    [Fact]
    public void IsEqual()
    {
        "AbC".Is("AbC");
        Specification.Is(
            """
            "AbC" is "AbC"
            """);
    }

    [Fact]
    public void IsLike()
    {
        "AbC".Is().Like("aBc").And.Like("AbC");
        Specification.Is(
            """
            "AbC" is like "aBc"
                and like "AbC"
            """);
    }

    [Fact]
    public void IsEquivalentTo()
    {
        "AbC".Is().EquivalentTo("aBc").And.EquivalentTo("AbC");
        Specification.Is(
            """
            "AbC" is equivalent to "aBc"
                and equivalent to "AbC"
            """);
    }

    [Fact]
    public void IsNotLike()
    {
        "AbC".Is().Not().Like("bac");
        Specification.Is(
            """
            "AbC" is not like "bac"
            """);
    }

    [Fact]
    public void IsNotEquivalentTo()
    {
        "AbC".Is().Not().EquivalentTo("bac");
        Specification.Is(
            """
            "AbC" is not equivalent to "bac"
            """);
    }

    [Fact]
    public void DoesStartWith()
    {
        "abcd".Does().StartWith("ab");
        Specification.Is(
            """
            "abcd" starts with "ab"
            """);
    }

    [Fact]
    public void DoesEndWith()
    {
        "abcd".Does().EndWith("cd");
        Specification.Is(
            """
            "abcd" ends with "cd"
            """);
    }

    [Fact]
    public void DoesContainOtherStringFails()
        => Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => "abcd".Does().Contain("xxx"));

    [Fact]
    public void DoesNotContainSubstringFails()
        => Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => "abcd".Does().Contain("xxx"));

    [Fact]
    public void WhenOneOf()
    {
        "x".Is().OneOf(["x", "y", "z"]);
        Specification.Is("""
            "x" is one of ["x", "y", "z"]
            """);
    }
}