using XspecT.Assert;
using XspecT.Test.AutoMock;

namespace XspecT.Test.ClassFixture;

public class MyClassFixture<TResult> : Spec<MyService, TResult>
{
    private readonly MyComponent _component;

    public MyClassFixture()
        => Given(_component = new MyComponent(An<IMyLogger>(), A(1)))
        .After(_ => _component.SetValue(_component.GetValue() + 1))
        .Before(_ => _component.SetValue(0));
}

public class WhenHasClassFixture(MyClassFixture<int> fixture) 
    : Spec<MyService, int>(fixture), IClassFixture<MyClassFixture<int>>
{
    [Fact]
    public void ThenSetupIsRunOnceBeforeFirstTest()
    {
        When(_ => _.GetValue()).Then().Result.Is(The<int>() + 1);
        Specification.Is(
            """
            Given _component = new MyComponent(an IMyLogger, a 1)
            When _.GetValue()
            After _component.SetValue(_component.GetValue() + 1)
            Before _component.SetValue(0)
            Then Result is the int + 1
            """);
    }

    [Fact]
    public void ThenSetupIsRunOnceBeforeSecondTest()
    {
        When(_ => _.GetValue()).Then().Result.Is(The<int>() + 1).And.Not(3);
        Specification.Is(
            """
            Given _component = new MyComponent(an IMyLogger, a 1)
            When _.GetValue()
            After _component.SetValue(_component.GetValue() + 1)
            Before _component.SetValue(0)
            Then Result is the int + 1
                and not 3
            """);
    }
}