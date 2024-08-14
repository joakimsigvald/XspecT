namespace XspecT.Test.AutoMock;

public class WhenInjectingAnInterfaceWithUsing : Spec<InterfaceService, int>
{
    public WhenInjectingAnInterfaceWithUsing()
        => Given(new MyComponent(An<IMyLogger>(), An<int>())).When(_ => _.GetValue());

    [Fact]
    public void ThenGetValue()
    {
        Result.Is(The<int>());
        Description.Is(
@"Given new MyComponent(an IMyLogger, an int)
When GetValue()
Then Result is the int");
    }

    [Fact]
    public void ThenInterfaceIsMocked()
    {
        Then<IMyLogger>(_ => _.LogValue(The<int>()));
        Description.Is(
@"Given new MyComponent(an IMyLogger, an int)
When GetValue()
Then IMyLogger.LogValue(the int)");
    }
}

public class WhenUseConcreteInstanceOfInterface : Spec<InterfaceService, int>
{
    public WhenUseConcreteInstanceOfInterface()
        => Given(CreateService()).And(An<int>).When(_ => _.GetServiceValue());

    [Fact]
    public void ThenUseIt()
    {
        Result.Is(TheSecond<int>());
        Description.Is(
@"Given CreateService()
 and an int
When GetServiceValue()
Then Result is the second int");
    }

    protected IMyService CreateService() => new MyService(new MyComponent(A<IMyLogger>(), ASecond<int>()));
}

public class WhenUsingConcreteInstanceForInterface : Spec<InterfaceService, int>
{
    public WhenUsingConcreteInstanceForInterface()
        => When(_ => _.GetValue()).Given(() => new MyComponent(An<IMyLogger>(), An<int>()))
        .And<IMyLogger>(() => new MyInvalidLogger<ApplicationException>());

    [Fact]
    public void ThenUseTheConcreteInstance()
    {
        Then().Throws<ApplicationException>();
        Description.Is(
@"Given new MyInvalidLogger<ApplicationException>()
 and new MyComponent(an IMyLogger, an int)
When GetValue()
Then throws ApplicationException");
    }
}

public class WhenIndirectlyUsingConcreteInstanceForInterface : Spec<InterfaceService, int>
{
    public WhenIndirectlyUsingConcreteInstanceForInterface()
        => When(_ => _.GetValue())
        .Given(A<MyComponent>)
        .And<IMyLogger>(() => new MyInvalidLogger<ApplicationException>());

    [Fact]
    public void ThenUseTheConcreteInstance()
    {
        Then().Throws<ApplicationException>();
        Description.Is(
@"Given new MyInvalidLogger<ApplicationException>()
 and a MyComponent
When GetValue()
Then throws ApplicationException");
    }
}

public class WhenUsingConcreteInstanceForInterfaceWithAutoMockedConstructorArgument : Spec<InterfaceService, int>
{
    public WhenUsingConcreteInstanceForInterfaceWithAutoMockedConstructorArgument()
        => When(_ => _.GetValue()).Given(A<MyComponent>).And(An<int>);

    [Fact]
    public void ThenAutoMockComponent()
    {
        Result.Is(The<int>());
        Description.Is(
@"Given an int
 and a MyComponent
When GetValue()
Then Result is the int");
    }
}

public interface IMyService
{
    int GetValue();
}

public class MyService(MyComponent component) : IMyService
{
    private readonly MyComponent _component = component;
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

public class MyInvalidLogger<TException> : IMyLogger
    where TException : Exception, new()
{
    public void LogValue(int value) => throw new TException();
}