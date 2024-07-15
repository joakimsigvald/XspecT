using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenNullableTimeSpan : Spec<TimeSpan?>
{
    [Fact]
    public void IsSame()
        => When(_ => A(_)).Then().Result.Is(The<TimeSpan?>())
        .And.NotNull();

    [Fact]
    public void IsSameNotNullable()
        => When(_ => A(_)).Then().Result.Is(The<TimeSpan?>().Value)
        .And.CloseTo(The<TimeSpan?>().Value, TimeSpan.Zero);

    [Fact] public void IsNull() => When(_ => (TimeSpan?)null).Then().Result.Is().Null();
}