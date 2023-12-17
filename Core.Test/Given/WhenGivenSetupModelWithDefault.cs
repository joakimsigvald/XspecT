using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenSetupModelWithDefault : SubjectSpec<MyService, MyModel>
{
    private const string _defaltName = "NoName";
    private static readonly MyModel _myModel = new() { Name = "My model" };

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
        .Given().That(() => ASecond(_myModel))
        .Then().Result.Is(_myModel);

    [Fact]
    public void GivenDefaultValue_ThenIgnoreItWhenGenerateModel()
        => Given(_defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName);
}