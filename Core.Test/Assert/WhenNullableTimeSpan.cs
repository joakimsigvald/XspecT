using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenNullableTimeSpan : Spec<TimeSpan?>
{
    [Fact]
    public void IsSame()
    {
        When(_ => The(_)).Then().Result.Is(The<TimeSpan?>())
            .And.Not().Null();
        Specification.Is(
            """
            When the _
            Then Result is the TimeSpan?
                and not null
            """);
    }

    [Fact]
    public void IsSameNotNullable()
    {
        When(_ => The(_)).Then().Result.Is(The<TimeSpan?>().Value)
            .And.CloseTo(The<TimeSpan?>().Value, TimeSpan.Zero);
        Specification.Is(
            """
            When the _
            Then Result is the TimeSpan?'s Value
                and close to the TimeSpan?'s Value
            """);
    }

    [Fact]
    public void IsNull()
    {
        When(_ => (TimeSpan?)null).Then().Result.Is().Null();
        Specification.Is(
            """
            When (TimeSpan?)null
            Then Result is null
            """);
    }
}