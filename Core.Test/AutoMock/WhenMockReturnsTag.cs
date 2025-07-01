using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenMockReturnsTag : Spec<InterfaceService, int> 
{
    const int _123 = 123;
    static readonly Tag<int> value = new();

    public WhenMockReturnsTag() 
        => When(_ => _.GetServiceValue())
        .Given<IMyService>().That(_ => _.GetValue()).Returns(value)
        .And(value).Is(_123);

    [Fact]
    public void ThenReturnTaggedValue()
    {
        Then().Result.Is(_123);
        Specification.Is(
            """
            Given value is _123
              and IMyService.GetValue() returns value
            When _.GetServiceValue()
            Then Result is _123
            """);
    }
}