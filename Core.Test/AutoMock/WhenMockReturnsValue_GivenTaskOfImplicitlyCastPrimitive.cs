using XspecT.Fixture;
using XspecT.Test.Given;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenMockReturnsValue_GivenTaskOfImplicitlyCastPrimitive : SubjectSpec<MyValueIntService, string>
{
    private const string _retVal = "abc";

    public WhenMockReturnsValue_GivenTaskOfImplicitlyCastPrimitive()
        => When(_ => _.GetValueAsync(A<MyValueInt>()))
        .Given<IMyValueIntRepo>().That(_ => _.GetAsync(The<MyValueInt>())).Returns(() => _retVal);

    [Fact] public void Then_ItReturnsExpectedValue() => Result.Is(_retVal);
}