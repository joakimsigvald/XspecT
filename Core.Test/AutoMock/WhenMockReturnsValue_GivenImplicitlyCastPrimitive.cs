using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenMockReturnsValue_GivenImplicitlyCastPrimitive : Spec<MyValueIntService, string>
{
    private const string _retVal = "abc";

    public WhenMockReturnsValue_GivenImplicitlyCastPrimitive()
        => When(_ => _.GetValue(A<MyValueInt>()))
        .Given<IMyValueIntRepo>().That(_ => _.Get(The<MyValueInt>())).Returns(() => _retVal);

    [Fact]
    public void Then_ItReturnsExpectedValue()
    {
        Result.Is(_retVal);
        Description.Is(
            """
            Given IMyValueIntRepo.Get(the MyValueInt) returns _retVal
            When GetValue(a MyValueInt)
            Then Result is _retVal
            """);
    }
}

public class WhenMockReturnsObject : Spec<MyValueIntService, object>
{
    public WhenMockReturnsObject() => When(_ => _.GetObject());

    [Fact]
    public void Then_ReturnAnObject()
    {
        Result.GetType().Is(typeof(object));
        Description.Is(
            """
            When GetObject()
            Then Result.GetType() is typeof object
            """);
    }
}
public class WhenMockReturnsTaskOfObject : Spec<MyValueIntService, object>
{
    public WhenMockReturnsTaskOfObject() => When(_ => _.GetObjectAsync());

    [Fact]
    public void Then_ReturnAnObject()
    {
        Result.GetType().Is(typeof(object));
        Description.Is(
            """
            When GetObjectAsync()
            Then Result.GetType() is typeof object
            """);
    }
}
public class WhenMockReturnsSelf : Spec<MyValueIntService, IMyValueIntRepo>
{
    public WhenMockReturnsSelf() => When(_ => _.GetRepo());

    [Fact]
    public void Then_ReturnIt()
    {
        Result.GetObject().GetType().Is(typeof(object));
        Description.Is(
            """
            When GetRepo()
            Then Result.GetObject().GetType() is typeof object
            """);
    }
}