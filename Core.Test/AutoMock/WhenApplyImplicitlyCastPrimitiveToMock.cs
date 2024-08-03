using static XspecT.Test.Helper;

using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenApplyImplicitlyCastPrimitiveToMock : Spec<MyValueIntService, object>
{
    public WhenApplyImplicitlyCastPrimitiveToMock() => When(_ => _.SetValue(A<MyValueInt>()));

    [Fact]
    public void Then_ItIsApplied()
    {
        Then<IMyValueIntRepo>(_ => _.Set(The<MyValueInt>()));
        VerifyDescription(
@"When SetValue(a MyValueInt),
 then IMyValueIntRepo.Set(the MyValueInt)");
    }
}