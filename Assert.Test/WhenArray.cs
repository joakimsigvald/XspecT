using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenArray : Spec<object, int[]>
{
    [Fact]
    public void IsSameAs()
    {
        var values1 = new[] { 1, 2 };
        var values2 = values1;
        values1.Is(values2);
    }

    [Fact]
    public void IsEqualButNotSame()
    {
        var values1 = new[] { 1, 2 };
        var values2 = values1.ToArray();
        values1.Is().Not(values2).But.EqualTo(values2);
    }

    [Fact]
    public void IsNotEqual()
    {
        var values1 = new[] { 1, 2 };
        var values2 = new[] { 1, 2, 3 };
        values1.Is().NotEqualTo(values2);
    }

    [Fact]
    public void IsEmpty()
    {
        var values = Array.Empty<string>();
        values.Is().Empty();
    }

    [Fact]
    public void IsNotEmpty()
    {
        var values = new[] { 1, 2 };
        values.Is().NotEmpty();
    }

    [Fact]
    public void IsNotEmptyAndSingle()
    {
        var values = new[] { 1 };
        values.Is().NotEmpty().And.Single().Is(1);
    }
}