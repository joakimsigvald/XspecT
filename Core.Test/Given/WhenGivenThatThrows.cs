﻿using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenThatThrows : SubjectSpec<MyService, MyModel>
{
    [Fact]
    public void GivenExceptionType_ThenThrowsExceptionOfThatType()
        => When(_ => _.GetModel())
        .Given<IMyRepository>().That(_ => _.GetModel()).Throws<NotFound>()
        .Then().Throws<NotFound>();

    [Fact]
    public void GivenException_ThenThrowsThatException()
         => When(_ => _.GetModel())
         .Given<IMyRepository>().That(_ => _.GetModel()).Throws(A<NotFound>)
         .Then().Throws(The<NotFound>);

    [Fact]
    public void GivenSpecificException_ThenThrowsThatException()
        => When(_ => _.GetModel())
        .Given<IMyRepository>().That(_ => _.GetModel()).Throws(An<Exception>)
        .Given(new Exception(A<string>()))
        .Then().Throws(The<Exception>).And().Throws<Exception>(_ => _.Message.Is(The<string>()));

    [Fact]
    public void GivenExceptionWithProperties_ThenThrowsExceptionWithThoseProperties()
         => When(_ => _.GetModel())
         .Given<IMyRepository>().That(_ => _.GetModel()).Throws(A<NotFound>)
         .Then().Throws<NotFound>(_ => _.Message.Is(The<NotFound>().Message));
}