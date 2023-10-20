using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Verification;

public class WhenNullableTimeSpan : StaticSpec<TimeSpan?>
{
    [Fact] 
    public void IsSame() 
        => Given(A<TimeSpan?>()).When(_ => _).Then().Result.Is(The<TimeSpan?>())
        .And.NotNull();

    [Fact]
    public void IsSameNotNullable()
        => Given(A<TimeSpan?>()).When(_ => _).Then().Result.Is(The<TimeSpan?>().Value)
        .And.CloseTo(The<TimeSpan?>().Value, TimeSpan.Zero);

    [Fact] public void IsNull() => Given((TimeSpan?)null).When(_ => _).Then().Result.Is().Null();
}