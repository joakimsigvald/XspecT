using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenNullableTimeSpan : StaticSpec<TimeSpan?>
{
    [Fact] 
    public void IsSame() 
        => When(_ => _, A<TimeSpan?>()).Then().Result.Is(The<TimeSpan?>())
        .And.NotNull();

    [Fact]
    public void IsSameNotNullable()
        => When(_ => _, A<TimeSpan?>()).Then().Result.Is(The<TimeSpan?>().Value)
        .And.CloseTo(The<TimeSpan?>().Value, TimeSpan.Zero);

    [Fact] public void IsNull() => When(_ => _, (TimeSpan?)null).Then().Result.Is().Null();
}