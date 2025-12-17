using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenArray : Spec<int[]>
{
    [Fact]
    public void IsSameAs()
    {
        var values1 = new[] { 1, 2 };
        var values2 = values1;
        values1.Is(values2);
        Specification.Is("Values1 is values2");
    }

    [Fact]
    public void IsEqualButNotSame()
    {
        var values1 = new[] { 1, 2 };
        var values2 = values1.ToArray();
        values1.Is().Not(values2).But.EqualTo(values2);
        Specification.Is(
            """
            Values1 is not values2
                but equal to values2
            """);
    }

    [Fact]
    public void IsNotEqual()
    {
        var values1 = new[] { 1, 2 };
        var values2 = new[] { 1, 2, 3 };
        values1.Is().Not().EqualTo(values2);
        Specification.Is("Values1 is not equal to values2");
    }

    [Fact]
    public void IsEmpty()
    {
        var values = Array.Empty<string>();
        values.Is().Empty();
        Specification.Is("Values is empty");
    }

    [Fact]
    public void IsNotEmpty()
    {
        var values = new[] { 1, 2 };
        values.Is().Not().Empty();
        Specification.Is("Values is not empty");
    }

    [Fact]
    public void IsNotEmptyAndHasOneItem()
    {
        var values = new[] { 1 };
        values.Is().Not().Empty().And.Has().OneItem(it => it == 1);
        Specification.Is(
            """
            Values is not empty
                and has one item it = 1
            """);
    }
}