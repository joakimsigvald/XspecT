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
        When(_ => (DateTime?)null).Then().Result.Is().Null();
        Specification.Is(
            """
            When (DateTime?)null
            Then Result is null
            """);
    }

    [Fact]
    public void IsBeforeEtc()
    {
        Given((DateTime?)DateTime.Now).When(_ => A(_));
        Result.Is().Before(The<DateTime?>().Value.AddDays(1)).And.After(The<DateTime?>().Value.AddDays(-1));
        Result.Is().NotBefore(The<DateTime?>().Value);
        Result.Is().NotAfter(The<DateTime?>().Value);
        Specification.Is(
            """
            Given (DateTime?)DateTime.Now
            When a _
            Then Result is before the DateTime?'s Value.AddDays(1)
                and after the DateTime?'s Value.AddDays(-1)
            Result is not before the DateTime?'s Value
            Result is not after the DateTime?'s Value
            """);
    }
}