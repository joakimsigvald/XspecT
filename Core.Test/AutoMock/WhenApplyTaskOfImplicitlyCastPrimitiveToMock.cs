using XspecT.Fixture;
using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenApplyTaskOfImplicitlyCastPrimitiveToMock : SubjectSpec<MyValueIntService, object>
{
    public WhenApplyTaskOfImplicitlyCastPrimitiveToMock() 
        => When(_ => _.SetValueAsync(A<MyValueInt>()));
    [Fact] public void Then_ItIsApplied() 
        => Then<IMyValueIntRepo>(_ => _.SetAsync(The<MyValueInt>()));
}