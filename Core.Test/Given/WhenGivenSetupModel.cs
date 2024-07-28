namespace XspecT.Test.Given;

public class WhenGivenSetupModel : Spec<MyService, MyModel>
{
    private static readonly MyModel _myModel = new() { Name = "My model" };

    [Fact]
    public void ThenCanApplySpecificValueToSecondPosition()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Given().ASecond(_myModel)
        .Then().Result.Is(_myModel);

    [Fact]
    public void ThenCanApplyNullAsSpecificValueToSecondPosition()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
        .When(_ => _.GetModel())
        .Given().ASecond((MyModel)null)
        .Then().Result.Is().Null();

    [Fact]
    public void ThenOnlyApplyNullAsSpecificValueToMentionedPosition()
        => Given<IMyRepository>().That(_ => _.GetModels()).Returns(() => One<MyModel>())
        .When(_ => _.GetModel()) //Unspecified default value
        .Given().A((MyModel)null)
        .Then().Result.Is().NotNull(); //Not the provide null mentioned value
}