using static XspecT.Test.Helper;

using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenApplyTaskOfImplicitlyCastPrimitiveToMock : Spec<MyValueIntService, object>
{
    public WhenApplyTaskOfImplicitlyCastPrimitiveToMock() 
        => When(_ => _.SetValueAsync(A<MyValueInt>()));

    [Fact]
    public void Then_ItIsApplied()
    {
        Then<IMyValueIntRepo>(_ => _.SetAsync(The<MyValueInt>()));
        VerifyDescription(
@"When SetValueAsync(a MyValueInt)
Then IMyValueIntRepo.SetAsync(the MyValueInt)");
    }
}