using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenSetupModelWithDefault : Spec<MyService, MyModel>
{
    private const string _defaultName = "NoName";

    [Fact]
    public void GivenDefaultWithAutoMock()
        => Given<MyModel>(_ => _.Name = _defaultName)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaultName);

    [Fact]
    public void GivenDefaultNotOverridden()
        => Given<MyModel>(_ => _.Name = _defaultName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaultName);

    [Fact]
    public void GivenTwoDefaultSetups_ThenApplySecond()
        => Given<MyModel>(_ => _.Name = "123")
        .Given<MyModel>(_ => _.Name = _defaultName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaultName);

    [Fact]
    public void GivenTwoDifferentDefaultSetups_ThenApplyBoth()
        => Given<MyModel>(_ => _.Id = 123)
        .Given<MyModel>(_ => _.Name = _defaultName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaultName).And(Result).Id.Is(123);

    [Fact]
    public void GivenDefaultValueAndDefaultSetup()
        => Given(_defaultName)
        .Given<MyModel>(_ => _.Name = A<string>())
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaultName);

    [Fact]
    public void GivenDefaultIsOverridden()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Given<MyModel>(_ => _.Name = _defaultName)
        .And().ASecond<MyModel>(_ => _.Name = "Altered")
        .Then().Result.Name.Is("Altered");

    [Fact]
    public void GivenDefaultIsNotOverridden()
        => When(_ => MyService.Echo(A<MyModel>()))
        .Given<IMyRepository>().That(_ => _.GetModel()).Returns(() => The<MyModel>())
        .Given<MyModel>(_ => _.Name = _defaultName)
        .Then().Result.Name.Is(_defaultName);

    [Fact]
    public void GivenDefaultIsReplaced()
        => Given<MyModel>(_ => _.Name = _defaultName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Given().ASecond(new MyModel() { Name = "My model" })
        .Then().Result.Name.Is("My model");

    [Fact]
    public void GivenDefaultValue_ThenIgnoreItWhenGenerateModel()
        => Given(_defaultName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaultName);

    [Fact]
    public void GivenProvideDefaultSetupAfterModelIsUsedInWhen_ThenUseSetup()
        => Given(_defaultName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaultName);

    [Fact]
    public void GivenModel_ReferencedAsInputTwice_AndWithDefaultSetup_ThenUseDefaultSetup()
        => When(_ => MyService.Echo(A<MyModel>()))
        .Given<IMyRepository>().That(_ => _.SetModel(The<MyModel>())).Returns(() => Another<MyModel>())
        .Given<MyModel>(_ => _.Id = 123)
        .Then().Result.Id.Is(123);
}

public class OverrideDefaultSetupAfterWhenReturn : Spec<MyService, MyModel>
{
    private const string _theName = "TheName";

    public OverrideDefaultSetupAfterWhenReturn()
        => Given<MyModel>(_ => _.Name = "Something")
        .When(_ => _.GetModel());

    [Fact]
    public void GivenDefaultSetup_ThenUseOverride()
        => Given<MyModel>(_ => _.Name = _theName)
        .Then().Result.Name.Is(_theName);
}

public class OverrideDefaultValueAfterWhenReturn : Spec<MyService, MyModel>
{
    private const string _theName = "TheName";

    public OverrideDefaultValueAfterWhenReturn()
        => Given("Something").When(_ => _.GetModel());

    [Fact]
    public void GivenDefaultValue_ThenUseDefaultValue()
        => Given<MyModel>(_ => _.Name = _theName).Then().Result.Name.Is(_theName);
}

public class OverrideDefaultSetupAfterWhenArgument : Spec<MyService, MyModel>
{
    private const string _theName = "TheName";

    public OverrideDefaultSetupAfterWhenArgument()
        => Given<MyModel>(_ => _.Name = "Something")
        .When(_ => MyService.Echo(A<MyModel>()));

    [Fact]
    public void GivenDefaultSetup_ThenUseOverride()
        => Given<MyModel>(_ => _.Name = _theName)
        .Then().Result.Name.Is(_theName);
}

public class OverrideDefaultValueAfterWhenArgument : Spec<MyService, MyModel>
{
    private const string _theName = "TheName";

    public OverrideDefaultValueAfterWhenArgument()
        => Given("Something").When(_ => MyService.Echo(A<MyModel>()));

    [Fact]
    public void GivenDefaultValue_ThenUseDefaultValue()
        => Given<MyModel>(_ => _.Name = _theName).Then().Result.Name.Is(_theName);

    [Fact]
    public void GivenDefaultSetup_ThenUseDefaultValue()
        => Given(_theName).Then().Result.Name.Is(_theName);
}