using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenInjectingAnInterfaceWithUsing : SubjectSpec<InterfaceService, int>
{
    public WhenInjectingAnInterfaceWithUsing() 
        => Using(new MyComponent(An<IMyLogger>(), An<int>())).When(_ => _.GetValue());

    [Fact] public void ThenGetValue() => Result.Is(The<int>());
    [Fact] public void ThenInterfaceIsMocked() => Then<IMyLogger>(_ => _.LogValue(The<int>()));
}

public class InterfaceService
{
    private readonly MyComponent _component;

    public InterfaceService(MyComponent component) => _component = component;

    public int GetValue() => _component.GetValue();
}

public class MyComponent
{
    private readonly IMyLogger _logger;
    private readonly int _value;

    public MyComponent(IMyLogger logger, int value)
    {
        _logger = logger;
        _value = value;
    }

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