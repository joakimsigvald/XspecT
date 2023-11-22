using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenNullableDateTime : StaticSpec<DateTime?>
{
    [Fact] 
    public void IsSame() 
        => Given(A<DateTime?>()).When(_ => _).Then().Result.Is(The<DateTime?>())
        .And.NotNull();

    [Fact]
    public void IsSameNotNullable()
        => Given(A<DateTime?>()).When(_ => _).Then().Result.Is(The<DateTime?>().Value)
        .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);

    [Fact] public void IsNull() => Given((DateTime?)null).When(_ => _).Then().Result.Is().Null().And.Null();
}