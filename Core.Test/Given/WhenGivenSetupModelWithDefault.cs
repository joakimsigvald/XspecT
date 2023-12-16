using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenSetupModelWithDefault : SubjectSpec<MyService, MyModel>
{
    private const string _defaltName = "NoName";
    private static readonly MyModel _myModel = new() { Name = "My model" };

    [Fact]
    public void GivenDefaultNotOverridden()
        => GivenDefault<MyModel>(_ => _.Name = _defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(_defaltName);

    [Fact]
    public void GivenDefaultIsOverridden()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .GivenDefault<MyModel>(_ => _.Name = _defaltName)
        .And().That(() => ASecond<MyModel>(_ => _.Name = "Altered"))
        .Then().Result.Name.Is("Altered");

    [Fact]
    public void GivenDefaultIsReplaced()
        => GivenDefault<MyModel>(_ => _.Name = _defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Given().That(() => ASecond(_myModel))
        .Then().Result.Is(_myModel);

    [Fact]
    public void GivenDefaultValue_ThenIgnoreItWhenGenerateModel()
        => GivenDefault(_defaltName)
        .And<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Name.Is().Not(_defaltName);
}