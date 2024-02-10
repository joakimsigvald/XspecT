using XspecT.Assert;
using XspecT.Test.Subjects.Purchase;

namespace XspecT.Test.AutoMock;

public class WhenInjectingAnInterfaceWithUsing : SubjectSpec<InterfaceService, int>
{
    public WhenInjectingAnInterfaceWithUsing()
        => Given(new MyComponent(An<IMyLogger>(), An<int>())).When(_ => _.GetValue());

    [Fact] public void ThenGetValue() => Result.Is(The<int>());
    [Fact] public void ThenInterfaceIsMocked() => Then<IMyLogger>(_ => _.LogValue(The<int>()));
}

public class WhenUseConcreteInstanceOfInterface : SubjectSpec<InterfaceService, int>
{
    public WhenUseConcreteInstanceOfInterface()
        => Given(CreateService()).And(An<int>).When(_ => _.GetServiceValue());

    [Fact] public void ThenUseIt() => Result.Is(TheSecond<int>());

    protected IMyService CreateService()
        => new MyService(new MyComponent(A<IMyLogger>(), ASecond<int>()), A<IMyLogger>());
}

public class WhenUsingConcreteInstanceForInterface : SubjectSpec<InterfaceService, int>
{
    public WhenUsingConcreteInstanceForInterface()
        => When(_ => _.GetValue()).Given(() => new MyComponent(An<IMyLogger>(), An<int>()))
        .And<IMyLogger>(() => new MyInvalidLogger());

    [Fact] public void ThenUseTheConcreteInstance() => Then().Throws<ArgumentOutOfRangeException>();
}

public class WhenIndirectlyUsingConcreteInstanceForInterface : SubjectSpec<InterfaceService, int>
{
    public WhenIndirectlyUsingConcreteInstanceForInterface()
        => When(_ => _.GetValue())
        .Given(A<MyComponent>)
        .And<IMyLogger>(() => new MyInvalidLogger());

    [Fact] public void ThenUseTheConcreteInstance() => Then().Throws<ArgumentOutOfRangeException>();
}

public class WhenUsingConcreteInstanceForInterfaceWithAutoMockedConstructorArgument : SubjectSpec<InterfaceService, int>
{
    public WhenUsingConcreteInstanceForInterfaceWithAutoMockedConstructorArgument()
        => When(_ => _.GetValue()).Given(A<MyComponent>).And(An<int>);

    [Fact] public void ThenAutoMockComponent() => Result.Is(The<int>());
}

public interface IMyService
{
    int GetValue();
}

public class MyService(MyComponent component, IMyLogger logger) : IMyService
{
    private readonly MyComponent _component = component;
    private readonly IMyLogger _logger = logger;

    public int GetValue() => _component.GetValue();
}

public class InterfaceService(MyComponent component, IMyService service)
{
    private readonly MyComponent _component = component;
    private readonly IMyService _service = service;

    public int GetValue() => _component.GetValue();

    public int GetServiceValue() => _service.GetValue();
}

public class MyComponent(IMyLogger logger, int value)
{
    private readonly IMyLogger _logger = logger;
    private readonly int _value = value;

    public int GetValue()
    {
        _logger.LogValue(_value);
        return _value;
    }
}

public interface IMyLogger
{
    void LogValue(int value);
}

public class MyInvalidLogger : IMyLogger
{
    public void LogValue(int value) => throw new ArgumentOutOfRangeException();
}