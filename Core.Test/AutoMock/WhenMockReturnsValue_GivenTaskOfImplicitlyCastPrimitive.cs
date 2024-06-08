using XspecT.Assert;
using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenMockReturnsValue_GivenTaskOfImplicitlyCastPrimitive : Spec<MyValueIntService, string>
{
    private const string _retVal = "abc";

    public WhenMockReturnsValue_GivenTaskOfImplicitlyCastPrimitive()
        => When(_ => _.GetValueAsync(A<MyValueInt>()))
        .Given<IMyValueIntRepo>().That(_ => _.GetAsync(The<MyValueInt>())).Returns(() => _retVal);

    [Fact] public void Then_ItReturnsExpectedValue() => Result.Is(_retVal);
}