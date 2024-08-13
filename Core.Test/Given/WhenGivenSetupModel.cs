namespace XspecT.Test.Given;

public class WhenGivenSetupModel : Spec<MyService, MyModel>
{
    private static readonly MyModel _myModel = new() { Name = "My model" };

    [Fact]
    public void ThenCanApplySpecificValueToSecondPosition()
    {
        Given<IMyRepository>().That(_ => _.GetModel()).Returns(ASecond<MyModel>)
            .When(_ => _.GetModel())
            .Given().ASecond(_myModel)
            .Then().Result.Is(_myModel);
        VerifyDescription(
            """
            Given a second MyModel { _myModel }
             and IMyRepository.GetModel() returns a second MyModel
            When GetModel()
            Then Result is _myModel
            """);
    }

    [Fact]
    public void ThenCanApplyNullAsSpecificValueToSecondPosition()
    {
        Given<IMyRepository>().That(_ => _.GetModel()).Returns(() => ASecond<MyModel>())
            .When(_ => _.GetModel())
            .Given().ASecond((MyModel)null)
            .Then().Result.Is().Null();
        VerifyDescription(
            """
            Given a second MyModel { (MyModel)null }
             and IMyRepository.GetModel() returns a second MyModel
            When GetModel()
            Then Result is null
            """);
    }

    [Fact]
    public void ThenOnlyApplyNullAsSpecificValueToMentionedPosition()
    {
        Given<IMyRepository>().That(_ => _.GetModels()).Returns(() => One<MyModel>())
            .When(_ => _.GetModel()) //Unspecified default value
            .Given().A((MyModel)null)
            .Then().Result.Is().NotNull(); //Not the provide null mentioned value
        VerifyDescription(
            """
            Given a MyModel { (MyModel)null }
             and IMyRepository.GetModels() returns one MyModel
            When GetModel()
            Then Result is not null
            """);
    }
}