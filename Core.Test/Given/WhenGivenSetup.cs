using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenSetupValue : SubjectSpec<MyService, DateTime>
{
    private static readonly DateTime _now = DateTime.Now;
    private static readonly DateTime _anotherTime = DateTime.Now.AddDays(1);

    [Fact]
    public void ThenCanApplySpecificValueForPreviouslyMentionedType()
        => Given(A<DateTime>)
        .When(_ => _.GetTime())
        .Given().That(() => A(_now))
        .Then().Result.Is(_now);

    [Fact]
    public void ThenApplyFirstSpecifiedValueForPreviouslyMentionedType()
        => Given(A<DateTime>)
        .When(_ => _.GetTime())
        .Given().That(() => A(_now))
        .And().That(() => A(_anotherTime)) //Ignore this since a specific value has already been provided
        .Then().Result.Is(_now);
}


public class WhenGivenSetupModel : SubjectSpec<MyService, MyModel>
{
    private static readonly MyModel _myModel = new() { Name = "My model" };

    [Fact]
    public void ThenCanApplySpecificValueToSecondPosition()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Given().That(() => ASecond(_myModel))
        .Then().Result.Is(_myModel);

    [Fact]
    public void ThenCanApplyNullAsSpecificValueToSecondPosition()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
        .When(_ => _.GetModel())
        .Given().That(() => ASecond((MyModel)null))
        .Then().Result.Is().Null();

    [Fact]
    public void ThenOnlyApplyNullAsSpecificValueToMentionedPosition()
        => Given<IMyRepository>().That(_ => _.GetModels()).Returns(One<MyModel>)
        .When(_ => _.GetModel()) //Unspecified default value
        .Given().That(() => A((MyModel)null))
        .Then().Result.Is().NotNull(); //Not the provide null mentioned value
}