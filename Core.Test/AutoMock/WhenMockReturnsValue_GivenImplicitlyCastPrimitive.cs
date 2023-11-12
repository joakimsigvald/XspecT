using XspecT.Assert;
using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenMockReturnsValue_GivenImplicitlyCastPrimitive : SubjectSpec<MyValueIntService, string>
{
    private const string _retVal = "abc";

    public WhenMockReturnsValue_GivenImplicitlyCastPrimitive()
        => When(_ => _.GetValue(A<MyValueInt>()))
        .Given<IMyValueIntRepo>().That(_ => _.Get(The<MyValueInt>())).Returns(() => _retVal);

    [Fact] public void Then_ItReturnsExpectedValue() => Result.Is(_retVal);
}