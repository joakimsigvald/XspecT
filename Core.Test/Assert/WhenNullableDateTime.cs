using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenNullableDateTime : Spec<DateTime?>
{
    [Fact]
    public void IsSame()
        => When(_ => A(_)).Then().Result.Is(The<DateTime?>())
        .And.NotNull();

    [Fact]
    public void IsSameNotNullable()
        => When(_ => A(_)).Then().Result.Is(The<DateTime?>().Value)
        .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);

    [Fact] public void IsNull() => When(_ => (DateTime?)null).Then().Result.Is().Null().And.Null();
}