using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenNullableDateTime : Spec<DateTime?>
{
    [Fact]
    public void IsSame()
    {
        When(_ => A(_)).Then().Result.Is(The<DateTime?>())
            .And.NotNull();
        Specification.Is(
            """
            When a _
            Then Result is the DateTime?
                and not null
            """);
    }

    [Fact]
    public void IsSameNotNullable()
    {
        When(_ => A(_)).Then().Result.Is(The<DateTime?>().Value)
            .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);
        Specification.Is(
            """
            When a _
            Then Result is the DateTime?'s Value
                and close to the DateTime?'s Value
            """);
    }

    [Fact]
    public void IsNull()
    {
        When(_ => (DateTime?)null).Then().Result.Is().Null().And.Null();
        Specification.Is(
            """
            When (DateTime?)null
            Then Result is null
                and null
            """);
    }
}