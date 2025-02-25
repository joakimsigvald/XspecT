using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenObject : Spec<object>
{
    internal record MyModel(string Value, int Id = 1) { }
    internal record MyArrayModel(params int[] Values) { }

    [Fact]
    public void IsSame()
    {
        The<MyModel>().Is(The<MyModel>());
        Specification.Is("The MyModel is the MyModel");
    }

    [Fact]
    public void IsNot()
    {
        new MyModel("A").Is().Not(new MyModel("A"));
        Specification.Is("""New MyModel("A") is not new MyModel("A")""");
    }

    [Fact]
    public void IsEqual()
    {
        new MyModel("AbC").Is().EqualTo(new MyModel("AbC"));
        Specification.Is("""New MyModel("AbC") is equal to new MyModel("AbC")""");
    }

    [Fact]
    public void IsLike()
    {
        new MyArrayModel(1, 2).Is().NotEqualTo(new MyArrayModel(1, 2))
            .But.Like(new MyArrayModel(1, 2)).And.Like(new MyArrayModel(2, 1));
        Specification.Is(
            """
            New MyArrayModel(1, 2) is not equal to new MyArrayModel(1, 2)
                but like new MyArrayModel(1, 2)
                and like new MyArrayModel(2, 1)
            """);
    }

    [Fact]
    public void IsEquivalentTo()
    {
        new MyArrayModel(1, 2).Is().EquivalentTo(new MyArrayModel(2, 1));
        Specification.Is("""New MyArrayModel(1, 2) is equivalent to new MyArrayModel(2, 1)""");
    }

    [Fact]
    public void IsNotLike()
    {
        new MyArrayModel(1, 2).Is().NotLike(new MyArrayModel(1, 2, 1));
        Specification.Is("""New MyArrayModel(1, 2) is not like new MyArrayModel(1, 2, 1)""");
    }

    [Fact]
    public void IsNotEquivalentTo()
    {
        new MyArrayModel(1, 2).Is().NotEquivalentTo(new MyArrayModel(1, 2, 1));
        Specification.Is(
            """
            New MyArrayModel(1, 2) is not equivalent to new MyArrayModel(1, 2, 1)
            """);
    }

    [Fact]
    public void Match()
    {
        new MyArrayModel(1, 2).Satisfies(_ => _.Values.Length == 2);
        Specification.Is("""New MyArrayModel(1, 2) satisfies _.Values.Length == 2""");
    }

    [Fact]
    public void Match2()
    {
        new MyModel(AFirst<string>(), An<int>()).Satisfies(it => it.Value == TheFirst<string>() && it.Id == An<int>());
        Specification.Is(
            """
            New MyModel(a first string, an int) satisfies it.Value == TheFirst<string>() &&
                  it.Id == An<int>()
            """);
    }
}