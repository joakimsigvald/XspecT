using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenNullableDateTime : Spec<DateTime?>
{
    [Fact] 
    public void IsSame() 
        => When(A).Then().Result.Is(The<DateTime?>())
        .And.NotNull();

    [Fact]
    public void IsSameNotNullable()
        => When(A).Then().Result.Is(The<DateTime?>().Value)
        .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);

    [Fact] public void IsNull() => When(_ => (DateTime?)null).Then().Result.Is().Null().And.Null();
}