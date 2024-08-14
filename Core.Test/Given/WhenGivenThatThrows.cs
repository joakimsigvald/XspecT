namespace XspecT.Test.Given;

public class WhenGivenThatThrows : Spec<MyService, MyModel>
{
    [Fact]
    public void GivenExceptionType_ThenThrowsExceptionOfThatType()
    {
        When(_ => _.GetModel())
            .Given<IMyRepository>().That(_ => _.GetModel()).Throws<NotFound>()
            .Then().Throws<NotFound>();
        Description.Is(
            """
            Given IMyRepository.GetModel() throws NotFound
            When GetModel()
            Then throws NotFound
            """);
    }

    [Fact]
    public void GivenDefaultExceptionType_ThenThrowsExceptionOfThatType()
    {
        When(_ => _.GetModel())
            .Given<IMyRepository>().Throws<NotFound>()
            .Then().Throws<NotFound>();
        Description.Is(
            """
            Given IMyRepository throws NotFound
            When GetModel()
            Then throws NotFound
            """);
    }

    [Fact]
    public void GivenException_ThenThrowsThatException()
    {
        When(_ => _.GetModel())
             .Given<IMyRepository>().That(_ => _.GetModel()).Throws(A<NotFound>)
             .Then().Throws(The<NotFound>);
        Description.Is(
            """
            Given IMyRepository.GetModel() throws a NotFound
            When GetModel()
            Then throws the NotFound
            """);
    }

    [Fact]
    public void GivenDefaultException_ThenThrowsThatException()
    {
        When(_ => _.GetModel())
             .Given<IMyRepository>().Throws(A<NotFound>)
             .Then().Throws(The<NotFound>);
        Description.Is(
            """
            Given IMyRepository throws a NotFound
            When GetModel()
            Then throws the NotFound
            """);
    }

    [Fact]
    public void GivenSpecificException_ThenThrowsThatException()
    {
        When(_ => _.GetModel())
            .Given<IMyRepository>().That(_ => _.GetModel()).Throws(An<Exception>)
            .And(new Exception(A<string>()))
            .Then().Throws(The<Exception>).And().Throws<Exception>(_ => _.Message.Is(The<string>()));
        Description.Is(
            """
            Given new Exception(a string)
             and IMyRepository.GetModel() throws an Exception
            When GetModel()
            Then throws the Exception
             and throws Exception where _.Message is the string
            """);
    }

    [Fact]
    public void GivenExceptionWithProperties_ThenThrowsExceptionWithThoseProperties()
    {
        When(_ => _.GetModel())
             .Given<IMyRepository>().That(_ => _.GetModel()).Throws(A<NotFound>)
             .Then().Throws<NotFound>(_ => _.Message.Is(The<NotFound>().Message));
        Description.Is(
            """
            Given IMyRepository.GetModel() throws a NotFound
            When GetModel()
            Then throws NotFound where _.Message is the NotFound's Message
            """);
    }
}