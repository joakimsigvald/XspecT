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

public class WhenTapMockThatReturnsValueAsync : Spec<MyValueIntService, string>
{
    private const string _retVal = "abc";
    private int _tappedValue = 0;

    [Fact]
    public void ThenTappedValueIsSet()
    {
        When(_ => _.GetValueAsync(A<MyValueInt>()))
            .Given<IMyValueIntRepo>()
            .That(_ => _.GetAsync(The<MyValueInt>()))
            .Tap((int value) => _tappedValue = value)
            .Returns(() => _retVal)
            .Then();
        _tappedValue.Is(The<MyValueInt>());
    }

    [Fact]
    public void ThenCanReturnTappedValueAsync()
        => When(_ => _.GetValueAsync(A<MyValueInt>()))
            .Given<IMyValueIntRepo>()
        .That(_ => _.GetAsync(The<MyValueInt>()))
        .Returns<int>(i => $"{2 * i}")
        .And<IMyValueIntRepo>().That(_ => _.Get(The<MyValueInt>()))
        .Returns<int>(i => $"{3 * i}")
        .Then().Result.Is($"{2 * The<MyValueInt>()}");

    [Fact]
    public void ThenCanReturnTappedValue()
        => When(_ => _.GetValue(A<MyValueInt>()))
            .Given<IMyValueIntRepo>()
        .That(_ => _.GetAsync(The<MyValueInt>()))
        .Returns<int>(i => $"{2 * i}")
        .And<IMyValueIntRepo>().That(_ => _.Get(The<MyValueInt>()))
        .Returns<int>(i => $"{3 * i}")
        .Then().Result.Is($"{3 * The<MyValueInt>()}");
}

public class WhenTapMockThatReturnsValue : Spec<MyValueIntService, string>
{
    private const string _retVal = "abc";
    private int _tappedValue = 0;

    public WhenTapMockThatReturnsValue()
        => When(_ => _.GetValue(A<MyValueInt>()))
        .Given<IMyValueIntRepo>()
        .That(_ => _.Get(The<MyValueInt>()))
        .Tap((int value) => _tappedValue = value)
        .Returns(() => _retVal);

    [Fact]
    public void ThenTappedValueIsSet()
    {
        Then();
        _tappedValue.Is(The<MyValueInt>());
    }
}
public class WhenTapMockWithTwoArguments : Spec<MyValueIntService, string>
{
    private const string _retVal = "abc";
    private int _tappedValue = 0;

    public WhenTapMockWithTwoArguments()
        => When(_ => _.GetValue2(A<MyValueInt>(), ASecond<MyValueInt>()))
        .Given<IMyValueIntRepo>()
        .That(_ => _.Get2(The<MyValueInt>(), TheSecond<MyValueInt>()))
        .Tap((int v1, int v2) => _tappedValue = v1 + v2)
        .Returns(() => _retVal);

    [Fact]
    public void ThenTappedValueIsSet()
    {
        Then();
        _tappedValue.Is(The<MyValueInt>()+ TheSecond<MyValueInt>());
    }
}