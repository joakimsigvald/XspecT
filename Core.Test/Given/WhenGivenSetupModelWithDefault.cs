using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenSetupModelWithDefault : SubjectSpec<MyService, MyModel>
{
    private const string _defaltName = "NoName";

    [Fact]
    public void GivenDefaultWithAutoMock()
        => Given<MyModel>(_ => _.Name = _defaltName)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName);

    [Fact]
    public void GivenDefaultNotOverridden()
        => Given<MyModel>(_ => _.Name = _defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName);

    [Fact]
    public void GivenTwoDefaultSetups_ThenApplySecond()
        => Given<MyModel>(_ => _.Name = "123")
        .Given<MyModel>(_ => _.Name = _defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName);

    [Fact]
    public void GivenTwoDifferentDefaultSetups_ThenApplyBoth()
        => Given<MyModel>(_ => _.Id = 123)
        .Given<MyModel>(_ => _.Name = _defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName).And(Result).Id.Is(123);

    [Fact]
    public void GivenDefaultValueAndDefaultSetup()
        => Given(_defaltName)
        .Given<MyModel>(_ => _.Name = A<string>())
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName);

    [Fact]
    public void GivenDefaultIsOverridden()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Given<MyModel>(_ => _.Name = _defaltName)
        .And().That(() => ASecond<MyModel>(_ => _.Name = "Altered"))
        .Then().Result.Name.Is("Altered");

    [Fact]
    public void GivenDefaultIsReplaced()
        => Given<MyModel>(_ => _.Name = _defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Given().That(() => ASecond(new MyModel() { Name = "My model" }))
        .Then().Result.Name.Is("My model");

    [Fact]
    public void GivenDefaultValue_ThenIgnoreItWhenGenerateModel()
        => Given(_defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName);

    [Fact]
    public void GivenProvideDefaultSetupAfterModelIsUsedInWhen_ThenUseSetup()
        => Given(_defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName);
}

public class OverrideDefaultSetupAfterWhenReturn : SubjectSpec<MyService, MyModel>
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

public class OverrideDefaultValueAfterWhenReturn : SubjectSpec<MyService, MyModel>
{
    private const string _theName = "TheName";

    public OverrideDefaultValueAfterWhenReturn()
        => Given("Something").When(_ => _.GetModel());

    [Fact]
    public void GivenDefaultValue_ThenUseDefaultValue()
        => Given<MyModel>(_ => _.Name = _theName).Then().Result.Name.Is(_theName);
}

public class OverrideDefaultSetupAfterWhenArgument : SubjectSpec<MyService, MyModel>
{
    private const string _theName = "TheName";

    public OverrideDefaultSetupAfterWhenArgument()
        => Given<MyModel>(_ => _.Name = "Something")
        .When(_ => _.Echo(A<MyModel>()));

    [Fact]
    public void GivenDefaultSetup_ThenUseOverride()
        => Given<MyModel>(_ => _.Name = _theName)
        .Then().Result.Name.Is(_theName);
}

public class OverrideDefaultValueAfterWhenArgument : SubjectSpec<MyService, MyModel>
{
    private const string _theName = "TheName";

    public OverrideDefaultValueAfterWhenArgument()
        => Given("Something").When(_ => _.Echo(A<MyModel>()));

    [Fact]
    public void GivenDefaultValue_ThenUseDefaultValue()
        => Given<MyModel>(_ => _.Name = _theName).Then().Result.Name.Is(_theName);

    [Fact]
    public void GivenDefaultSetup_ThenUseDefaultValue()
        => Given(_theName).Then().Result.Name.Is(_theName);
}